using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PodcastAppAPI.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var users = _context.Users.Select(u => new {
        u.FirstName,
        u.LastName,
        u.Email,
        u.PhoneNumber
        })
    .ToList();


        return Ok(users);
    }
}
