using System.ComponentModel.DataAnnotations;

namespace PlaygroundStandAloneWasm.Models;

public class Product
{
    [Required]
    public int Id { get; set; }

    [MaxLength(255)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = "";
}


