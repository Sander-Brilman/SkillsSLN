using System.ComponentModel.DataAnnotations;

namespace PersonalNotes.Server.Data.Models;

public class Journal
{
    [Key]
    public int Id { get; set; }

    public string Content { get; set; } = "";

    [MaxLength(100)]
    public string Title { get; set; } = "";

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public DateTime LastEditedAt { get; set; } = DateTime.UtcNow;

    // relations

    public ApplicationUser User { get; set; }

    public string UserId { get; set; } = "";

    //

    public void UpdateLastEdited()
    {
        LastEditedAt = DateTime.UtcNow;
    }
}
