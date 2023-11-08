using System.ComponentModel.DataAnnotations;

namespace ProductExport.Server.Data.Models.XmlModels;

public class ProductXML
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = string.Empty;
}
