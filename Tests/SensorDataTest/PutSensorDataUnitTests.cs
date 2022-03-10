﻿using Database;
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
    public class PutSensorDataUnitTests
    {
        private readonly MySqlContext _context;

        public PutSensorDataUnitTests()
        {
            var context = new MySqlContext();
            _context = context;
        }

        #region Add New Sensor

        [Fact]
        public async void Task_Add_ValidData_Return_NoContentResult()
        {
            //Arrange  
            var controller = new SensorDataController(_context);
            string sensorId = "59e11482-a54f-4939-9dde-fe4e407b3f56";
            var sensor = new SensorData() { SensorDataGuid = "59e11482-a54f-4939-9dde-fe4e407b3f56", Timestamp = DateTime.Now, SoilTemperatureCelsius = 12.5, AmbientTemperatureCelsius = 10.2, UvIndex = 6, SolarRadiation = 220, LeafWetness = 5, AmbientHumidityPercentage = 33, SoilHumidityPercentage = 16, GrowthCm = 3 };

            //Act  
            var data = await controller.PutSensorData(sensorId, sensor);

            //Assert  
            Assert.IsType<NoContentResult>(data);
        }

        [Fact]
        public async void Task_Add_ValidData_Return_BadRequest()
        {
            //Arrange  
            var controller = new SensorDataController(_context);
            string sensorId = "59e11482-a54f-4939-9dde-fe4e407b3f56";
            var sensor = new SensorData() { Timestamp = DateTime.Now, SoilTemperatureCelsius = 12.5, AmbientTemperatureCelsius = 10.2, UvIndex = 6, SolarRadiation = 220, LeafWetness = 5, AmbientHumidityPercentage = 33, SoilHumidityPercentage = 16, GrowthCm = 3 };

            //Act  
            var data = await controller.PutSensorData(sensorId, sensor);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        #endregion
    }
}
