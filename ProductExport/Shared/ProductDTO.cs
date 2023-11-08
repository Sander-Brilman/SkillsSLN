using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductExport.Shared;
public class ProductDTO
{
    [Key]
    [Required]
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Title { get; set; } = "";

    [MaxLength(1000)]
    [Required]
    public string Description { get; set; } = string.Empty;
}


