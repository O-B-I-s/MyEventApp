using MyEventApp.Core.Models;
using MyEventApp.Core.Services;
using MyEventApp.Data.Repositories;

namespace MyEventApp.Api.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        public EventService(IEventRepository repo) => _repo = repo;
        public Task<IList<Event>> GetUpcomingEventsAsync(int days)
            => _repo.GetUpcomingAsync(days);
    }
}
