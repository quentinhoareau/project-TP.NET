using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Server.Database;
using ASP.Server.Models;
using AutoMapper;
using ASP.Server.Dtos;
using AutoMapper.QueryableExtensions;

namespace ASP.Server.Api
{

    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class BookController(LibraryDbContext libraryDbContext, IMapper mapper) : ControllerBase
    {
        private readonly LibraryDbContext libraryDbContext = libraryDbContext;
        private readonly IMapper mapper = mapper;
        
        [HttpGet("/api/book")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks([FromQuery] List<int> genre, [FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            IQueryable<Book> query = libraryDbContext.Books;

            if (genre != null && genre.Count > 0)
            {
                query = query.Where(b => b.Genres.Any(g => genre.Contains(g.Id)));
            }

            var totalBooks = await query.CountAsync(); 

            var books = await query
                .Skip(offset)
                .Take(limit)
                .ProjectTo<BookWithoutContentDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            var paginationHeader = $"{offset + 1}-{offset + books.Count}/{totalBooks}";

            Response.Headers.Add("Pagination", paginationHeader);
            return Ok(books);
        }
        
        [HttpGet("/api/book/{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await libraryDbContext.Books
                .Where(b => b.Id == id)
                .ProjectTo<BookDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

    }
}

