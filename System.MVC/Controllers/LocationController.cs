using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.DAL.Data;
using System.DAL.Models;
using System.MVC.ViewModels;

namespace System.MVC.Controllers
{
    public class LocationController : Controller
    {
        private readonly AppDbContext _context;

        public LocationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Location
        public async Task<IActionResult> Index()
        {
            var locations = await _context.Locations
                .Select(l => new LocationViewModel
                {
                    LocationID = l.LocationID,
                    LocationName = l.LocationName
                }).ToListAsync();

            return View(locations);
        }

        // GET: Location/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Select(l => new LocationViewModel
                {
                    LocationID = l.LocationID,
                    LocationName = l.LocationName
                }).FirstOrDefaultAsync(m => m.LocationID == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var location = new Location
                {
                    LocationName = viewModel.LocationName
                };

                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Location/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            var viewModel = new LocationViewModel
            {
                LocationID = location.LocationID,
                LocationName = location.LocationName
            };

            return View(viewModel);
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LocationViewModel viewModel)
        {
            if (id != viewModel.LocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var location = await _context.Locations.FindAsync(id);
                    location.LocationName = viewModel.LocationName;

                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(viewModel.LocationID))
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
            return View(viewModel);
        }

        // GET: Location/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Select(l => new LocationViewModel
                {
                    LocationID = l.LocationID,
                    LocationName = l.LocationName
                }).FirstOrDefaultAsync(m => m.LocationID == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.LocationID == id);
        }
    }

}
