using System.ComponentModel.DataAnnotations;

namespace EducationAssingments.Data.Entities;

public class AssingmentAnswer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Answer { get; set; } = string.Empty;

    public Assingment Assingment { get; set; }

    public int AssingmentId { get; set; }

    public ApplicationUser User { get; set; }

    public string UserId { get; set; }
}
