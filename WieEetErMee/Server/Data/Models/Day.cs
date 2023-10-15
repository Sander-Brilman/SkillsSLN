using System.ComponentModel.DataAnnotations;

namespace WieEetErMee.Server.Data.Models;

public class Day
{
    [Key]
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public List<UserPresence> UserPresence { get; set; } = new();
}
