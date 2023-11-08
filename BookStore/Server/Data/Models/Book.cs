using System.ComponentModel.DataAnnotations;

namespace BookStore.Server.Data.Models;

public class Book
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    public string Desctiption { get; set; } = "";

    public double Price { get; set; }

    public Author Author { get; set; }

    public int AuthorId { get; set; }

    public Category Category { get; set; }

    public int CategoryId { get; set; }

}
