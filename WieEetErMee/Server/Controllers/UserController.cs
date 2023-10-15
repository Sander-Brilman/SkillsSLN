using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WieEetErMee.Server.Data;
using WieEetErMee.Server.Data.Models;
using WieEetErMee.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WieEetErMee.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets all user names.
    /// </summary>
    /// <returns></returns>
    [HttpGet("allUserNames")]
    public async Task<List<string>> GetAllUserNames()
    {
        return await _context.Users
            .Select(u => u.Name)
            .ToListAsync();
    }





    // GET api/<UserController>/5
    [HttpGet("{username}")]
    public async Task<ActionResult<UserSettingsDTO>> Get(string username)
    {
        User? user = await _context.Users.FindAsync(username);

        if (user is null)
        {
            return NotFound();
        }

        return user.Adapt<UserSettingsDTO>();
    }



    // POST api/<UserController>
    [HttpPost]
    public async Task<ActionResult> Post(UserSettingsDTO newUser)
    {
        if (await DoesUserExist(newUser.Name) is false) {
            return BadRequest();
        }

        User user = newUser.Adapt<User>();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), newUser);
    }



    // PUT api/<UserController>/5
    [HttpPut("{username}")]
    public async Task<ActionResult> Put(string username, UserSettingsDTO newUser)
    {
        if (await DoesUserExist(newUser.Name) is false || username != newUser.Name)
        {
            return BadRequest();
        }

        _context.Entry(newUser.Adapt<User>()).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }



    // DELETE api/<UserController>/5
    [HttpDelete("{username}")]
    public async Task<ActionResult> Delete(string username)
    {
        await _context.Users
            .Where(u => u.Name == username)
            .ExecuteDeleteAsync();

        return NoContent();
    }



    private async Task<bool> DoesUserExist(string username)
    {
        return await _context.Users.AnyAsync(u => u.Name == username);
    }
}
