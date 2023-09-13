using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreMVC_WebAPI.Data;
using CoreMVC_WebAPI.Models;
using CoreMVC_WebAPI.DTOs;

namespace CoreMVC_WebAPI.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly PubsContext _context;

        public AuthorsController(PubsContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
          //return await _context.Authors.ToListAsync();

          var res = await (from author in _context.Authors
                    select new AuthorDTO
                    {
                        au_id=author.au_id,
                        au_fname=author.au_fname,
                        au_lname=author.au_lname,
                        Address=author.Address,
                        Phone=author.Phone,
                        Zip=author.Zip,
                        City=author.City,
                        Contract=author.Contract,
                        State=author.State,
                    }).ToListAsync();
          return Ok(res);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(string id)
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        [Route("state/{state}")]
        public IQueryable<Author> GetauthorsByState(string state)
        {
            //return db.authors;

            var res = from a in _context.Authors
                      where a.State == state
                      select a;

            return res;
        }

        [Route("city/{city}")]
        public IQueryable<Author> GetauthorsByCity(string city)
        {
            //return db.authors;

            var res = from a in _context.Authors
                      where a.City == city
                      select a;

            return res;
        }

        [Route("contract_letter/{contract}/{letter}")]
        public IQueryable<Author> GetauthorsByContractAndNameLetter(bool contract, string letter)
        {
            //return db.authors;

            var res = from a in _context.Authors
                      where a.Contract == contract && a.au_fname.StartsWith(letter)
                      select a;

            return res;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(string id, Author author)
        {
            if (id != author.au_id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

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
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDTO authorDto)
        {
            if (authorDto == null)
            {
                return Problem("No data provided!");
            }
            if (_context.Authors == null)
            {
                return Problem("Entity set 'PubsContext.Authors'  is null.");
            }

            Author author = new Author()
            {
                au_id = authorDto.au_id,
                au_fname = authorDto.au_fname,
                au_lname = authorDto.au_lname,
                Address = authorDto.Address,
                Phone = authorDto.Phone,
                Zip = authorDto.Zip,
                City = authorDto.City,
                Contract = authorDto.Contract,
                State = authorDto.State,
            };

            _context.Authors.Add(author);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuthorExists(author.au_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAuthor", new { id = author.au_id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(string id)
        {
            return (_context.Authors?.Any(e => e.au_id == id)).GetValueOrDefault();
        }
    }
}
