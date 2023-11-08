using BookStore.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class XmlFileController : ControllerBase
{
    private readonly XmlFileConverter _xmlFileConverter;
    private readonly XmlFileManager _xmlFileManager;

    public XmlFileController(XmlFileConverter xmlFileConverter)
    {
        _xmlFileConverter = xmlFileConverter;
    }


    /// <summary>
    /// Import endpoint for loading a xml file into the database
    /// </summary>
    /// <param name="xmlFile">the xml file</param>
    /// <returns></returns>
    [HttpPost]
    public async Task Import([FromForm] IFormFile xmlFile)
    {
        string fileName = await _xmlFileManager.UploadXmlFileAsync(xmlFile.OpenReadStream());

        await _xmlFileConverter.ImportXmlFileIntoDbAsync(fileName);
    }

    /// <summary>
    /// endpoint for exporting all the contents of the database to a xml file
    /// </summary>
    /// <returns>the xml file</returns>
    [HttpGet]
    public async Task<ActionResult> Export()
    {
        Stream xmlStream = await _xmlFileConverter.ExportDbtoXmlStreamAsync();

        return File(xmlStream, "application/xml", "Export.xml");
    }
}
