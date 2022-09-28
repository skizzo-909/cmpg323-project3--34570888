using DeviceManagement_WebApp.Models;
using System;

namespace DeviceManagement_WebApp.Repository
{
    public interface IDevicesRepository : IGenericRepository<Device>
    {
        Device GetMostRecentDevice();
        // implementation of the DeviceExists() method
        bool DeviceExists(Guid id);
    }
}
