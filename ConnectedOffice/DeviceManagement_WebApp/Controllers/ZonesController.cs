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

namespace DeviceManagement_WebApp.Controllers
{
    public class ZonesController : Controller
    {
        private readonly ConnectedOfficeContext _context;
        private readonly IZonesRepository _zonesRepository;

        public ZonesController(ConnectedOfficeContext context, IZonesRepository zonesRepository)
        {
            _context = context;
            _zonesRepository = zonesRepository;
        }

        // GET: Index() method that gets all Zones and displays them when the page is loaded
        public async Task<IActionResult> Index()
        {
            return View(_zonesRepository.GetAll());
        }

        // GET: Details() method that displays the details of a Zone by id
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zonesRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create() method to create a Zone by generating a new id then adding it to the database
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            // generate a new id for the category
            zone.ZoneId = Guid.NewGuid();
            // add category to database
            _zonesRepository.Add(zone);

            return RedirectToAction(nameof(Index));
        }

        // GET: Edit() method that gets a Zone by its unique id
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zonesRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }
            return View(zone);
        }

        // POST: Edit() method that gets a Zone by its id then updates any changes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return NotFound();
            }

            try
            {
                // update a Zone and save the changes
                _zonesRepository.Update(zone);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.ZoneId))
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

        // GET: Delete() method that gets a Zone by its id and delete it
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zonesRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST: Delete() method that gets a Zone by its id, then delete it by calling the Remove() method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zone = _zonesRepository.GetById(id);
            _zonesRepository.Remove(zone);
            return RedirectToAction(nameof(Index));
        }

        private bool ZoneExists(Guid id)
        {
            // reference of method created in repository class
            return _zonesRepository.ZoneExists(id);
        }
    }
}
