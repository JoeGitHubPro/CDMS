using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.DAL.Data;
using System.DAL.Models;
using System.MVC.ViewModels;

namespace System.MVC.Controllers
{
    [Authorize]
    public class DeviceController : Controller
    {
        private readonly AppDbContext _context;

        public DeviceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Device
        public async Task<IActionResult> Index()
        {
            var devices = await _context.Devices
                            .Include(d => d.Category)
                            .Select(d => new DeviceViewModel
                            {
                                ID = d.DeviceID,
                                CategoryName = d.Category.CategoryName,
                                Name = d.DeviceName,
                                INFO = d.DeviceINFO
                            })
                            .ToListAsync();


            return View(devices);
        }

        // GET: Device/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                           .Include(d => d.Category)
                           .Select(d => new DeviceViewModel
                           {
                               ID = d.DeviceID,
                               CategoryName = d.Category.CategoryName,
                               Name = d.DeviceName,
                               INFO = d.DeviceINFO
                           })
                           .FirstOrDefaultAsync(m => m.ID == id);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Device/Create
        public IActionResult Create()
        {
            ViewData["DeviceCategory"] = new SelectList(_context.DeviceCategories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Device/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceViewModel deviceViewModel)
        {
            if (ModelState.IsValid)
            {
                var device = new Device
                {
                    DeviceID = deviceViewModel.ID,
                    DeviceCategory = deviceViewModel.Category,
                    DeviceName = deviceViewModel.Name,
                    DeviceINFO = deviceViewModel.INFO
                };

                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceCategory"] = new SelectList(_context.DeviceCategories, "CategoryID", "CategoryName", deviceViewModel.Category);
            return View(deviceViewModel);
        }

        // GET: Device/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Select(d => new DeviceViewModel
                {
                    ID = d.DeviceID,
                    Category = d.DeviceCategory,
                    Name = d.DeviceName,
                    INFO = d.DeviceINFO
                })
                .FirstOrDefaultAsync(m => m.ID == id);

            if (device == null)
            {
                return NotFound();
            }

            ViewData["DeviceCategory"] = new SelectList(_context.DeviceCategories, "CategoryID", "CategoryName", device.Category);
            return View(device);
        }

        // POST: Device/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DeviceViewModel deviceViewModel)
        {
            if (id != deviceViewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var device = await _context.Devices.FindAsync(id);
                    device.DeviceCategory = deviceViewModel.Category;
                    device.DeviceName = deviceViewModel.Name;
                    device.DeviceINFO = deviceViewModel.INFO;

                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(deviceViewModel.ID))
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
            ViewData["DeviceCategory"] = new SelectList(_context.DeviceCategories, "CategoryID", "CategoryName", deviceViewModel.Category);
            return View(deviceViewModel);
        }

        // GET: Device/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Select(d => new DeviceViewModel
                {
                    ID = d.DeviceID,
                    Category = d.DeviceCategory,
                    Name = d.DeviceName,
                    INFO = d.DeviceINFO
                })
                .FirstOrDefaultAsync(m => m.ID == id);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Device/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string DeviceID)
        {
            var device = await _context.Devices.FindAsync(DeviceID);
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(string id)
        {
            return _context.Devices.Any(e => e.DeviceID == id);
        }
    }


}
