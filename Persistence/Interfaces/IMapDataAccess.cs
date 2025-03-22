using MoonRobotSimulation.Models;
using System.Collections.Generic;

namespace MoonRobotSimulation.Persistence.Interfaces
{
    public interface IMapDataAccess
    {
        List<Map> GetMaps();
        Map? GetMapById(int id);
        void AddMap(Map map);
        bool UpdateMap(int id, Map map);
        bool DeleteMap(int id);
    }
}
