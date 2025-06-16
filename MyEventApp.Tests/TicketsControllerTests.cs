using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyEventApp.Api.Controllers;
using MyEventApp.Core.Models;
using MyEventApp.Core.Services;

namespace MyEventApp.Tests
{
    [TestFixture]
    public class TicketsControllerTests
    {
        [Test]
        public async Task GetForEvent_WithValidId_ReturnsOkWithTickets()
        {
            // Arrange
            var mockService = new Mock<ITicketService>();
            var eventId = Guid.NewGuid();
            var expectedTickets = new List<TicketSale>
        {
            new TicketSale { Id = Guid.NewGuid(), EventId = eventId, PriceInCents = 1000 },
            new TicketSale { Id = Guid.NewGuid(), EventId = eventId, PriceInCents = 1500 }
        };
            mockService.Setup(s => s.GetTicketsForEventAsync(eventId)).ReturnsAsync(expectedTickets);
            var mockLogger = new Mock<ILogger<TicketsController>>();
            var controller = new TicketsController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.GetForEvent(eventId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedTickets, okResult.Value);
        }

        [Test]
        public async Task TopByQty_ReturnsOkWithTop5ByQuantity()
        {
            // Arrange
            var mockService = new Mock<ITicketService>();
            var expectedTop5 = new List<EventSales>
        {
            new EventSales { EventId = Guid.NewGuid(), TotalQuantity = 100, TotalRevenue = 1000m }
        };
            mockService.Setup(s => s.GetTop5ByQuantityAsync()).ReturnsAsync(expectedTop5);
            var mockLogger = new Mock<ILogger<TicketsController>>();
            var controller = new TicketsController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.TopByQty();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedTop5, okResult.Value);
        }

        [Test]
        public async Task TopByRev_ReturnsOkWithTop5ByRevenue()
        {
            // Arrange
            var mockService = new Mock<ITicketService>();
            var expectedTop5 = new List<EventSales>
        {
            new EventSales { EventId = Guid.NewGuid(), TotalQuantity = 50, TotalRevenue = 2000m }
        };
            mockService.Setup(s => s.GetTop5ByRevenueAsync()).ReturnsAsync(expectedTop5);
            var mockLogger = new Mock<ILogger<TicketsController>>();
            var controller = new TicketsController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.TopByRev();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedTop5, okResult.Value);
        }
    }
}
