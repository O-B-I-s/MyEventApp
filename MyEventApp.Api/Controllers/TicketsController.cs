using Microsoft.AspNetCore.Mvc;
using MyEventApp.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace MyEventApp.Api.Controllers
{
    /// <summary>
    /// Exposes endpoints for ticket sales queries (by event, top 5 by quantity/revenue).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketsController"/> class.
        /// </summary>
        /// <param name="ticketService">The ticket service dependency.</param>
        /// <param name="logger">The logger for capturing errors and diagnostics.</param>
        /// <exception cref="ArgumentNullException">Thrown if dependencies are null.</exception>
        public TicketsController(
            ITicketService ticketService,
            ILogger<TicketsController> logger)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all tickets for a specific event.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/tickets/event/68a4c68e-8c1a-4475-9b9a-3f5d5e35bdc5
        /// </remarks>
        /// <param name="id">The event ID (GUID format).</param>
        /// <response code="200">Returns the list of tickets for the event.</response>
        /// <response code="400">Invalid event ID format.</response>
        /// <response code="404">No tickets found for the specified event.</response>
        /// <response code="500">Internal server error while processing the request.</response>
        [HttpGet("event/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetForEvent(
            [FromRoute, Required(ErrorMessage = "Event ID is required.")] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("Invalid event ID: {EventId}", id);
                    return BadRequest("Event ID cannot be empty.");
                }

                _logger.LogInformation("Fetching tickets for event: {EventId}", id);

                var tickets = await _ticketService.GetTicketsForEventAsync(id);

                if (tickets == null)
                {
                    _logger.LogWarning("Ticket service returned null for event: {EventId}", id);
                    return NotFound($"No tickets found for event {id}.");
                }

                if (!tickets.Any())
                {
                    _logger.LogInformation("No tickets found for event: {EventId}", id);
                    return NotFound($"No tickets found for event {id}.");
                }

                _logger.LogInformation("Returning {TicketCount} tickets for event {EventId}", tickets.Count(), id);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching tickets for event {EventId}: {ErrorMessage}", id, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves top 5 events by ticket quantity sold.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/tickets/top5/quantity
        /// </remarks>
        /// <response code="200">Returns top 5 events by ticket quantity.</response>
        /// <response code="204">No ticket sales data available.</response>
        /// <response code="500">Internal server error while processing the request.</response>
        [HttpGet("top5/quantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TopByQty()
        {
            try
            {
                _logger.LogInformation("Fetching top 5 events by ticket quantity");

                var results = await _ticketService.GetTop5ByQuantityAsync();

                if (results == null)
                {
                    _logger.LogWarning("Top 5 by quantity service returned null");
                    return NoContent();
                }

                if (!results.Any())
                {
                    _logger.LogInformation("No ticket sales data available for quantity ranking");
                    return NoContent();
                }

                _logger.LogInformation("Returning top {ResultCount} events by quantity", results.Count());
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching top 5 events by quantity: {ErrorMessage}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves top 5 events by ticket revenue generated.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/tickets/top5/revenue
        /// </remarks>
        /// <response code="200">Returns top 5 events by ticket revenue.</response>
        /// <response code="204">No ticket revenue data available.</response>
        /// <response code="500">Internal server error while processing the request.</response>
        [HttpGet("top5/revenue")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TopByRev()
        {
            try
            {
                _logger.LogInformation("Fetching top 5 events by ticket revenue");

                var results = await _ticketService.GetTop5ByRevenueAsync();

                if (results == null)
                {
                    _logger.LogWarning("Top 5 by revenue service returned null");
                    return NoContent();
                }

                if (!results.Any())
                {
                    _logger.LogInformation("No ticket revenue data available");
                    return NoContent();
                }

                _logger.LogInformation("Returning top {ResultCount} events by revenue", results.Count());
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching top 5 events by revenue: {ErrorMessage}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}