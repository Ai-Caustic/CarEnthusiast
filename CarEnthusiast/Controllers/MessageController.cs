//using CarEnthusiast.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Diagnostics;
//using CarEnthusiast.Data;
//using Microsoft.EntityFrameworkCore;
//using System.Web.WebPages.Html;

//namespace CarEnthusiast.Controllers
//{
//    public class MessageController : Controller
//    {
//        //public readonly UserManager<IdentityUser> _userManager;
//        public readonly UserManager<IdentityUser> _userManager;

//        public readonly UserContext _context;
//        private readonly ILogger<MessageController> _logger;

//        public MessageController(UserManager<IdentityUser> userManager, UserContext context)
//        {
//            _userManager = userManager;
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
//            var currentUser = await _userManager.GetUserAsync(User);
//            ViewBag.CurrentUserName = currentUser.UserName;
//            var messages = await _context.Messages.ToListAsync();
//            return View();
//        }

//        public async Task<IActionResult> Create(Message message)
//        {
//            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
//            if (ModelState.IsValid)
//            {
//                message.UserName = User.Identity.Name;
//                var sender = _userManager.GetUserAsync(User);
//                message.UserId = sender.Id;
//                await _context.Messages.AddAsync(message);
//                await _context.SaveChangesAsync();
//                return Ok();
//            }
//            return Error();
//        }
//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
