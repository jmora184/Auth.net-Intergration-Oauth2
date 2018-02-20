using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication19.Controllers;
using System.Web.Mvc;

namespace UnitTestW19
{
    [TestClass]
    public class UnitTest1
    {
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
    }
}
