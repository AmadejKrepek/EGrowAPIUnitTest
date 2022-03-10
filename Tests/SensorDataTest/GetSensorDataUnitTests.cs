using Database;
using EGrowAPI.Controllers;
using FluentAssertions;
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
    public class GetSensorDataUnitTests
    {
        private readonly MySqlContext _context;

        public GetSensorDataUnitTests()
        {
            var context = new MySqlContext();
            _context = context;
        }

        #region Get One By Id

        [Fact]
        public async void Task_GetSensorById_Return_ActionResult()
        {
            //Arrange  
            var controller = new SensorDataController(_context);
            var sensorId = "03a1b821-6778-48c8-8268-57eb07bc935c";

            //Act  
            var data = await controller.GetSensorData(sensorId);

            //Assert  
            Assert.IsType<ActionResult<SensorData>>(data);
        }

        [Fact]
        public async void Task_GetSensorById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new SensorDataController(_context);
            var sensorId = "napacno";

            //Act  
            var data = await controller.GetSensorData(sensorId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetSensorById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new SensorDataController(_context);
            var sensorId = "napacno";
            sensorId = null;

            //Act  
            var data = await controller.GetSensorData(sensorId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async void Task_GetSensorById_Return_MatchResult()
        {
            //Arrange  
            var controller = new SensorDataController(_context);
            var sensorId = "03a1b821-6778-48c8-8268-57eb07bc935c";

            //Act  
            var data = await controller.GetSensorData(sensorId);

            //Assert  
            Assert.IsType<ActionResult<SensorData>>(data);

            var okResult = data.Should().BeOfType<ActionResult<SensorData>>().Subject;
            var sensor = okResult.Value.Should().BeAssignableTo<SensorData>().Subject;

            Assert.Equal("03a1b821-6778-48c8-8268-57eb07bc935c", sensor.SensorDataGuid);
            Assert.Equal(DateTime.Parse("2021-03-04 02:45:58"), sensor.Timestamp);
            Assert.Equal(10.9, sensor.SoilTemperatureCelsius);
            Assert.Equal(43, sensor.AmbientTemperatureCelsius);
            Assert.Equal(1, sensor.UvIndex);
            Assert.Equal(1322, sensor.SolarRadiation);
            Assert.Equal(27, sensor.LeafWetness);
        }

        #endregion

        #region Get All 

        [Fact]
        public async void Task_GetSensorData_Return_ActionResult()
        {
            //Arrange  
            var controller = new SensorDataController(_context);

            //Act  
            var data = await controller.GetSensorData();

            //Assert  
            Assert.IsType<ActionResult<IEnumerable<SensorData>>>(data);
        }

        [Fact]
        public async void Task_GetSensorData_Return_BadRequest()
        {
            //Arrange  
            var controller = new SensorDataController(_context);

            //Act  
            var data = await controller.GetSensorData();
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async void Task_GetSensorData_Return_MatchResult()
        {
            //Arrange  
            var controller = new SensorDataController(_context);

            //Act  
            var data = await controller.GetSensorData();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<SensorData>>>(data);

            var okResult = data.Should().BeOfType<ActionResult<IEnumerable<SensorData>>>().Subject;
            var sensor = okResult.Value.Should().BeAssignableTo<IEnumerable<SensorData>>().Subject.ToList();

            Assert.Equal("03a1b821-6778-48c8-8268-57eb07bc935c", sensor[0].SensorDataGuid);
            Assert.Equal(DateTime.Parse("2021-03-04 02:45:58"), sensor[0].Timestamp);
            Assert.Equal(10.9, sensor[0].SoilTemperatureCelsius);
            Assert.Equal(43, sensor[0].AmbientTemperatureCelsius);
            Assert.Equal(1, sensor[0].UvIndex);
            Assert.Equal(1322, sensor[0].SolarRadiation);
            Assert.Equal(27, sensor[0].LeafWetness);

            Assert.Equal("06cd8921-e885-47c3-acda-1b60b3508930", sensor[1].SensorDataGuid);
            Assert.Equal(DateTime.Parse("2021-08-25 02:23:03"), sensor[1].Timestamp);
            Assert.Equal(19.7, sensor[1].SoilTemperatureCelsius);
            Assert.Equal(11.7, sensor[1].AmbientTemperatureCelsius);
            Assert.Equal(4, sensor[1].UvIndex);
            Assert.Equal(19, sensor[1].SolarRadiation);
            Assert.Equal(14, sensor[1].LeafWetness);
        }

        #endregion

    }
}
