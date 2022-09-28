using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public class DevicesRepository : GenericRepository<Device>, IDevicesRepository
    {
        public DevicesRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        // A method to get all the devices
        public IEnumerable<Device> GetDevices()
        {
            var devices = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return devices.ToList();
        }

        // A method to get a device by its id
        public Device GetDeviceById(Guid? id)
        {
            return _context.Device.Where(m => m.DeviceId == id).Include(d => d.Category).Include(d => d.Zone).FirstOrDefault();
        }

        public Device GetMostRecentDevice()
        {
            return _context.Device.OrderByDescending(dev => dev.DateCreated).FirstOrDefault();
        }

        // A method to check if Device exists by using its id
        public bool DeviceExists(Guid id)
        {
            return _context.Device.Any(dev => dev.DeviceId == id);
        }
    }
}
