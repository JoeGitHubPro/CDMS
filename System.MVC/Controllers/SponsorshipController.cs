using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.DAL.Data;
using System.DAL.Models;
using System.MVC.ViewModels;

namespace System.MVC.Controllers
{
    [Authorize]
    public class SponsorshipController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public SponsorshipController(AppDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Sponsorship
        public async Task<IActionResult> Index()
        {
            var sponsorships = await _context.Sponsorships
                .Select(s => new SponsorshipViewModel
                {
                    SponsorshipID = s.SponsorshipID,
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    DeviceId = s.DeviceId,
                    DeviceName = s.Device.DeviceName,
                    Date = s.Date,
                    LocationId = s.LocationId,
                    LocationName = s.Location.LocationName,
                    Note = s.Note
                }).ToListAsync();

            return View(sponsorships);
        }

        // GET: Sponsorship/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsorship = await _context.Sponsorships
                .Select(s => new SponsorshipViewModel
                {
                    SponsorshipID = s.SponsorshipID,
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    DeviceId = s.DeviceId,
                    DeviceName = s.Device.DeviceName,
                    Date = s.Date,
                    LocationId = s.LocationId,
                    LocationName = s.Location.LocationName,
                    Note = s.Note
                }).FirstOrDefaultAsync(m => m.SponsorshipID == id);

            if (sponsorship == null)
            {
                return NotFound();
            }

            return View(sponsorship);
        }

        public IActionResult Create()
        {
            ViewData["Users"] = new SelectList(_context.Users.ToList(), "Id", "UserName");
            ViewData["Devices"] = new SelectList(_context.Devices.ToList(), "DeviceID", "DeviceName");
            ViewData["Locations"] = new SelectList(_context.Locations.ToList(), "LocationID", "LocationName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SponsorshipViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (DeviceExists(viewModel.DeviceId))
                {
                    ModelState.AddModelError("DeviceId", "Repeated Device");
                    ViewData["Users"] = new SelectList(_context.Users.ToList(), "Id", "UserName");
                    ViewData["Devices"] = new SelectList(_context.Devices.ToList(), "DeviceID", "DeviceName");
                    ViewData["Locations"] = new SelectList(_context.Locations.ToList(), "LocationID", "LocationName");
                    return View(viewModel);

                }

                var sponsorship = new Sponsorship
                {
                    UserId = viewModel.UserId,
                    DeviceId = viewModel.DeviceId,
                    Date = viewModel.Date,
                    LocationId = viewModel.LocationId,
                    Note = viewModel.Note
                };

                _context.Add(sponsorship);
                await _context.SaveChangesAsync();
                await Notify(sponsorship.SponsorshipID);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Users"] = new SelectList(_context.Users.ToList(), "Id", "UserName");
            ViewData["Devices"] = new SelectList(_context.Devices.ToList(), "DeviceID", "DeviceName");
            ViewData["Locations"] = new SelectList(_context.Locations.ToList(), "LocationID", "LocationName");
            return View(viewModel);
        }

        // GET: Sponsorship/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsorship = await _context.Sponsorships
                .Include(a => a.User)
                .Include(a => a.Location)
                .Include(a => a.Device)
                .SingleOrDefaultAsync(a => a.SponsorshipID == id);

            if (sponsorship == null)
            {
                return NotFound();
            }

            var viewModel = new SponsorshipViewModel
            {
                SponsorshipID = sponsorship.SponsorshipID,
                UserId = sponsorship.UserId,
                UserName = sponsorship.User.UserName,
                DeviceId = sponsorship.DeviceId,
                DeviceName = sponsorship.Device.DeviceName,
                Date = sponsorship.Date,
                LocationId = sponsorship.LocationId,
                LocationName = sponsorship.Location.LocationName,
                Note = sponsorship.Note
            };

            ViewData["Users"] = new SelectList(_context.Users.ToList(), "Id", "UserName");
            ViewData["Devices"] = new SelectList(_context.Devices.ToList(), "DeviceID", "DeviceName");
            ViewData["Locations"] = new SelectList(_context.Locations.ToList(), "LocationID", "LocationName");
            return View(viewModel);
        }

        // POST: Sponsorship/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SponsorshipViewModel viewModel)
        {
            if (id != viewModel.SponsorshipID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (DeviceExists(viewModel.DeviceId))
                {
                    ModelState.AddModelError("DeviceId", "Repeated Device");
                    ViewData["Users"] = new SelectList(_context.Users.ToList(), "Id", "UserName");
                    ViewData["Devices"] = new SelectList(_context.Devices.ToList(), "DeviceID", "DeviceName");
                    ViewData["Locations"] = new SelectList(_context.Locations.ToList(), "LocationID", "LocationName");
                    return View(viewModel);

                }
                try
                {
                    var sponsorship = await _context.Sponsorships.FindAsync(id);
                    sponsorship.UserId = viewModel.UserId;
                    sponsorship.DeviceId = viewModel.DeviceId;
                    sponsorship.Date = viewModel.Date;
                    sponsorship.LocationId = viewModel.LocationId;
                    sponsorship.Note = viewModel.Note;

                    _context.Update(sponsorship);
                    await _context.SaveChangesAsync();
                    await Notify(sponsorship.SponsorshipID);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorshipExists(viewModel.SponsorshipID))
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

            ViewData["Users"] = new SelectList(_context.Users.ToList(), "Id", "UserName");
            ViewData["Devices"] = new SelectList(_context.Devices.ToList(), "DeviceID", "DeviceName");
            ViewData["Locations"] = new SelectList(_context.Locations.ToList(), "LocationID", "LocationName");
            return View(viewModel);
        }

        // GET: Sponsorship/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsorship = await _context.Sponsorships
                .Select(s => new SponsorshipViewModel
                {
                    SponsorshipID = s.SponsorshipID,
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    DeviceId = s.DeviceId,
                    DeviceName = s.Device.DeviceName,
                    Date = s.Date,
                    LocationId = s.LocationId,
                    LocationName = s.Location.LocationName,
                    Note = s.Note
                }).FirstOrDefaultAsync(m => m.SponsorshipID == id);

            if (sponsorship == null)
            {
                return NotFound();
            }

            return View(sponsorship);
        }

        // POST: Sponsorship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sponsorship = await _context.Sponsorships.FindAsync(id);
            _context.Sponsorships.Remove(sponsorship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task Notify(int sponsorshipId)
        {

            var sponsorship = await _context.Sponsorships.Include(a => a.User).Include(a => a.Device).Include(a => a.Location).SingleOrDefaultAsync(s => s.SponsorshipID == sponsorshipId);
            try
            {
                await _emailSender.SendEmailAsync(
                sponsorship.User.Email,
                "Sponsorship Notification",
                $"Dear {sponsorship.User.UserName} ,\n A new device has been added to your custody \n device no. {sponsorship.DeviceId} \n device name {sponsorship.Device.DeviceName} \n at location {sponsorship.Location.LocationName} \n at {sponsorship.Date} \n {sponsorship.Note}");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
        private bool DeviceExists(string deviceId)
        {
            return _context.Sponsorships.Any(a => a.DeviceId == deviceId);
        }
        private bool SponsorshipExists(int id)
        {
            return _context.Sponsorships.Any(e => e.SponsorshipID == id);
        }
    }

}
