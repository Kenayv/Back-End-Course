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
    public ActionResult GetUser(int id)
    {
        var user = _db.GetUser(id);

        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("{name}")]
    public ActionResult AddUser(string name)
    {
        var user = _db.AddUser(name);

        if (user == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}/{name}")]
    public ActionResult UpdateUser(int id, string name)
    {
        var updatedUser = _db.UpdateUser(id, name);

        if (updatedUser == null)
        {
            return NotFound();
        }
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public ActionResult RemoveUser(int id)
    {
        if (!_db.RemoveUser(id))
        {
            return NotFound();
        }
        return NoContent();
    }
}
