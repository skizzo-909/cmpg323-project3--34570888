using DeviceManagement_WebApp.Models;
using System;
using System.Collections.Generic;

namespace DeviceManagement_WebApp.Repository
{
    public interface IDevicesRepository : IGenericRepository<Device>
    {
        IEnumerable<Device> GetDevices();
        Device GetDeviceById(Guid? id);
        Device GetMostRecentDevice();
        // implementation of the DeviceExists() method
        bool DeviceExists(Guid id);
    }
}
