using BookStore.Server.Data;
using BookStore.Server.Data.XmlModels;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Xml;
using System.Xml.Linq;
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
        string xmlData = _xmlFileManager.GetXmlString(fileName);

        using TextReader reader = new StringReader(xmlData);

        var books = (BooksXmlRootObject?)_serializer.Deserialize(reader);

        List<Author> authors = books.book
            .Select(b => new Author()
            {
                Id = 0,
                Name = b.author.name,
            })
            .Distinct()
            .ToList();


        List<Category> catagories = books.book
            .Select(b => new Category()
            {
                Id = 0,
                Title = b.category.title,
            })
            .Distinct()
            .ToList();


        List<Book> dbBooks = books.book
            .Select(b => new Book()
            {
                Id = 0,
                Desctiption = b.description,
                Price = decimal.ToDouble(b.price),
                Title = b.title,
                Author = authors.Where(a => a.Name == b.author.name).First(),
                Category = catagories.Where(c => c.Title == b.category.title).First(),
            })
            .ToList();

        _dbContext.Book.AddRange(dbBooks);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Exports all the contents of the database to a memory stream containing the data in xml format
    /// </summary>
    /// <returns>the stream containing the db content in xml format</returns>
    public async Task<string> ExportDbtoXmlStreamAsync()
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

        string xmlString = "";

        using (var sww = new StringWriter())
        {
            using XmlWriter writer = XmlWriter.Create(sww);
            _serializer.Serialize(writer, xmlBooks);
            xmlString = sww.ToString(); // Your XML
        }

        return xmlString;
    } 
}
