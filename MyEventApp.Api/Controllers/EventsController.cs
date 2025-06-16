using Microsoft.AspNetCore.Mvc;
using MyEventApp.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace MyEventApp.Api.Controllers
{
    /// <summary>
    /// API controller for retrieving upcoming event data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ILogger<EventsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsController"/> class.
        /// </summary>
        /// <param name="eventService">The event service dependency.</param>
        /// <param name="logger">The logger for capturing errors and diagnostics.</param>
        /// <exception cref="ArgumentNullException">Thrown if dependencies are null.</exception>
        public EventsController(
            IEventService eventService,
            ILogger<EventsController> logger)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves upcoming events within a specified time frame.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/events?days=30
        ///
        /// Constraints:
        /// - `days` must be between 1 and 365 (inclusive)
        /// </remarks>
        /// <param name="days">Number of days to look ahead for events (default: 30).</param>
        /// <response code="200">Returns the list of upcoming events.</response>
        /// <response code="204">No events found in the specified period.</response>
        /// <response code="400">Invalid input parameter detected.</response>
        /// <response code="500">Internal server error while processing the request.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUpcomingEvents(
            [FromQuery, Range(1, 365, ErrorMessage = "Days must be between 1 and 365.")] int days = 30)
        {
            try
            {
                _logger.LogInformation("Fetching upcoming events for next {Days} days.", days);

                var events = await _eventService.GetUpcomingEventsAsync(days);

                if (events == null)
                {
                    _logger.LogWarning("Event service returned null for days: {Days}", days);
                    return NoContent();
                }

                if (!events.Any())
                {
                    _logger.LogInformation("No upcoming events found for next {Days} days.", days);
                    return NoContent();
                }

                _logger.LogInformation("Returning {EventCount} upcoming events.", events.Count());
                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching upcoming events for {Days} days: {ErrorMessage}", days, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}