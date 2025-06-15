using MyEventApp.Core.Models;
using MyEventApp.Core.Services;
using MyEventApp.Data.Repositories;


namespace MyEventApp.Api.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketSaleRepository _repo;
        public TicketService(ITicketSaleRepository repo) => _repo = repo;

        public Task<IList<TicketSale>> GetTicketsForEventAsync(Guid eventId)
            => _repo.GetByEventAsync(eventId);

        public Task<IList<EventSales>> GetTop5ByQuantityAsync()
            => _repo.Top5ByQuantityAsync();

        public Task<IList<EventSales>> GetTop5ByRevenueAsync()
            => _repo.Top5ByRevenueAsync();
    }
}
