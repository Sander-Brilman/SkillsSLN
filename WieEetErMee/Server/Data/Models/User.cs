using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WieEetErMee.Server.Data.Models;

[PrimaryKey(nameof(Name))]
public class User
{
    [Key]
    [MaxLength(100)]
    public string Name { get; set; }

    public bool DefaultPresentOnMonday { get; set; } = false;

    public bool DefaultPresentOnTuesday { get; set; } = false;

    public bool DefaultPresentOnWednesday { get; set; } = false;

    public bool DefaultPresentOnThursday { get; set; } = false;

    public bool DefaultPresentOnFriday { get; set; } = false;

    public bool DefaultPresentOnSaturday { get; set; } = false;

    public bool DefaultPresentOnSunday { get; set; } = false;

    public List<UserPresence> UserPresence { get; set; } = new();



    public bool IsDefaultPresentOnDay(DateOnly date)
    {
        return date.DayOfWeek switch
        {
            DayOfWeek.Monday => DefaultPresentOnMonday,
            DayOfWeek.Tuesday => DefaultPresentOnTuesday,
            DayOfWeek.Wednesday => DefaultPresentOnWednesday,
            DayOfWeek.Thursday => DefaultPresentOnThursday,
            DayOfWeek.Friday => DefaultPresentOnFriday,
            DayOfWeek.Saturday => DefaultPresentOnSaturday,
            DayOfWeek.Sunday => DefaultPresentOnSunday,
            _ => false,
        };
    }
}
