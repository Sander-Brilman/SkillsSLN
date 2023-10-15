using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WieEetErMee.Server.Data;
using WieEetErMee.Server.Data.Models;
using WieEetErMee.Shared;

namespace WieEetErMee.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DayPresenceController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DayPresenceController(ApplicationDbContext context)
    {
        _context = context;
    }


    [HttpGet("{rawDateString}")]
    public async Task<ActionResult<DayPresenceDTO>> GetDayPresenceForDate(string rawDateString)
    {
        if (DateTime.TryParse(rawDateString, out DateTime date) is false)
        {
            return BadRequest();
        }

        DateOnly dateOnlyDate = DateOnly.FromDateTime(date);

        Day day = await _context.Days
            .Include(d => d.UserPresence)
            .FirstOrDefaultAsync(d => d.Date == date)
            ?? new Day()
            {
                Date = date,
                UserPresence = new(),
            };

        List<string> alreadyKnownUsers = day.UserPresence.Select(u => u.UserName).Distinct().ToList(); 

        List<User> missingUsers = await _context.Users
            .Where(u => alreadyKnownUsers.Contains(u.Name) == false)
            .ToListAsync();

        foreach (User user in missingUsers)
        {
            day.UserPresence.Add(new UserPresence()
            {
                Day = day,
                UserName = user.Name,
                DayId = day.Id,
                IsPresent = user.IsDefaultPresentOnDay(dateOnlyDate),
            });
        }

        return new DayPresenceDTO()
        {
            Date = dateOnlyDate,
            TotalPresence = day.UserPresence.Select(u => new UserPresenceDTO()
            {
                Date = dateOnlyDate,
                Name = u.UserName,
                IsPresent = u.IsPresent
            })
            .ToList(),
        };
    }
}
