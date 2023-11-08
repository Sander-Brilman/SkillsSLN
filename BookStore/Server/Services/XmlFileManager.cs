namespace BookStore.Server.Services;

public class XmlFileManager
{
    private static readonly string _xmlFolderPath = Path.Combine(
        Directory.GetCurrentDirectory(),
        "XmlFiles"
    );

    /// <summary>
    /// Creates a xml file containting the stream's data in the XmlFiles directory
    /// </summary>
    /// <param name="xmlStream">the stream containing the xml data</param>
    /// <returns>the file name of the newly created file</returns>
    public async Task<string> UploadXmlFileAsync(Stream xmlStream)
    {
        string newFileName = $"{Guid.NewGuid()}.xml";

        string fullPath = Path.Combine( _xmlFolderPath, newFileName);

        using var fileStream = new FileStream(fullPath, FileMode.CreateNew);
        await xmlStream.CopyToAsync(fileStream);

        return newFileName;
    }


    /// <summary>
    /// Gets a stream containing the xml file contents of the specified file
    /// </summary>
    /// <param name="fileName">the filename of the file inside the XmlFiles directory</param>
    /// <returns>the stream containing all the data</returns>
    public Stream GetXmlFileStream(string fileName)
    {
        string fullPath = Path.Combine(_xmlFolderPath, fileName);

        return File.OpenRead(fullPath);
    }
}
