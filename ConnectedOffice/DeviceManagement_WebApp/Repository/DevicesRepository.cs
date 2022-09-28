using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class DevicesRepository : GenericRepository<Device>, IDevicesRepository
    {
        public DevicesRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Device GetMostRecentDevice()
        {
            return _context.Device.OrderByDescending(service => service.DateCreated).FirstOrDefault();
        }

        // A method to check if Device exists by using its id
        public bool DeviceExists(Guid id)
        {
            return _context.Device.Any(dev => dev.DeviceId == id);
        }
    }
}
