using MoonRobotSimulation.Models;
using MoonRobotSimulation.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MoonRobotSimulation.Persistence.Repositories
{
    public class RobotCommandEF : IRobotCommandDataAccess
    {
        private readonly RobotContext _context;

        public RobotCommandEF(RobotContext context)
        {
            _context = context;
        }

        public List<RobotCommand> GetRobotCommands()
        {
            return _context.RobotCommands.ToList();
        }

        public RobotCommand? GetRobotCommandById(int id)
        {
            return _context.RobotCommands.Find(id);
        }

        public void AddRobotCommand(RobotCommand command)
        {
            _context.RobotCommands.Add(command);
            _context.SaveChanges();
        }

        public bool UpdateRobotCommand(int id, RobotCommand command)
        {
            var existingCommand = _context.RobotCommands.Find(id);
            if (existingCommand == null) return false;

            _context.Entry(existingCommand).CurrentValues.SetValues(command);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteRobotCommand(int id)
        {
            var command = _context.RobotCommands.Find(id);
            if (command == null) return false;

            _context.RobotCommands.Remove(command);
            _context.SaveChanges();
            return true;
        }

        public List<RobotCommand> GetMoveCommands()
        {
            return _context.RobotCommands.Where(c => c.IsMoveCommand).ToList();
        }
    }
}
