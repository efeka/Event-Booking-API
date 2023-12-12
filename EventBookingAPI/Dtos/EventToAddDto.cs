namespace EventBookingAPI.Models
{
    public class EventToAddDto
    {
        public int OrganizerId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
