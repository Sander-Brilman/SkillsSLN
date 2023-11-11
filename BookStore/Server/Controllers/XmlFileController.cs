using BookStore.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BookStore.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class XmlFileController : ControllerBase
{
    private readonly XmlFileConverter _xmlFileConverter;
    private readonly XmlFileManager _xmlFileManager;

    public XmlFileController(XmlFileConverter xmlFileConverter, XmlFileManager xmlFileManager)
    {
        _xmlFileConverter = xmlFileConverter;
        _xmlFileManager = xmlFileManager;
    }


    /// <summary>
    /// Import endpoint for loading a xml file into the database
    /// </summary>
    /// <param name="xmlFile">the xml file</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Import([FromForm] IFormFile xmlFile)
    {
        string fileName = await _xmlFileManager.UploadXmlFileAsync(xmlFile.OpenReadStream());

        await _xmlFileConverter.ImportXmlFileIntoDbAsync(fileName);

        return Redirect("/import-export");
    }

    /// <summary>
    /// endpoint for exporting all the contents of the database to a xml file
    /// </summary>
    /// <returns>the xml file</returns>
    [HttpGet]
    public async Task<ActionResult> Export()
    {
        try
        {
            string xmlText = await _xmlFileConverter.ExportDbtoXmlStreamAsync();

            return File(Encoding.UTF8.GetBytes(xmlText), "application/xml", "Export.xml");
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}
