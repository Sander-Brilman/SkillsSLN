using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared;
public class CategoryDTO
{
    [Key]
    [Required]
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Title { get; set; } = string.Empty;
}
