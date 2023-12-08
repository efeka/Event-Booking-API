using Microsoft.AspNetCore.Mvc;

namespace EventBookingAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    public UserController()
    {

    }

    [HttpGet]
    public string[] GetUsers()
    {
        return new string[] { "user1", "user2", "user3 " };
    }

}
