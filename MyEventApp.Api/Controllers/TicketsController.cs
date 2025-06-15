using Microsoft.AspNetCore.Mvc;
using MyEventApp.Core.Services;

namespace MyEventApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _svc;
        public TicketsController(ITicketService svc) => _svc = svc;

        [HttpGet("event/{id}")]
        public async Task<IActionResult> GetForEvent(Guid id)
        {
            return Ok(await _svc.GetTicketsForEventAsync(id));
        }

        [HttpGet("top5/quantity")]
        public async Task<IActionResult> TopByQty()
        {
            return Ok(await _svc.GetTop5ByQuantityAsync());
        }

        [HttpGet("top5/revenue")]
        public async Task<IActionResult> TopByRev()
        {
            return Ok(await _svc.GetTop5ByRevenueAsync());
        }
    }
}
