using Microsoft.AspNetCore.Mvc;
using MoonRobotSimulation.Models;
using MoonRobotSimulation.Persistence.Interfaces;
using System.Collections.Generic;

namespace MoonRobotSimulation.Controllers
{
    [ApiController]
    [Route("api/robot-commands")]
    public class RobotCommandsController : ControllerBase
    {
        private readonly IRobotCommandDataAccess _robotCommandRepository;

        public RobotCommandsController(IRobotCommandDataAccess robotCommandRepository)
        {
            _robotCommandRepository = robotCommandRepository;
        }

        [HttpGet]
        public IEnumerable<RobotCommand> GetAll() => _robotCommandRepository.GetRobotCommands();

        [HttpGet("move")]
        public IEnumerable<RobotCommand> GetMoveCommands() => _robotCommandRepository.GetMoveCommands();

        [HttpGet("{id}")]
        public ActionResult<RobotCommand> GetById(int id)
        {
            var command = _robotCommandRepository.GetRobotCommandById(id);
            return command == null ? NotFound() : Ok(command);
        }

        [HttpPost]
        public ActionResult<RobotCommand> AddCommand([FromBody] RobotCommand command)
        {
            _robotCommandRepository.AddRobotCommand(command);
            return CreatedAtAction(nameof(GetById), new { id = command.Id }, command);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCommand(int id, [FromBody] RobotCommand command)
        {
            if (!_robotCommandRepository.UpdateRobotCommand(id, command)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCommand(int id)
        {
            if (!_robotCommandRepository.DeleteRobotCommand(id)) return NotFound();
            return NoContent();
        }
    }
}
