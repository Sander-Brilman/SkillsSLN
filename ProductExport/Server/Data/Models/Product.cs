using System.ComponentModel.DataAnnotations;

namespace ProductExport.Server.Data.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = "";

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public CategoryWithProductXml? Category { get; set; }

    public int CategoryId { get; set; }
}




