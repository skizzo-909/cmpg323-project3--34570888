using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(ConnectedOfficeContext context) : base(context){}

        public Category GetMostRecentCategory()
        {
            return _context.Category.OrderByDescending(service => service.DateCreated).FirstOrDefault();
        }

        // Method to check if category exists by using its id
        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(cat => cat.CategoryId == id);
        }
    }
}
