using MyEventApp.Core.Models;

namespace MyEventApp.Core.Services
{
    public interface IEventService
    {
        Task<IList<Event>> GetUpcomingEventsAsync(int days);
    }
}
