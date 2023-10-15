using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WieEetErMee.Server.Data;
using WieEetErMee.Server.Data.Models;
using WieEetErMee.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WieEetErMee.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserPresenceController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public UserPresenceController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<UserPresenceDTO>> GetPresense(string username, DateOnly date)
    {
        User? user = await _dbContext.Users.FindAsync(username);

        if (user is null)
        {
            return NotFound();
        }

        DateTime dateTime = date.ToDateTime(new TimeOnly());

        UserPresence? userPresence = await _dbContext.UserPresense
            .Where(u => u.User.Name == username && u.Day.Date == dateTime)
            .FirstOrDefaultAsync();

        return userPresence is null
            ? new UserPresenceDTO()
            {
                Date = date,
                IsPresent = user.IsDefaultPresentOnDay(date),
                Name = username,
            }
            : new UserPresenceDTO()
            {
                Date = date,
                IsPresent = userPresence.IsPresent,
                Name = username,
            };
    }



    [HttpPut("{username}")]
    public async Task<ActionResult> SetPresence(string username, [FromBody] UserPresenceDTO userPresenceDTO)
    {
        User? user = await _dbContext.Users.FindAsync(username);

        if (user is null)
        {
            return NotFound();
        }

        DateTime dateTime = userPresenceDTO.Date.ToDateTime(new TimeOnly());

        Day? day = await _dbContext.Days.FirstOrDefaultAsync(d => d.Date == dateTime);

        if (day is null)
        {
            day = new()
            {
                Date = dateTime,
                UserPresence = new List<UserPresence>() {
                    new UserPresence()
                    {
                        IsPresent = userPresenceDTO.IsPresent,
                        UserName = username,
                    }
                }
            };

            _dbContext.Days.Add(day);
            await _dbContext.SaveChangesAsync();

            return NoContent();

        }

        bool recordAlreadyExists = await _dbContext.UserPresense.AnyAsync(x => x.UserName == username && x.DayId == day.Id);

        UserPresence newUserPresense = new()
        {
            UserName = username,
            IsPresent = userPresenceDTO.IsPresent,
            Day = day,
        };

        UserPresence entryUserPresence = new()
        {
            DayId = day.Id,
            UserName = username,
            IsPresent = userPresenceDTO.IsPresent,
            Day = day,
        };

        _dbContext.Entry(entryUserPresence).State = recordAlreadyExists ? EntityState.Modified : EntityState.Added;
        await _dbContext.SaveChangesAsync();

        return NoContent();
    } 
}
