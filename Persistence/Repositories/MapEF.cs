using MoonRobotSimulation.Models;
using MoonRobotSimulation.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MoonRobotSimulation.Persistence.Repositories
{
    public class MapEF : IMapDataAccess
    {
        private readonly RobotContext _context;

        public MapEF(RobotContext context)
        {
            _context = context;
        }

        public List<Map> GetMaps()
        {
            return _context.Maps.ToList();
        }

        public Map? GetMapById(int id)
        {
            return _context.Maps.Find(id);
        }

        public void AddMap(Map map)
        {
            _context.Maps.Add(map);
            _context.SaveChanges();
        }

        public bool UpdateMap(int id, Map map)
        {
            var existingMap = _context.Maps.Find(id);
            if (existingMap == null) return false;

            _context.Entry(existingMap).CurrentValues.SetValues(map);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteMap(int id)
        {
            var map = _context.Maps.Find(id);
            if (map == null) return false;

            _context.Maps.Remove(map);
            _context.SaveChanges();
            return true;
        }
    }
}
