using MyEventApp.Core.Models;

namespace MyEventApp.Data.Repositories
{
    public interface IEventRepository
    {
        Task<IList<Event>> GetUpcomingAsync(int days);
    }
}
