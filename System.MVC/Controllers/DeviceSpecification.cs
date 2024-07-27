using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.DAL.Data;
using System.DAL.Models;
using System.MVC.ViewModels;

namespace System.MVC.Controllers
{
    public class DeviceSpecificationsController : Controller
    {
        private readonly AppDbContext _context;

        public DeviceSpecificationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DeviceSpecifications
        public async Task<IActionResult> Index()
        {
            var specifications = await _context.DeviceSpecifications
                .Select(s => new DeviceSpecificationsViewModel
                {
                    SpecificationID = s.SpecificationID,
                    ModelName = s.ModelName,
                    INFO = s.INFO
                }).ToListAsync();

            return View(specifications);
        }

        // GET: DeviceSpecifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.DeviceSpecifications
                .Select(s => new DeviceSpecificationsViewModel
                {
                    SpecificationID = s.SpecificationID,
                    ModelName = s.ModelName,
                    INFO = s.INFO
                }).FirstOrDefaultAsync(m => m.SpecificationID == id);

            if (specification == null)
            {
                return NotFound();
            }

            return View(specification);
        }

        // GET: DeviceSpecifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeviceSpecifications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceSpecificationsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var specification = new DeviceSpecifications
                {
                    ModelName = viewModel.ModelName,
                    INFO = viewModel.INFO
                };

                _context.Add(specification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: DeviceSpecifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.DeviceSpecifications.FindAsync(id);
            if (specification == null)
            {
                return NotFound();
            }

            var viewModel = new DeviceSpecificationsViewModel
            {
                SpecificationID = specification.SpecificationID,
                ModelName = specification.ModelName,
                INFO = specification.INFO
            };

            return View(viewModel);
        }

        // POST: DeviceSpecifications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeviceSpecificationsViewModel viewModel)
        {
            if (id != viewModel.SpecificationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var specification = await _context.DeviceSpecifications.FindAsync(id);
                    specification.ModelName = viewModel.ModelName;
                    specification.INFO = viewModel.INFO;

                    _context.Update(specification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceSpecificationsExists(viewModel.SpecificationID))
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

        // GET: DeviceSpecifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.DeviceSpecifications
                .Select(s => new DeviceSpecificationsViewModel
                {
                    SpecificationID = s.SpecificationID,
                    ModelName = s.ModelName,
                    INFO = s.INFO
                }).FirstOrDefaultAsync(m => m.SpecificationID == id);

            if (specification == null)
            {
                return NotFound();
            }

            return View(specification);
        }

        // POST: DeviceSpecifications/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int SpecificationID)
        {
            var specification = await _context.DeviceSpecifications.FindAsync(SpecificationID);
            _context.DeviceSpecifications.Remove(specification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceSpecificationsExists(int id)
        {
            return _context.DeviceSpecifications.Any(e => e.SpecificationID == id);
        }
    }


}
