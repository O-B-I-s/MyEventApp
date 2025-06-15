using Microsoft.AspNetCore.Mvc;
using MyEventApp.Core.Services;

namespace MyEventApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _svc;
        public EventsController(IEventService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int days = 30)
        {
            var list = await _svc.GetUpcomingEventsAsync(days);
            return Ok(list);
        }
    }
}
