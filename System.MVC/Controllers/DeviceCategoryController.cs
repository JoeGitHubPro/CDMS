using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.DAL.Data;
using System.DAL.Models;
using System.MVC.ViewModels;

namespace System.MVC.Controllers
{
    public class DeviceCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public DeviceCategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DeviceCategory
        public async Task<IActionResult> Index()
        {
            var categories = await _context.DeviceCategories
                .Select(dc => new DeviceCategoryViewModel
                {
                    CategoryID = dc.CategoryID,
                    CategoryName = dc.CategoryName,
                    CategoryDescription = dc.CategoryDescription
                }).ToListAsync();

            return View(categories);
        }

        // GET: DeviceCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories
                .Select(dc => new DeviceCategoryViewModel
                {
                    CategoryID = dc.CategoryID,
                    CategoryName = dc.CategoryName,
                    CategoryDescription = dc.CategoryDescription
                })
                .FirstOrDefaultAsync(m => m.CategoryID == id);

            if (deviceCategory == null)
            {
                return NotFound();
            }

            return View(deviceCategory);
        }

        // GET: DeviceCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeviceCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceCategoryViewModel deviceCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var deviceCategory = new DeviceCategory
                {
                    CategoryName = deviceCategoryViewModel.CategoryName,
                    CategoryDescription = deviceCategoryViewModel.CategoryDescription
                };

                _context.Add(deviceCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deviceCategoryViewModel);
        }

        // GET: DeviceCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories
                .Select(dc => new DeviceCategoryViewModel
                {
                    CategoryID = dc.CategoryID,
                    CategoryName = dc.CategoryName,
                    CategoryDescription = dc.CategoryDescription
                })
                .FirstOrDefaultAsync(m => m.CategoryID == id);

            if (deviceCategory == null)
            {
                return NotFound();
            }

            return View(deviceCategory);
        }

        // POST: DeviceCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeviceCategoryViewModel deviceCategoryViewModel)
        {
            if (id != deviceCategoryViewModel.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var deviceCategory = await _context.DeviceCategories.FindAsync(id);
                    deviceCategory.CategoryName = deviceCategoryViewModel.CategoryName;
                    deviceCategory.CategoryDescription = deviceCategoryViewModel.CategoryDescription;

                    _context.Update(deviceCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceCategoryExists(deviceCategoryViewModel.CategoryID))
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
            return View(deviceCategoryViewModel);
        }

        // GET: DeviceCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories
                .Select(dc => new DeviceCategoryViewModel
                {
                    CategoryID = dc.CategoryID,
                    CategoryName = dc.CategoryName,
                    CategoryDescription = dc.CategoryDescription
                })
                .FirstOrDefaultAsync(m => m.CategoryID == id);

            if (deviceCategory == null)
            {
                return NotFound();
            }

            return View(deviceCategory);
        }

        // POST: DeviceCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CategoryID)
        {
            var deviceCategory = await _context.DeviceCategories.FindAsync(CategoryID);
            _context.DeviceCategories.Remove(deviceCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceCategoryExists(int id)
        {
            return _context.DeviceCategories.Any(e => e.CategoryID == id);
        }
    }

}
