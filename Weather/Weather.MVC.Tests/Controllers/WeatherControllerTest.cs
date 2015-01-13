using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Weather.MVC;
using Weather.MVC.Controllers;
using Weather.Domain.Entities;
using Weather.Domain.Service;

namespace Weather.MVC.Tests.Controllers
{
    [TestClass]
    public class WeatherControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            WeatherController controller = new WeatherController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Forecast()
        {
            // Arrange
            WeatherController controller = new WeatherController();

            // Act
            var interger = 55;
            ViewResult result = controller.Forecast(interger) as ViewResult;

            // Assert     
            Assert.IsNotNull(result);
        }

      
        

        

        
    }
}
