using Database;
using EGrowAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EGrowAPIUnitTest.Tests.SensorDataTest
{
    public class PostSensorDataUnitTests
    {
        private readonly MySqlContext _context;

        public PostSensorDataUnitTests()
        {
            var context = new MySqlContext();
            _context = context;
        }

        #region Add New Sensor

        [Fact]
        public async void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new SensorDataController(_context);
            var sensor = new SensorData() { SensorDataGuid = Guid.NewGuid().ToString(), Timestamp = DateTime.Now, SoilTemperatureCelsius = 12.5, AmbientTemperatureCelsius = 10.2, UvIndex = 6, SolarRadiation = 220, LeafWetness = 5, AmbientHumidityPercentage = 33, SoilHumidityPercentage = 16, GrowthCm = 3 };

            //Act  
            var data = await controller.PostSensorData(sensor);

            //Assert  
            Assert.IsType<ActionResult<SensorData>>(data);
        }

        [Fact]
        public async void Task_Add_InvalidData_Return_BadRequest()
        {
            //Arrange  
            var controller = new SensorDataController(_context);
            var sensor = new SensorData() { SensorDataGuid = "569bc60f-c897-4562-a747-decfe03659de", Timestamp = DateTime.Now, SoilTemperatureCelsius = 12.5, AmbientTemperatureCelsius = 10.2, UvIndex = 6, SolarRadiation = 220, LeafWetness = 5, AmbientHumidityPercentage = 33, SoilHumidityPercentage = 16, GrowthCm = 3 };

            //Act  
            var data = await controller.PostSensorData(sensor);

            //Assert  
            Assert.IsType<ActionResult<SensorData>>(data);
        }

        #endregion
    }
}
