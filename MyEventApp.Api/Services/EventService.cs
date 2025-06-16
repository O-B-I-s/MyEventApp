using MyEventApp.Core.Models;
using MyEventApp.Core.Services;
using MyEventApp.Data.Repositories;

namespace MyEventApp.Api.Services
{

    /// <summary>
    /// Service for handling event-related business logic and data operations.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        /// <param name="repo">The event repository dependency.</param>
        public EventService(IEventRepository repo) => _repo = repo;



        /// <summary>
        /// Retrieves upcoming events within a specified time frame.
        /// </summary>
        /// <param name="days">Number of days to look ahead for events.</param>
        /// <returns>
        /// A list of upcoming events. Returns an empty list if no events are found.
        /// </returns>
        public Task<IList<Event>> GetUpcomingEventsAsync(int days)
            => _repo.GetUpcomingAsync(days);
    }
}