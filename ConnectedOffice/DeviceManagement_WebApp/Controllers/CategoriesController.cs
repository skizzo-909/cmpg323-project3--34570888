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
    public class CategoriesController : Controller
    {
        private readonly ConnectedOfficeContext _context;
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ConnectedOfficeContext context, ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
            _context = context;
        }

        // GET: Method that gets all Categories and displays them
        public async Task<IActionResult> Index()
        {
            return View(_categoriesRepository.GetAll());
        }

        // GET: Method that displays the details of categories by id
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriesRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Method to create a category
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            // generate a new id for the category
            category.CategoryId = Guid.NewGuid();
            // add category to database
            _categoriesRepository.Add(category);
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriesRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Method to edit a category
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            try
            {
                // update a category and save the changes
                _categoriesRepository.Update(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
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

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriesRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Method that deletes a category by its id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            // get the id of a category
            var category = _categoriesRepository.GetById(id);
            _categoriesRepository.Remove(category);
            return RedirectToAction(nameof(Index));
        }

        // Method to check if category exists by using its id
        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
