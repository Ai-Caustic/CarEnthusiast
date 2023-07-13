//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using CarEnthusiast.Data;
//using CarEnthusiast.Models;
//using Microsoft.AspNetCore.Identity;
//using System.Web.WebPages;
//using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;

//namespace CarEnthusiast.Controllers
//{
//    public class UsersController : Controller
//    {
//        private readonly UserContext _context;

//        public UsersController(UserContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            return View("Login");
//        }

//        // Post: User
//        [HttpPost]
//        public async Task<ActionResult> Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var userdetails = await _context.Users.SingleOrDefaultAsync(m => m.Email == model.email && m.Password == model.password);
//                if (userdetails == null)
//                {
//                    ModelState.AddModelError("Password", "Invalid login attempt");
//                    return View("Login");
//                }
//                HttpContext.Session.SetString("userId", userdetails.UserName);
//            }
//            else
//            {
//                ModelState.AddModelError("Password", "Fill in the fields");
//                return View("Login");
//            }

//            // TODO: Add the correct view for after logging in
//            return View("Accounts");
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {  
//                if (ModelState.IsValid)
//                {
//                    User user = new User
//                    {
//                        UserName = model.Name,
//                        Email = model.Email,
//                        CreatedAt = model.CreatedAt,
//                        Password = model.Password
//                    };
//                    _context.Add(user);
//                    await _context.SaveChangesAsync();
//                    //return RedirectToAction(nameof(Index));
//                }
//                else
//            {
//                ModelState.AddModelError("ConfirmPassword", "Fill in the fields");
//                return View("Register");
//            }
  
//            return View("Login");
//        }


//        public async Task<IActionResult> Accounts()
//        {
//            return _context.Users != null ?
//                          View(await _context.Users.ToListAsync()) :
//                          Problem("Entity set 'UserContext.Users'  is null.");
//        }

//        // GET: Users/Details/5
//        //public async Task<IActionResult> Details(int? id)
//        //{
//        //    if (id == null || _context.Users == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    var user = await _context.Users
//        //        .FirstOrDefaultAsync(m => m.Id == id);
//        //    if (user == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    return View(user);
//        //}

//        // GET: Users/Login
//        public IActionResult Login()
//        {
//            return View();
//        }

//        // GET: Users/Create
//        public IActionResult Register()
//        {
//            return View();
//        }

//        // POST: Users/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

//        // GET: Users/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var user = await _context.Users.FindAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }
//            return View(user);
//        }

//        // POST: Users/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,CreatedAt")] User user)
//        //{
//        //    if (id != user.Id)
//        //    {
//        //        return NotFound();
//        //    }

//        //    if (ModelState.IsValid)
//        //    {
//        //        try
//        //        {
//        //            _context.Update(user);
//        //            await _context.SaveChangesAsync();
//        //        }
//        //        catch (DbUpdateConcurrencyException)
//        //        {
//        //            if (!UserExists(user.Id))
//        //            {
//        //                return NotFound();
//        //            }
//        //            else
//        //            {
//        //                throw;
//        //            }
//        //        }
//        //        return RedirectToAction(nameof(Index));
//        //    }
//        //    return View(user);
//        //}

//        // GET: Users/Delete/5
//        //public async Task<IActionResult> Delete(int? id)
//        //{
//        //    if (id == null || _context.Users == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    var user = await _context.Users
//        //        .FirstOrDefaultAsync(m => m.Id == id);
//        //    if (user == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    return View(user);
//        //}

//        // POST: Users/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Users == null)
//            {
//                return Problem("Entity set 'UserContext.Users'  is null.");
//            }
//            var user = await _context.Users.FindAsync(id);
//            if (user != null)
//            {
//                _context.Users.Remove(user);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        //private bool UserExists(int id)
//        //{
//        //  return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
//        //}

//        //public async Task<IActionResult> OnPostAsync (string returnUrl = null)
//        //{
//        //    returnUrl = returnUrl ?? Url.Content("/");
//        //    ExternalLoginsModel = (await _signInManager.GetExternalLoginsAsync());
//        //}
//    }
//}
