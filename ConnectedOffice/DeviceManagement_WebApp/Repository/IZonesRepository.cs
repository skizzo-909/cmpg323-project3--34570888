using DeviceManagement_WebApp.Models;
using System;

namespace DeviceManagement_WebApp.Repository
{
    public interface IZonesRepository : IGenericRepository<Zone>
    {
        Zone GetMostRecentZone();
        // imlementation of the ZoneExists() method
        bool ZoneExists(Guid id);
    }
}
