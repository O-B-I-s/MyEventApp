using Moq;
using MyEventApp.Api.Services;
using MyEventApp.Core.Models;
using MyEventApp.Data.Repositories;

namespace MyEventApp.Tests
{

    [TestFixture]
    public class EventServiceTests
    {
        [Test]
        public async Task GetUpcomingEventsAsync_CallsRepositoryAndReturnsEvents()
        {
            // Arrange
            var mockRepo = new Mock<IEventRepository>();
            var expectedEvents = new List<Event>
        {
            new Event { Id = Guid.NewGuid(), Name = "Event1", StartsOn = DateTime.UtcNow.AddDays(1) }
        };
            mockRepo.Setup(r => r.GetUpcomingAsync(30)).ReturnsAsync(expectedEvents);
            var service = new EventService(mockRepo.Object);

            // Act
            var result = await service.GetUpcomingEventsAsync(30);

            // Assert
            Assert.AreEqual(expectedEvents, result);
            mockRepo.Verify(r => r.GetUpcomingAsync(30), Times.Once);
        }
    }
}
