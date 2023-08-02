using Microsoft.AspNetCore.Mvc;
using CarEnthusiast.Data;
using Microsoft.EntityFrameworkCore;
using CarEnthusiast.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using CarEnthusiast.Hubs;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace CarEnthusiast.Controllers
{
    
    public class ChatController : Controller
    {

        private readonly UserContext _context;

        public ChatController (UserContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
            var messages = _context.Messages.ToList();

            return View(messages);
        }

        public IActionResult GroupChat()
        {
            var groups = _context.Groups.Include(g => g.Messages).ToList();
            return View(groups);
        }

        public IActionResult GroupDetails(int groupId)
        {
            var group = _context.Groups
                        .Include(g => g.Messages)
                        .FirstOrDefault(g => g.Id == groupId);
            
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }
    }
}
