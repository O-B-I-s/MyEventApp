using MyEventApp.Core.Models;
using MyEventApp.Core.Services;
using MyEventApp.Data.Repositories;


namespace MyEventApp.Api.Services
{
    /// <summary>
    /// Service implementation for ticket-related operations.
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly ITicketSaleRepository _repo;
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketService"/> class.
        /// </summary>
        /// <param name="repo">The ticket repository dependency.</param>
        public TicketService(ITicketSaleRepository repo) => _repo = repo;
        /// <summary>
        /// Retrieves all ticket sales for a specific event.
        /// </summary>
        /// <param name="eventId">The event ID (GUID format).</param>
        /// <returns>
        /// A list of ticket sales for the event. Returns an empty list if no tickets are found.
        /// </returns>

        public Task<IList<TicketSale>> GetTicketsForEventAsync(Guid eventId)
            => _repo.GetByEventAsync(eventId);


        /// <summary>
        /// Retrieves top 5 events by ticket quantity sold.
        /// </summary>
        /// <returns>
        /// A list of event sales records. Returns an empty list if no data is available.
        /// </returns>
        public Task<IList<EventSales>> GetTop5ByQuantityAsync()
            => _repo.Top5ByQuantityAsync();

        /// <summary>
        /// Retrieves top 5 events by ticket revenue generated.
        /// </summary>
        /// <returns>
        /// A list of event sales records. Returns an empty list if no data is available.
        /// </returns>
        public Task<IList<EventSales>> GetTop5ByRevenueAsync()
            => _repo.Top5ByRevenueAsync();
    }
}
