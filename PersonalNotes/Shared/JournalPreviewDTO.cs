using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalNotes.Shared;
public class JournalPreviewDTO
{
    public int Id { get; set; }

    public string Title { get; set; } = "";


    /// <summary>
    /// First 200 characters of the content as a small preview 
    /// </summary>
    public string ContentPreview { get; set; } = "";

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public DateTime LastEditedAt { get; set; }
}
