using System.ComponentModel.DataAnnotations;

namespace StandAloneWasmTesting.Models;

public class Product : ICloneable
{
    public object Clone()
    {
        return MemberwiseClone();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Titel is verplicht")]
    [MaxLength(50)]
    public string Title { get; set; }

    [Required(ErrorMessage = "Beschrijving is verplicht")]
    [MaxLength(200)]
    public string Description { get; set; }

    [Required(ErrorMessage = "Foto url is verplicht")]
    [Url]
    public string ImageUrl { get; set; }

    [Required(ErrorMessage = "Aantal op voorraad is verplicht")]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Required(ErrorMessage = "Prijs is verplicht")]
    public decimal Price { get; set; }
}
