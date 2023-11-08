using Mapster;
using Microsoft.EntityFrameworkCore;
using ProductExport.Server.Data;
using ProductExport.Server.Data.Models;
using ProductExport.Server.Data.Models.XmlModels;
using System.Xml;
using System.Xml.Serialization;

namespace ProductExport.Server.Services;

public class XmlService
{
    private readonly ApplicationDbContext _dbContext;

    private readonly XmlSerializer _xmlSerializer = new(typeof(List<CategoryWithProductXml>));

    public XmlService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Exports all the categories and products to xml format as a stream
    /// </summary>
    /// <returns>The stream containing all the categories as xml</returns>
    public async Task<Stream> ExportAllToXmlAsync()
    {
        List<CategoryWithProductXml> categories = (await _dbContext.Category
            .Include(c => c.Products)
            .ToListAsync())
            .Adapt<List<CategoryWithProductXml>>();

        Stream xmlStream = new MemoryStream();

        _xmlSerializer.Serialize(xmlStream, categories);

        return xmlStream;
    }

    /// <summary>
    /// Imports all the categories and products from a xml file
    /// </summary>
    /// <param name="xmlFilePath">The file path to the xml file</param>
    /// <returns></returns>
    public async Task<bool> ImportFromXml(string xmlFilePath)
    {
        StreamReader xmlStream = new(xmlFilePath);

        List<CategoryWithProductXml> xmlCategories = (List<CategoryWithProductXml>)_xmlSerializer.Deserialize(xmlStream)!;

        List<Category> dbCategories = xmlCategories.Adapt<List<Category>>();

        _dbContext.Category.AddRange(dbCategories);
        await _dbContext.SaveChangesAsync();
    }

}
