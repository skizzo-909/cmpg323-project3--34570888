using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class ZonesRepository : GenericRepository<Zone>, IZonesRepository
    {
        public ZonesRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Zone GetMostRecentZone()
        {
            return _context.Zone.OrderByDescending(service => service.DateCreated).FirstOrDefault();
        }

        // A method to check if zone exists by using its id
        public bool ZoneExists(Guid id)
        {
            return _context.Zone.Any(zon => zon.ZoneId == id);
        }
    }
}
