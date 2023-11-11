using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Server.Data;
using Mapster;
using BookStore.Shared;

namespace BookStore.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Author
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
    {
        return (await _context.Authors.ToListAsync())
            .Adapt<List<AuthorDTO>>();
    }



    // GET: api/Author/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return author.Adapt<AuthorDTO>();
    }



    // PUT: api/Author/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(int id, AuthorDTO author)
    {
        if (id != author.Id)
        {
            return BadRequest();
        }

        _context.Entry(author.Adapt<Author>()).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }



    // POST: api/Author
    [HttpPost]
    public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO author)
    {
        author.Id = 0;
        var dbAuthor = author.Adapt<Author>();

        _context.Authors.Add(dbAuthor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAuthor), new { author.Id }, dbAuthor.Adapt<AuthorDTO>());
    }



    // DELETE: api/Author/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _context
            .Authors
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return NoContent();
    }



    private bool AuthorExists(int id)
    {
        return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
