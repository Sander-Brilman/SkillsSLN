using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalNotes.Shared;

public class JournalDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Content { get; set; } = "";

    [MaxLength(100)]
    [Required]
    public string Title { get; set; } = "";

    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime LastEditedAt { get; set; }
}
