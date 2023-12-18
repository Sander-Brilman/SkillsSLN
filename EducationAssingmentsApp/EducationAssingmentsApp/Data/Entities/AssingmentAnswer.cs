using System.ComponentModel.DataAnnotations;

namespace EducationAssingmentsApp.Data.Entities;

public class AssingmentAnswer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Answer { get; set; } = string.Empty;

    [Range(0, 100)]
    public int? Grade { get; set; } = null;

    public DateTime TurnedInAt { get; set; } = DateTime.UtcNow;


    // relations

    public Assingment Assingment { get; set; }

    public int AssingmentId { get; set; }

    public ApplicationUser User { get; set; }

    public string UserId { get; set; }
}