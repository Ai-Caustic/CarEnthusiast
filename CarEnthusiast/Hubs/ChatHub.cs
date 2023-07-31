using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using CarEnthusiast.Models;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using CarEnthusiast.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        [Authorize]
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
            //await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name ,message);
        }

    }
}

