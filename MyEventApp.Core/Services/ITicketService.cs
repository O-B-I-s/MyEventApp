using MyEventApp.Core.Models;

namespace MyEventApp.Core.Services
{
    public interface ITicketService
    {
        Task<IList<TicketSale>> GetTicketsForEventAsync(Guid eventId);
        Task<IList<EventSales>> GetTop5ByQuantityAsync();
        Task<IList<EventSales>> GetTop5ByRevenueAsync();
    }
}
