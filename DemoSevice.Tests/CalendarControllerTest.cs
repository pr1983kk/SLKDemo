using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoSevice;
using DemoSevice.Controllers;
using System.Net.Http;
using Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Routing;

namespace DemoSevice.Tests
{
    [TestClass]
    public class CalendarControllerTest
    {
        [TestMethod]
        public void Get_ReturnsDaysOfWeek()
        {
            // Arrange
            var controller = new CalendarController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Get();

            // Assert
            List<WeekDays> daysOfWeekList;
            Assert.IsTrue(response.TryGetContentValue<List<WeekDays>>(out daysOfWeekList));
            Assert.AreEqual(7, daysOfWeekList.Count);
        }

        [TestMethod]
        public void PostSetsLocationHeader()
        {
            // Arrange
            var controller = new CalendarController();

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:44352/api/Calendar/Weekday")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "products" } });

            // Act
            List<DateTime> dateList = new List<DateTime> { DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), DateTime.Now.AddDays(3),DateTime.Now.AddDays(4),DateTime.Now.AddDays(5),DateTime.Now.AddDays(6) };
            var weekdayslist =new CommonAPI.CalendarAPI().PostDaysOfWeek(dateList);
            var response = controller.Post(dateList);
            // Assert
            Assert.AreEqual(weekdayslist.GetType(), response.RequestMessage.Content.GetType());
        }
    }
}
