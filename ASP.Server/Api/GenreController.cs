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
    public class GenreController(LibraryDbContext libraryDbContext, IMapper mapper) : ControllerBase
    {
        private readonly LibraryDbContext libraryDbContext = libraryDbContext;
        private readonly IMapper mapper = mapper;
        
        [HttpGet("/api/genres")]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres([FromQuery] List<int> genreIds, [FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            IQueryable<Genre> query = libraryDbContext.Genres;
            var genres = await query
                .Skip(offset)
                .Take(limit)
                .ProjectTo<GenreDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(genres);
        }
        
        [HttpGet("/api/genre/{id}")]
        public async Task<ActionResult<GenreDto>> GetGenreById(int id)
        {
            var genre = await libraryDbContext.Genres
                .Where(b => b.Id == id)
                .ProjectTo<GenreDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

    }
}

