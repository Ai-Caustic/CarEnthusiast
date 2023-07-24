﻿using System;
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
    [Authorize]
    public class CarsController : Controller
    {
        private readonly UserContext _context;
        private readonly UserManager<User> _userManager;

        public CarsController(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
              return _context.Cars != null ? 
                          View(await _context.Cars.ToListAsync()) :
                          Problem("Entity set 'UserContext.Cars'  is null.");
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

        // GET: Cars/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarViewModel carViewModel)
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

                return RedirectToAction(nameof(Index));
            }

            return View("Create");
        }

        public IActionResult GetImage(int Id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == Id);

            if (car != null && car.Image != null)
            {
                return File(car.Image, "image/jpg"); // Adjust the MIME type according to your image format
            }
            else
            {
                return null; //Handles the case when the image is not found
            }
            
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

        // POST: Cars/Edit/5
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

        // GET: Cars/Delete/5
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

        // POST: Cars/Delete/5
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
