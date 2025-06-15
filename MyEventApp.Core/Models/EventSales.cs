namespace MyEventApp.Core.Models
{
    public class EventSales
    {
        public virtual Guid EventId { get; set; }
        public virtual string EventName { get; set; }
        public virtual long TotalQuantity { get; set; }
        public virtual decimal TotalRevenue { get; set; }
    }
}
