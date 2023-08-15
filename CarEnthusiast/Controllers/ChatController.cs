using Microsoft.AspNetCore.Mvc;
using CarEnthusiast.Data;
using Microsoft.EntityFrameworkCore;
using CarEnthusiast.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using CarEnthusiast.Hubs;
// using CarEnthusiast.Migrations
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace CarEnthusiast.Controllers
{
    
    public class ChatController : Controller
    {

        private readonly UserContext _context;
        private readonly UserManager<User> _userManager;

        public ChatController (UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Chat(int? groupId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Identity/Account");
            }

            var userId = user.Id;

            var userGroupsIds = _context.UserGroups
                             .Where(ug => ug.UserId == userId)
                             .Select(ug => ug.GroupId)
                             .ToList();

            var userGroups = _context.Groups
                             .Where(group => userGroupsIds.Contains(group.Id))
                             .ToList();

            Group selectedGroup = null;

            if (groupId.HasValue)
            {
                selectedGroup = userGroups.FirstOrDefault(group => group.Id == groupId);

                if (selectedGroup == null)
                {
                    // Handle case where the selected group is not found
                    return View("Chat");
                }

                // Load messages associated with the selected group
                _context.Entry(selectedGroup).Collection(g => g.Messages).Load();
            }


            var viewModel = new GroupChatViewModel
            {
                UserGroups = userGroups,
                SelectedGroup = selectedGroup,
                Messages = selectedGroup?.Messages?.OrderByDescending(msg => msg.dateTime).ToList()
            };

            return View(viewModel);
        }
      

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                var userMessages = _context.Messages.Where(m => m.UserId == userId && m.GroupId == null)
                                                    .OrderBy(m => m.dateTime)
                                                    .ToList();
                var otherUserMessages = _context.Messages.Where(m => m.UserId != userId && m.GroupId == null)
                                                         .OrderBy(m => m.dateTime)
                                                         .ToList();

                if (userMessages.Count == 0)
                {
                    var mtViewModel = new MessageViewModel
                    {
                        UserMessages = new List<Message>(),
                        OtherMessages = otherUserMessages
                    };

                    return View(mtViewModel);
                }

                var viewModel = new MessageViewModel
                {
                    UserMessages = userMessages,
                    OtherMessages = otherUserMessages
                };
                  return View(viewModel);              
            }
            else
            {
                //Handle Message View if user is not logged in TODO:
                var messages = _context.Messages.Where(m => m.GroupId == null).ToList();


                return View(messages);
            }
            
        }

        public IActionResult GroupChat()
        {
            var groups = _context.Groups.Include(g => g.Messages).ToList();
            return View(groups);
        }

        public async Task<IActionResult> GroupDetails(int groupId, string groupName)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = user.Id;
                var userMessages = _context.Messages
                                   .Where(m => m.UserId == userId && m.GroupId == null)
                                   .OrderBy(m => m.dateTime)
                                   .ToList();
                var userGroupMessages = _context.Messages
                                    .Where(m => m.UserId == userId && m.GroupId == groupId)
                                    .OrderBy(m => m.dateTime)
                                    .ToList();
                var otherUserGroupMessages = _context.Messages
                                    .Where(m => m.UserId != userId && m.GroupId == groupId)
                                    .OrderBy(m => m.dateTime)
                                    .ToList();

                var viewModel = new MessageViewModel
                {
                    UserMessages = userGroupMessages,
                    OtherMessages = otherUserGroupMessages,
                    GroupId = groupId,
                    GroupName = groupName
                };
                return View(viewModel);
            }
            return View("GroupChat");
        }
    }
}
