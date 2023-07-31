using Microsoft.AspNetCore.Mvc;
using CarEnthusiast.Data;
using Microsoft.EntityFrameworkCore;

namespace CarEnthusiast.Controllers
{
    public class ChatController : Controller
    {

        private readonly UserContext _context;

        public ChatController (UserContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var messages = _context.Messages.ToList();

            return View(messages);
        }
        //public async Task<IActionResult> Index()
        //{
            
        //    var messages = await _context.Messages.ToListAsync();
        //    return View(messages);
        //}
        
        public IActionResult GroupChat()
        {
            return View();
        }
    }
}
