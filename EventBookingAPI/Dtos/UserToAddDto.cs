namespace EventBookingAPI.Models
{
    public class UserToAddDto
    {
        public int RoleId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
