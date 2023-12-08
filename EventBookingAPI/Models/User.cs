namespace EventBookingAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
