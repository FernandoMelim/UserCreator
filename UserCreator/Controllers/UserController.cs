using Microsoft.AspNetCore.Mvc;

namespace UserCreator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    public UserController()
    {

    }

    [HttpGet]
    public async Task Get()
    {
        
    }

    [HttpGet("{id}")]
    public async Task Get([FromQuery] int userId)
    {

    }

    [HttpPost]
    public async Task Post([FromBody] object user)
    {

    }
}

