using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared;
public class BookDTO
{
    
    [Key]
    [Required]
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Desctiption { get; set; } = "";

    [Required]
    public double Price { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
