using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Produces("application/json")]
    [Route("api/values")]
    public class ValuesController : Controller
    {
        private readonly TodoAppContext _context;

        public ValuesController(TodoAppContext context)
        {
            _context = context;

            if (_context.TodoNotes.Count() == 0)
            {
                _context.TodoNotes.Add(new TodoNote { Name = "Initial" });
                _context.SaveChanges();
            }
        }

        // GET: api/TodoNotes
        [HttpGet]
        public IEnumerable<TodoNote> GetTodoNote()
        {
            return _context.TodoNotes;
        }

        // GET: api/TodoNotes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoNote = await _context.TodoNotes.SingleOrDefaultAsync(m => m.Id == id);

            if (todoNote == null)
            {
                return NotFound();
            }

            return Ok(todoNote);
        }

        // PUT: api/TodoNotes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoNote([FromRoute] int id, [FromBody] TodoNote todoNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todoNote.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoNoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoNotes
        [HttpPost]
        public async Task<IActionResult> PostTodoNote([FromBody] TodoNote todoNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TodoNotes.Add(todoNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoNote", new { id = todoNote.Id }, todoNote);
        }

        // DELETE: api/TodoNotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoNote = await _context.TodoNotes.SingleOrDefaultAsync(m => m.Id == id);
            if (todoNote == null)
            {
                return NotFound();
            }

            _context.TodoNotes.Remove(todoNote);
            await _context.SaveChangesAsync();

            return Ok(todoNote);
        }

        private bool TodoNoteExists(int id)
        {
            return _context.TodoNotes.Any(e => e.Id == id);
        }
    }
}