using Microsoft.EntityFrameworkCore;

namespace WieEetErMee.Server.Data.Models;

[PrimaryKey(nameof(DayId), nameof(UserName))]
public class UserPresence
{
    public Day Day { get; set; }

    public int DayId { get; set; }

    public bool IsPresent { get; set; }

    public User User { get; set; }

    public string UserName { get; set; }
}
