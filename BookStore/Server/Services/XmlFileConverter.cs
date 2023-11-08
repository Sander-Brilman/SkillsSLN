using BookStore.Server.Data;
using BookStore.Server.Data.XmlModels;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace BookStore.Server.Services;

public class XmlFileConverter
{
    private readonly ApplicationDbContext _dbContext;
    private readonly XmlFileManager _xmlFileManager;

    private XmlSerializer _serializer;

    public XmlFileConverter(ApplicationDbContext dbContext, XmlFileManager xmlFileManager)
    {
        _dbContext = dbContext;
        _serializer = new(typeof(BooksXmlRootObject));
        _xmlFileManager = xmlFileManager;
    }


    /// <summary>
    /// Imports all the books from the specified file into the database
    /// </summary>
    /// <param name="fileName">the filename for the file that is to be imported</param>
    /// <returns></returns>
    public async Task ImportXmlFileIntoDbAsync(string fileName)
    {
        Stream xmlData = _xmlFileManager.GetXmlFileStream(fileName);

        var books = (BooksXmlRootObject?)_serializer.Deserialize(xmlData);

        List<Book> dbBooks = books.book
            .Select(b => new Book()
            {
                Id = b.id,
                Desctiption = b.description,
                Price = decimal.ToDouble(b.price),
                Title = b.title,
                Author = new Author()
                {
                    Id = b.author.id,
                    Name = b.author.name,
                },
                Category = new Category()
                {
                    Id = b.category.id,
                    Title = b.category.title,
                }
            })
            .ToList();

        _dbContext.Book.AddRange(dbBooks);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Exports all the contents of the database to a memory stream containing the data in xml format
    /// </summary>
    /// <returns>the stream containing the db content in xml format</returns>
    public async Task<Stream> ExportDbtoXmlStreamAsync()
    {
        List<Book> dbBooks = await _dbContext.Book
            .Include(b => b.Author)
            .Include(b => b.Category)
            .ToListAsync();

        BooksXmlRootObject xmlBooks = new()
        {
            book = dbBooks
                .Select(b => new BookXML()
                {
                    id = b.Id,
                    title = b.Title,
                    price = (decimal)b.Price,
                    description = b.Desctiption,
                    category = new CategoryXML()
                    {
                        id = b.Category.Id,
                        title = b.Category.Title
                    },
                    author = new AuthorXML()
                    {
                        id = b.Author.Id,
                        name = b.Author.Name
                    }
                })
                .ToArray()
        };

        Stream xmlStream = new MemoryStream();

        _serializer.Serialize(xmlStream, xmlBooks);

        return xmlStream;
    } 
}
