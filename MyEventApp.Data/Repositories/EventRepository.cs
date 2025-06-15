using MyEventApp.Core.Models;
using NHibernate;
using NHibernate.Linq;

namespace MyEventApp.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ISession _session;
        public EventRepository(ISession session) => _session = session;

        public async Task<IList<Event>> GetUpcomingAsync(int days)
        {
            var now = DateTime.UtcNow;
            var cutoff = now.AddDays(days);
            return await _session.Query<Event>()
                .Where(e => e.StartsOn >= now && e.StartsOn <= cutoff)
                .OrderBy(e => e.StartsOn)
                .ToListAsync();
        }
    }
}
