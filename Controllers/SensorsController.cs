using CapteurManagement.Data;
using CapteurManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapteurManagement.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SensorsController : ControllerBase
    {

        private readonly SensorContext _context;
        private readonly IMemoryCache _cache;

        public SensorsController(SensorContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetSensors()
        {
            var sensors = await _context.Sensors.ToListAsync();
            if (sensors == null || !sensors.Any())
            {
                return NotFound(new { message = "No sensors found." });
            }
            return Ok(new { message = "Sensors retrieved successfully.", data = sensors });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensor(int id)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null)
            {
                return NotFound(new { message = $"Sensor with ID {id} not found." });
            }
            return Ok(new { message = "Sensor retrieved successfully.", data = sensor });
        }
        //implemening the versionning to have new version of same action
        //for the version 1

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetSensorsV1()
        {
            var sensors = await _context.Sensors.ToListAsync();
            if (sensors == null || !sensors.Any())
            {
                return NotFound(new { message = "No sensors found in version 1." });
            }
            return Ok(new { message = "Sensors retrieved successfully (v1).", data = sensors });
        }

        //  //for the version 2

        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetSensorsV2()
        {
            var sensors = await _context.Sensors.ToListAsync();
            // New logic for version 2.0
            if (sensors == null || !sensors.Any())
            {
                return NotFound(new { message = "No sensors found in version 2." });
            }
            return Ok(new { message = "Sensors retrieved successfully (v2).", data = sensors });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSensor(Sensor sensor)
        {
            if (sensor == null)
            {
                return BadRequest(new { message = "Invalid sensor data." });
            }

            sensor.CreatedAt = DateTime.UtcNow;
            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSensor), new { id = sensor.Id }, new { message = "Sensor created successfully.", data = sensor });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor(int id, Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return BadRequest(new { message = "Sensor ID mismatch." });
            }

            var existingSensor = await _context.Sensors.FindAsync(id);
            if (existingSensor == null)
            {
                return NotFound(new { message = $"Sensor with ID {id} not found." });
            }

            existingSensor.Name = sensor.Name;
            existingSensor.IsActive = sensor.IsActive;  // Assuming Sensor has these properties.
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null)
            {
                return NotFound(new { message = $"Sensor with ID {id} not found." });
            }

            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Sensor with ID {id} successfully deleted." });
        }
    }
}
