using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]
    public class DevicesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IDevicesRepository _devicesRepository;
        private readonly IZonesRepository _zonesRepository;

        public DevicesController(ICategoriesRepository categoriesRepository, IDevicesRepository devicesRepository, IZonesRepository zonesRepository)
        {
            _categoriesRepository = categoriesRepository;
            _devicesRepository = devicesRepository;
            _zonesRepository = zonesRepository;
        }

        // GET: Index() method that gets all Devices and displays them when the page is loaded
        public async Task<IActionResult> Index()
        {
            return View(_devicesRepository.GetAll());
        }

        // GET: Details() method that displays the details of a Device by id
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _devicesRepository.GetById(id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            // referencing both the zone and category repositories to also display them when a device is created
            ViewData["CategoryId"] = new SelectList(_categoriesRepository.GetAll(), "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_zonesRepository.GetAll(), "ZoneId", "ZoneName");
            return View();
        }

        // POST: Create() method to create a Device by generating a new id then adding it to the database
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            // generate a new unique id for a Device
            device.DeviceId = Guid.NewGuid();
            // add Device to database
            _devicesRepository.Add(device);
            return RedirectToAction(nameof(Index));
        }

        // GET: Edit() method that gets a Device by its unique id
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _devicesRepository.GetById(id);
            if (device == null)
            {
                return NotFound();
            }
            // the category and zone repositories have been reference again
            ViewData["CategoryId"] = new SelectList(_categoriesRepository.GetAll(), "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_zonesRepository.GetAll(), "ZoneId", "ZoneName", device.ZoneId);
            return View(device);
        }

        // POST: Edit() method that gets a Device by its id then updates any changes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }
            try
            {
                // update a Device and save the changes
                _devicesRepository.Update(device);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId))
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

        // GET: Delete() method that gets a Device by its id and delete it
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _devicesRepository.GetById(id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Delete() method that gets a Device by its id, then delete it by calling the Remove() method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            // Get the id of the device
            var device = _devicesRepository.GetById(id);
            // remove a category
            _devicesRepository.Remove(device);
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(Guid id)
        {
            // reference of method created in repository class
            return _devicesRepository.DeviceExists(id);
        }
    }
}
