using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication19.Controllers;
using System.Web.Mvc;

namespace UnitTestW19
{
    [TestClass]
    public class UnitTest1
    {
        public bool fdss;
        [TestMethod]
        public void Index1()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Index1() as ViewResult;

            // Assert
            Assert.AreEqual("Index1", result.ViewName);
        }
        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
