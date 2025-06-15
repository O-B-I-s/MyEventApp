using MyEventApp.Core.Models;
using NHibernate;
using NHibernate.Linq;

namespace MyEventApp.Data.Repositories
{
    public class TicketSaleRepository : ITicketSaleRepository
    {
        private readonly ISession _session;
        public TicketSaleRepository(ISession session) => _session = session;

        public async Task<IList<TicketSale>> GetByEventAsync(Guid eventId)
        {
            //return await _session.Query<TicketSale>()
            //    .Where(t => t.EventId == eventId)
            //    .ToListAsync();
            return await _session.CreateSQLQuery(
            @"SELECT * 
            FROM TicketSales 
            WHERE LOWER(EventId) = LOWER(:eventId)")
                .AddEntity(typeof(TicketSale))
                .SetParameter("eventId", eventId.ToString(), NHibernateUtil.String)
                .ListAsync<TicketSale>();
        }

        public async Task<IList<EventSales>> Top5ByQuantityAsync()
        {
            var query = _session.Query<TicketSale>()
                .GroupBy(t => t.EventId)
                .Select(g => new EventSales
                {
                    EventId = g.Key,

                    TotalQuantity = g.Count(),
                    TotalRevenue = g.Sum(x => x.PriceInCents) / 100m
                })
                .OrderByDescending(es => es.TotalQuantity)
                .Take(5);
            return await query.ToListAsync();
        }

        public async Task<IList<EventSales>> Top5ByRevenueAsync()
        {
            var query = _session.Query<TicketSale>()
                .GroupBy(t => t.EventId)
                .Select(g => new EventSales
                {
                    EventId = g.Key,
                    TotalQuantity = g.Count(),
                    TotalRevenue = g.Sum(x => x.PriceInCents) / 100m
                })
                .OrderByDescending(es => es.TotalRevenue)
                .Take(5);
            return await query.ToListAsync();
        }
    }
}
