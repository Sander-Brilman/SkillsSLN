using System.ComponentModel.DataAnnotations;

namespace ProductExport.Server.Data.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = "";

    public List<Product> Products { get; set; } = new();
}
