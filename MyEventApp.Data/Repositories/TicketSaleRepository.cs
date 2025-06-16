using MyEventApp.Core.Models;
using NHibernate;
using NHibernate.Linq;

namespace MyEventApp.Data.Repositories
{
    /// <summary>
    /// Repository for managing TicketSale entities and related sales analytics
    /// </summary>
    public class TicketSaleRepository : ITicketSaleRepository
    {
        private readonly ISession _session;
        public TicketSaleRepository(ISession session) => _session = session;


        // <summary>
        /// Retrieves all ticket sales for a specific event
        /// </summary>
        /// <param name="eventId">The event identifier</param>
        /// <returns>List of ticket sales for the specified event</returns>
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


        /// <summary>
        /// Retrieves the top 5 events by ticket quantity sold
        /// </summary>
        /// <returns>List of EventSales objects ordered by quantity</returns>
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


        /// <summary>
        /// Retrieves the top 5 events by revenue generated
        /// </summary>
        /// /// <returns>List of EventSales objects ordered by revenue</returns>
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
