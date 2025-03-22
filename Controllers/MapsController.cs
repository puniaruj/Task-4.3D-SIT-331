using Microsoft.AspNetCore.Mvc;
using MoonRobotSimulation.Models;
using MoonRobotSimulation.Persistence.Interfaces;
using System.Collections.Generic;

namespace MoonRobotSimulation.Controllers
{
    [ApiController]
    [Route("api/maps")]
    public class MapsController : ControllerBase
    {
        private readonly IMapDataAccess _mapRepository;

        public MapsController(IMapDataAccess mapRepository)
        {
            _mapRepository = mapRepository;
        }

        [HttpGet]
        public IEnumerable<Map> GetAll() => _mapRepository.GetMaps();

        [HttpGet("{id}")]
        public ActionResult<Map> GetById(int id)
        {
            var map = _mapRepository.GetMapById(id);
            return map == null ? NotFound() : Ok(map);
        }

        [HttpPost]
        public ActionResult<Map> AddMap([FromBody] Map map)
        {
            _mapRepository.AddMap(map);
            return CreatedAtAction(nameof(GetById), new { id = map.Id }, map);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMap(int id, [FromBody] Map map)
        {
            if (!_mapRepository.UpdateMap(id, map)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMap(int id)
        {
            if (!_mapRepository.DeleteMap(id)) return NotFound();
            return NoContent();
        }
    }
}
