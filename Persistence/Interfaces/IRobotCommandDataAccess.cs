using MoonRobotSimulation.Models;
using System.Collections.Generic;

namespace MoonRobotSimulation.Persistence.Interfaces
{
    public interface IRobotCommandDataAccess
    {
        List<RobotCommand> GetRobotCommands();
        RobotCommand? GetRobotCommandById(int id);
        void AddRobotCommand(RobotCommand command);
        bool UpdateRobotCommand(int id, RobotCommand command);
        bool DeleteRobotCommand(int id);
        List<RobotCommand> GetMoveCommands();
    }
}
