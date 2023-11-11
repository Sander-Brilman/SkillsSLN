using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Server.Data;
using BookStore.Server.Data.Models;
using BookStore.Shared;
using Mapster;

namespace BookStore.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetBook()
    {
        return (await _context.Book.ToListAsync())
            .Adapt<List<BookDTO>>();
    }

    // GET: api/Book/allInCategory?catagoryId=5
    [HttpGet("allInCategory")]
    public async Task<ActionResult<List<BookDTO>>> GetBooksInCatagory(int catagoryId)
    {
        return (await _context.Book
            .Where(b => b.CategoryId == catagoryId)
            .ToListAsync())
            .Adapt<List<BookDTO>>();
    }


    // GET: api/Book/allFromAuthor?authorId=5
    [HttpGet("allFromAuthor")]
    public async Task<ActionResult<List<BookDTO>>> GetBooksFromAuthor(int authorId)
    {
        return (await _context.Book
            .Where(b => b.AuthorId == authorId)
            .ToListAsync())
            .Adapt<List<BookDTO>>();
    }


    // GET: api/Book/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDTO>> GetBook(int id)
    {
        var book = await _context.Book.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return book.Adapt<BookDTO>();  
    }



    // PUT: api/Book/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, BookDTO bookDTO)
    {
        if (id != bookDTO.Id)
        {
            return BadRequest();
        }

        var book = bookDTO.Adapt<Book>();

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }



    // POST: api/Book
    [HttpPost]
    public async Task<ActionResult<BookDTO>> PostBook(BookDTO bookDTO)
    {
        var book = bookDTO.Adapt<Book>();

        _context.Book.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { book.Id }, book.Adapt<BookDTO>());
    }



    // DELETE: api/Book/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _context.Book
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();

        return NoContent();
    }

    private bool BookExists(int id)
    {
        return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
