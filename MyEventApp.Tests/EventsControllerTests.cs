using Microsoft.AspNetCore.Mvc;
using Moq;
using MyEventApp.Api.Controllers;
using MyEventApp.Core.Models;
using MyEventApp.Core.Services;


namespace MyEventApp.Tests
{
    [TestFixture]
    public class EventsControllerTests
    {
        [Test]
        public async Task Get_WithValidDays_ReturnsOkWithEvents()
        {
            // Arrange
            var mockService = new Mock<IEventService>();
            var expectedEvents = new List<Event>
        {
            new Event { Id = Guid.NewGuid(), Name = "Event1", StartsOn = DateTime.UtcNow.AddDays(1) },
            new Event { Id = Guid.NewGuid(), Name = "Event2", StartsOn = DateTime.UtcNow.AddDays(2) }
        };
            mockService.Setup(s => s.GetUpcomingEventsAsync(30)).ReturnsAsync(expectedEvents);
            var controller = new EventsController(mockService.Object);

            // Act
            var result = await controller.Get(30);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedEvents, okResult.Value);
        }
    }

}




