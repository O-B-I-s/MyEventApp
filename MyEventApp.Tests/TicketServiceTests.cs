using Moq;
using MyEventApp.Api.Services;
using MyEventApp.Core.Models;
using MyEventApp.Data.Repositories;

namespace MyEventApp.Tests
{

    [TestFixture]
    public class TicketServiceTests
    {
        [Test]
        public async Task GetTicketsForEventAsync_CallsRepositoryWithCorrectEventId()
        {
            // Arrange
            var mockRepo = new Mock<ITicketSaleRepository>();
            var eventId = Guid.NewGuid();
            var expectedTickets = new List<TicketSale>
        {
            new TicketSale { Id = Guid.NewGuid(), EventId = eventId, PriceInCents = 1000 }
        };
            mockRepo.Setup(r => r.GetByEventAsync(eventId)).ReturnsAsync(expectedTickets);
            var service = new TicketService(mockRepo.Object);

            // Act
            var result = await service.GetTicketsForEventAsync(eventId);

            // Assert
            Assert.AreEqual(expectedTickets, result);
            mockRepo.Verify(r => r.GetByEventAsync(eventId), Times.Once);
        }

        [Test]
        public async Task GetTop5ByQuantityAsync_CallsRepositoryAndReturnsTop5()
        {
            // Arrange
            var mockRepo = new Mock<ITicketSaleRepository>();
            var expectedTop5 = new List<EventSales>
        {
            new EventSales { EventId = Guid.NewGuid(), TotalQuantity = 100, TotalRevenue = 1000m }
        };
            mockRepo.Setup(r => r.Top5ByQuantityAsync()).ReturnsAsync(expectedTop5);
            var service = new TicketService(mockRepo.Object);

            // Act
            var result = await service.GetTop5ByQuantityAsync();

            // Assert
            Assert.AreEqual(expectedTop5, result);
            mockRepo.Verify(r => r.Top5ByQuantityAsync(), Times.Once);
        }

        [Test]
        public async Task GetTop5ByRevenueAsync_CallsRepositoryAndReturnsTop5()
        {
            // Arrange
            var mockRepo = new Mock<ITicketSaleRepository>();
            var expectedTop5 = new List<EventSales>
        {
            new EventSales { EventId = Guid.NewGuid(), TotalQuantity = 50, TotalRevenue = 2000m }
        };
            mockRepo.Setup(r => r.Top5ByRevenueAsync()).ReturnsAsync(expectedTop5);
            var service = new TicketService(mockRepo.Object);

            // Act
            var result = await service.GetTop5ByRevenueAsync();

            // Assert
            Assert.AreEqual(expectedTop5, result);
            mockRepo.Verify(r => r.Top5ByRevenueAsync(), Times.Once);
        }
    }
}
