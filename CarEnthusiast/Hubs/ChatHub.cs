using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using CarEnthusiast.Models;
using Microsoft.AspNetCore.Identity;
using CarEnthusiast.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace CarEnthusiast.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserContext _context;
        private readonly UserManager<User> _userManager;

        public ChatHub ( UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SendMessage(string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);

            if (user != null)
            {
                var newMessage = new Message
                {

                    UserName = user.UserName,
                    Text = message,
                    UserId = user.Id
                };

                _context.Messages.Add(newMessage);
                await _context.SaveChangesAsync();

                await Clients.All.SendAsync("ReceiveMessage", user.UserName, message);
            }
        }

        public async Task JoinGroup(int groupId, string groupName)
        {
            //var groupId = int.Parse(Id);
            var user = await _userManager.GetUserAsync(Context.User);

            if (user != null)
            {
                // Check if user is already in a group
                var group = await _context.UserGroups.FirstOrDefaultAsync(ug => ug.UserId == user.Id && ug.GroupId == groupId);

                if (group == null)
                {
                    var userGroup = new UserGroup
                    {
                        UserId = user.Id,
                        GroupId = groupId
                    };

                    _context.UserGroups.Add(userGroup);
                    await _context.SaveChangesAsync();

                    //Add User to Group
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                }
                else
                {
                    await Clients.Caller.SendAsync("AlreadyJoinedGroup", groupId);
                }
                
            }
        }

        public async Task LeaveGroup(int groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId.ToString());
        }

        
        public async Task SendGroupMessage(int groupId, string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);

            if (user != null)
            {
                var newMessage = new Message
                {
                    UserName = user.UserName,
                    Text = message,
                    UserId = user.Id,
                    GroupId = groupId
                };

                _context.Messages.Add(newMessage);
                await _context.SaveChangesAsync();

                await Clients.Groups(groupId.ToString()).SendAsync("ReceiveMessage", user.UserName, message);
            }
        }

        public async Task CreateGroup(string groupName, string groupDesc)
        {
            var user = await _userManager.GetUserAsync(Context.User);

            if (user == null || string.IsNullOrWhiteSpace(groupName))
            {
                // Handle invalid input or user not found
                return;
            }

            // Check if a group with the same name already exists
            var existingGroup = await _context.Groups.FirstOrDefaultAsync(g => g.GroupName == groupName);

            if (existingGroup != null)
            {
                // Notify the client that the group name already exists
                await Clients.Caller.SendAsync("GroupAlreadyExists", groupName);
                return;
            }

            // Create the new group
            var group = new Group
            {
                GroupName = groupName,
                GroupDescription = groupDesc
            };

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            // Notify all clients that a new group has been created
            await Clients.All.SendAsync("NewGroupCreated", group);
        }

        //Check if user is already in a group
        public async Task CheckUserInGroup(int groupId)
        {
            var user = await _userManager.GetUserAsync(Context.User);

            if (user != null)
            {
                var group = await _context.UserGroups.FirstOrDefaultAsync(ug => ug.UserId == user.Id && ug.GroupId == groupId);

                if (group != null)
                {
                    await Clients.Caller.SendAsync("UserInGroup", true);
                }
                else
                {
                    await Clients.Caller.SendAsync("UserInGroup", false);
                }
            }
        }

    }
}

