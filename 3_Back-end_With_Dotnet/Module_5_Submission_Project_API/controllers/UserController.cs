using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DbControllerMock _db;

    public UserController(DbControllerMock db)
    {
        _db = db;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(int id)
    {
        var user = await _db.GetUserByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("all")]
    public async Task<ActionResult> GetAllUsers(int id)
    {
        var allUsers = await _db.GetAllUsersAsync();
        return Ok(JsonSerializer.Serialize(allUsers));
    }

    [HttpPost("{name}/{email}")]
    public async Task<ActionResult> AddUser(string name, string email)
    {
        try
        {
            var user = await _db.AddUserAsync(name, email);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut("{id}/{name}")]
    public async Task<ActionResult> UpdateUserName(int id, string name)
    {
        var updatedUser = await _db.UpdateUserNameAsync(id, name);

        if (updatedUser == null)
            return NotFound();

        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveUser(int id)
    {
        if (!await _db.RemoveUserAsync(id))
            return NotFound();

        return NoContent();
    }
}
