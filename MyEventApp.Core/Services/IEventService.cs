using MyEventApp.Core.Models;

namespace MyEventApp.Core.Services
{
    /// <summary>
    /// Defines operations for managing and retrieving event information.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Retrieves upcoming events within a specified time frame.
        /// </summary>
        /// <param name="days">
        /// Number of days to look ahead for events. 
        /// Must be between 1 and 365 (inclusive).
        /// </param>
        /// <returns>
        /// A list of upcoming events. Returns an empty list if no events are found
        /// or in case of non-critical errors.
        /// </returns>
        Task<IList<Event>> GetUpcomingEventsAsync(int days);
    }
}
