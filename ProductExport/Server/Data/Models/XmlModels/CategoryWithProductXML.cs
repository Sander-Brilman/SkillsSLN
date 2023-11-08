using System.ComponentModel.DataAnnotations;

namespace ProductExport.Server.Data.Models.XmlModels;

public class CategoryWithProductXml
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public List<ProductXML> Products { get; set; } = new();
}