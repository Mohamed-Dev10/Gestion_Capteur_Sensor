using CapteurManagement.Data;
using CapteurManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CapteurManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SensorTestController : ControllerBase
    {
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly SensorsController _controller;

        public SensorTestController()
        {
            _mockMemoryCache = new Mock<IMemoryCache>();


            var mockContext = new Mock<SensorContext>();

            _controller = new SensorsController(mockContext.Object, _mockMemoryCache.Object);
        }

        [Fact]
        public async Task GetSensors_ReturnsOkResult_WhenCached()
        {

            var cacheKey = "sensorsList";
            var sensors = new List<Sensor>
        {
            new Sensor { Id = 4, Name = "Temperature" },
            new Sensor { Id = 5, Name = "Humidity" }
        };


            object cachedData = sensors;
            _mockMemoryCache.Setup(m => m.TryGetValue(cacheKey, out cachedData)).Returns(true);

            // Act
            var result = await _controller.GetSensors();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Sensor>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetSensors_ReturnsFromDatabase_WhenNotCached()
        {
            // Arrange
            var cacheKey = "sensorsList";
            List<Sensor> sensors = null;

            // Simulate cache miss
            _mockMemoryCache.Setup(m => m.TryGetValue(cacheKey, out sensors)).Returns(false);

            // Simulate fetching data from the database
            var mockSensors = new List<Sensor>
        {
            new Sensor { Id = 4, Name = "Temperature" }
        };


            var result = await _controller.GetSensors();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Sensor>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task DeleteSensor_RemovesCache_WhenDeleted()
        {
            // Arrange
            var sensorId = 4;
            var sensor = new Sensor { Id = sensorId, Name = "Temperature" };

            // Simulate cache removal
            _mockMemoryCache.Setup(m => m.Remove(It.IsAny<string>()));

            // Simulate sensor exists in database

            var result = await _controller.DeleteSensor(sensorId);

            // Assert
            _mockMemoryCache.Verify(m => m.Remove("sensorsList"), Times.Once);

            // Check response (NoContent when deleted)
            var noContentResult = Assert.IsType<OkObjectResult>(result);
            var returnMessage = noContentResult.Value as dynamic;
            Assert.Equal($"Sensor with ID {sensorId} successfully deleted.", returnMessage.message);
        }
    }
}
