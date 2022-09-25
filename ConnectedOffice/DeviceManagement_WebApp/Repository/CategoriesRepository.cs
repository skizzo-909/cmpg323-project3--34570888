﻿using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class ProductRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public ProductRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Category GetMostRecentCategory()
        {
            return _context.Category.OrderByDescending(service => service.DateCreated).FirstOrDefault();
        }
    }
}