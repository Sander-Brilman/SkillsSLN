using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalNotes.Server.Data;
using PersonalNotes.Server.Data.Models;
using PersonalNotes.Shared;

namespace PersonalNotes.Server.Controllers;


[Route("api/[controller]")]
[ApiController]
public class JournalController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JournalController(ApplicationDbContext context)
    {
        _context = context;
    }



    // GET: api/Journal
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JournalPreviewDTO>>> GetJournal()
    {
        if (IsUnAuthenticated())
        {
            return Unauthorized();
        }


        string userId = GetUserId();

        return await _context.Journal
            .Where(j => j.UserId == userId)
            .Select(j => new JournalPreviewDTO()
            {
                ContentPreview = new string(j.Content.Take(200).ToArray()),
                Date = j.Date,
                Id = j.Id,
                LastEditedAt = j.LastEditedAt,
                Title = j.Title,
            })
            .ToListAsync();
    }



    // GET: api/Journal/5
    [HttpGet("{id}")]
    public async Task<ActionResult<JournalDTO>> GetJournal(int id)
    {
        if (IsUnAuthenticated())
        {
            return Unauthorized();
        }

        string userId = GetUserId();

        var journal = await _context.Journal
            .FirstOrDefaultAsync(j => j.Id == id && j.UserId == userId);

        if (journal == null)
        {
            return NotFound();
        }

        return journal.Adapt<JournalDTO>();
    }


    // PUT: api/Journal/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutJournal(int id, JournalDTO journalDTO)
    {
        if (IsUnAuthenticated())
        {
            return Unauthorized();
        }

        if (id != journalDTO.Id)
        {
            return BadRequest();
        }

        string userId = GetUserId();

        string userIdForJournal = await _context.Journal
            .Where(j => j.Id == id)
            .Select(j => j.UserId)
            .FirstAsync();

        if (userIdForJournal != userId)
        {
            return BadRequest("current user does not own this journal!");
        }

        var journal = journalDTO.Adapt<Journal>();
        journal.UpdateLastEdited();
        journal.UserId = userId;

        _context.Entry(journal).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!JournalExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }



    // POST: api/Journal
    [HttpPost]
    public async Task<ActionResult<JournalDTO>> PostJournal(JournalDTO journalDTO)
    {
        if (IsUnAuthenticated())
        {
            return Unauthorized();
        }

        var journal = journalDTO.Adapt<Journal>();

        journal.UpdateLastEdited();
        journal.UserId = GetUserId();

        _context.Journal.Add(journal);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetJournal", new { id = journal.Id }, journal.Adapt<JournalDTO>());
    }



    // DELETE: api/Journal/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJournal(int id)
    {
        if (IsUnAuthenticated())
        {
            return Unauthorized();
        }

        string userId = GetUserId();

        await _context
            .Journal
            .Where(j => j.Id == id && j.UserId == userId)
            .ExecuteDeleteAsync();

        return NoContent();
    }

    private bool IsUnAuthenticated()
    {
        return User.Identity!.IsAuthenticated is false;
    }

    private string GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }

    private bool JournalExists(int id)
    {
        return (_context.Journal?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
