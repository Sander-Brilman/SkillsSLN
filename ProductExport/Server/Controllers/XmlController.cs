using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductExport.Server.Services;

namespace ProductExport.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class XmlController : ControllerBase
{
    private readonly XmlService _xmlService;

    public XmlController(XmlService xmlService)
    {
        _xmlService = xmlService;
    }



    [HttpGet]
    public async Task<ActionResult> ExportToXmlFile()
    {
        Stream xmlStream = await _xmlService.ExportAllToXmlAsync();

        return File(xmlStream, "application/xml");
    }



    private static string _xmlFilesFolder = Path.Combine(
        Directory.GetCurrentDirectory(),
        "XmlFiles"
    );

    public async Task<ActionResult> ImportFromXmlFile([FromForm] IFormFile xmlFile)
    {
        string newFileName = $"{Guid.NewGuid()}.xml";
        string finalPath = Path.Combine(_xmlFilesFolder, newFileName);

        Stream readStream = xmlFile.OpenReadStream();

        using (var file = System.IO.File.Create(finalPath))
        {
            await readStream.CopyToAsync(file);
        }

        try
        {
            await _xmlService.ImportFromXml(finalPath);
        }
        catch (Exception)
        {

            throw;
        }


        return Ok();
    }


}
