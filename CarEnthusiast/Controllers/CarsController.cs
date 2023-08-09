using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarEnthusiast.Data;
using CarEnthusiast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CarEnthusiast.Controllers
{
    //[Authorize]
    public class CarsController : Controller
    {
        private readonly UserContext _context;
        private readonly UserManager<User> _userManager;

        public CarsController(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CarViewModel carViewModel)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return NotFound();
                }

                if (carViewModel.UploadedImages != null && carViewModel.UploadedImages.Count > 1)
                {
                    // If multiple images are uploaded, store them in the CarImage Table
                    var car = new Car
                    {
                        Make = carViewModel.Make,
                        Model = carViewModel.Model,
                        Year = carViewModel.Year,
                        Showroom = carViewModel.Showroom,
                        UserId = user.Id
                    };

                    foreach (var uploadedImage in carViewModel.UploadedImages)
                    {
                        if (uploadedImage != null && uploadedImage.Length > 0)
                        {
                            using var memoryStream = new MemoryStream();
                            await uploadedImage.CopyToAsync(memoryStream);

                            var carImage = new CarImage
                            {
                                ImageData = memoryStream.ToArray(),
                                FileName = uploadedImage.FileName
                            };

                            car.CarImages.Add(carImage);
                        }
                    }

                    _context.Cars.Add(car);
                }
                else
                {
                    // If only one image is uploaded, store it in the Car table as a byte array
                    var uploadedImage = carViewModel.UploadedImages?.FirstOrDefault();

                    if (uploadedImage != null && uploadedImage.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        await uploadedImage.CopyToAsync(memoryStream);

                        var car = new Car
                        {
                            Make = carViewModel.Make,
                            Model = carViewModel.Model,
                            Year = carViewModel.Year,
                            Showroom = carViewModel.Showroom,
                            UserId = user.Id,
                            Image = memoryStream.ToArray()
                        };

                        _context.Cars.Add(car);
                    }

                }

                await _context.SaveChangesAsync();

                return View("Land");
            }

            return View("Add");
        }

        public IActionResult Land()
        {
            return View();
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            var userCars = _context.Cars
                .Include(c => c.CarImages) // Include the CarImages navigation property
                .Where(c => c.UserId == userId)
                .ToList();

            return userCars != null ? View(userCars) : Problem("Entity Set 'Cars' is null"); 

        }


        public async Task<IActionResult> List()
        {
            var cars = await _context.Cars.Include(c => c.CarImages).ToListAsync();
            return View(cars);
        }

        public IActionResult Tester()
        {
            return View();
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }


        public IActionResult GetImage(int Id, int imageIndex = 0)
        {
            //var car = _context.Cars.FirstOrDefault(c => c.Id == Id);
            var car = _context.Cars.Include(c => c.CarImages).FirstOrDefault(c => c.Id == Id);

            if (car != null && car.CarImages != null && car.CarImages.Count > 0)
            {
                if (car.CarImages.Count > 1 && imageIndex >= 0 && imageIndex < car.CarImages.Count)
                {
                    // If the car has multiple images in CarImage table and the imageINdex is valid,return the image specified
                    var image = car.CarImages[imageIndex];
                    return File(image.ImageData, "image/jpg");
                }
                else
                {
                    // If the car has only one image in the CarImage table or the provided index is invalid,
                    // return the first image from the CarImages list (index 0).
                    var firstImage = car.CarImages[0];
                    return File(firstImage.ImageData, "image/jpg");
                }
            }
            else if (car != null && car.Image != null)
            {
                // If the car has only one image stored directly in the Car table, return that image
                return File(car.Image, "image/jpg");
            }

            var defaultImage = System.IO.File.ReadAllBytes("~/Assets/Toy.jpg");
            return File(defaultImage, "image/jpg");

        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Make,Model,Year,Image,Showroom")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'UserContext.Cars'  is null.");
            }
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
          return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
