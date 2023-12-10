namespace EventBookingAPI.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int OrganizerId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime StartDate { get; set; }
    }
}
