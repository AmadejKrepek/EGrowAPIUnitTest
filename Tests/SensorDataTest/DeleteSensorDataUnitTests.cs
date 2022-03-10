using Database;
using EGrowAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EGrowAPIUnitTest.Tests.SensorDataTest
{
    public class DeleteSensorDataUnitTests
    {
        private readonly MySqlContext _context;

        public DeleteSensorDataUnitTests()
        {
            var context = new MySqlContext();
            _context = context;
        }

        #region Delete Sensor By Id

        [Fact]
        public async void Task_Add_ValidData_Return_NoContentResult()
        {
            //Arrange  
            var controller = new SensorDataController(_context);
            string sensorId = "59e11482-a54f-4939-9dde-fe4e407b3f56";

            //Act  
            var data = await controller.DeleteSensorData(sensorId);

            //Assert  
            Assert.IsType<NoContentResult>(data);
        }

        [Fact]
        public async void Task_Add_ValidData_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new SensorDataController(_context);
            string sensorId = "asdqwsasdas";

            //Act  
            var data = await controller.DeleteSensorData(sensorId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion
    }
}
