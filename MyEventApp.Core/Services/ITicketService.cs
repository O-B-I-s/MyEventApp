using MyEventApp.Core.Models;

namespace MyEventApp.Core.Services
{
    /// <summary>
    /// Defines operations for managing and querying ticket sales data.
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Retrieves all ticket sales for a specific event.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event (non-empty GUID).</param>
        /// <returns>
        /// A list of ticket sales for the specified event. 
        /// Returns an empty list if no tickets are found or in case of non-critical errors.
        /// </returns>
        Task<IList<TicketSale>> GetTicketsForEventAsync(Guid eventId);


        /// <summary>
        /// Retrieves the top 5 events ranked by ticket quantity sold.
        /// </summary>
        /// <returns>
        /// A list of event sales records ordered by descending ticket quantity. 
        /// Returns an empty list if no data is available or in case of non-critical errors.
        /// </returns>
        Task<IList<EventSales>> GetTop5ByQuantityAsync();


        /// <summary>
        /// Retrieves the top 5 events ranked by total ticket revenue generated.
        /// </summary>
        /// <returns>
        /// A list of event sales records ordered by descending revenue. 
        /// Returns an empty list if no data is available or in case of non-critical errors.
        /// </returns>
        Task<IList<EventSales>> GetTop5ByRevenueAsync();
    }
}
