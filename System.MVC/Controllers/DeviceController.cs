using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.DAL.Data;
using System.DAL.Models;
using System.MVC.ViewModels;

namespace System.MVC.Controllers
{
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
                .Join(
                    _context.DeviceCategories,
                    device => device.DeviceCategory,
                    category => category.CategoryID,
                    (device, category) => new DeviceViewModel
                    {
                        DeviceID = device.DeviceID,
                        DeviceCategory = device.DeviceCategory,
                        DeviceName = device.DeviceName,
                        DeviceSpecificationId = device.DeviceSpecification,
                        DeviceCategoryName = category.CategoryName
                    }
                ).ToListAsync();

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
                .Join(
                    _context.DeviceCategories,
                    d => d.DeviceCategory,
                    c => c.CategoryID,
                    (d, c) => new { Device = d, Category = c }
                )
                .Select(dc => new DeviceViewModel
                {
                    DeviceID = dc.Device.DeviceID,
                    DeviceCategory = dc.Device.DeviceCategory,
                    DeviceName = dc.Device.DeviceName,
                    DeviceSpecificationId = dc.Device.DeviceSpecification,
                    DeviceCategoryName = dc.Category.CategoryName
                })
                .FirstOrDefaultAsync(m => m.DeviceID == id);

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
                    DeviceID = deviceViewModel.DeviceID,
                    DeviceCategory = deviceViewModel.DeviceCategory,
                    DeviceName = deviceViewModel.DeviceName,
                    DeviceSpecification = deviceViewModel.DeviceSpecificationId
                };

                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceCategory"] = new SelectList(_context.DeviceCategories, "CategoryID", "CategoryName", deviceViewModel.DeviceCategory);
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
                    DeviceID = d.DeviceID,
                    DeviceCategory = d.DeviceCategory,
                    DeviceName = d.DeviceName,
                    DeviceSpecificationId = d.DeviceSpecification
                })
                .FirstOrDefaultAsync(m => m.DeviceID == id);

            if (device == null)
            {
                return NotFound();
            }

            ViewData["DeviceCategory"] = new SelectList(_context.DeviceCategories, "CategoryID", "CategoryName", device.DeviceCategory);
            return View(device);
        }

        // POST: Device/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DeviceViewModel deviceViewModel)
        {
            if (id != deviceViewModel.DeviceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var device = await _context.Devices.FindAsync(id);
                    device.DeviceCategory = deviceViewModel.DeviceCategory;
                    device.DeviceName = deviceViewModel.DeviceName;
                    device.DeviceSpecification = deviceViewModel.DeviceSpecificationId;

                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(deviceViewModel.DeviceID))
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
            ViewData["DeviceCategory"] = new SelectList(_context.DeviceCategories, "CategoryID", "CategoryName", deviceViewModel.DeviceCategory);
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
                .Join(
                    _context.DeviceCategories,
                    d => d.DeviceCategory,
                    c => c.CategoryID,
                    (d, c) => new { Device = d, Category = c }
                )
                .Select(dc => new DeviceViewModel
                {
                    DeviceID = dc.Device.DeviceID,
                    DeviceCategory = dc.Device.DeviceCategory,
                    DeviceName = dc.Device.DeviceName,
                    DeviceSpecificationId = dc.Device.DeviceSpecification,
                    DeviceCategoryName = dc.Category.CategoryName
                })
                .FirstOrDefaultAsync(m => m.DeviceID == id);

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
