using System.ComponentModel.DataAnnotations;

namespace BookStore.Server.Data.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    public List<Book> Books { get; set; } = new();
}
