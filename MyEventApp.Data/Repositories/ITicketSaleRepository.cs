using MyEventApp.Core.Models;

namespace MyEventApp.Data.Repositories
{
    public interface ITicketSaleRepository
    {
        Task<IList<TicketSale>> GetByEventAsync(Guid eventId);
        Task<IList<EventSales>> Top5ByQuantityAsync();
        Task<IList<EventSales>> Top5ByRevenueAsync();
    }
}
