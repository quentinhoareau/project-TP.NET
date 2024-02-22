using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.ViewModels;
using System.Collections.Generic;
using System.Linq;
using ASP.Server.Models;
using AutoMapper;

namespace ASP.Server.Controllers
{
    public class GenreController(LibraryDbContext libraryDbContext, IMapper mapper) : Controller
    {
        private readonly LibraryDbContext libraryDbContext = libraryDbContext;
        private readonly IMapper mapper = mapper;

        // A vous de faire comme BookController.List mais pour les genres !
        public  ActionResult<IEnumerable<Genre>> List()
        {
            IEnumerable<Genre> ListGenres = libraryDbContext.Genres.Include(p => p.Books).ToList();
            return View(ListGenres);
        }

        public ActionResult<CreateGenreViewModel> Create(CreateGenreViewModel genre)
        {
            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateGenreViewModel() { AllBooks = libraryDbContext.Books });
        }
        
        public ActionResult<CreateGenreViewModel> Insert(CreateGenreViewModel genre)
        {
            if (ModelState.IsValid)
            {
                var genreToAdd = new Genre()
                {
                    Name = genre.Name,
                    Books = genre.Books.Select(p => libraryDbContext.Books.Find(p)).ToList()
                };
                libraryDbContext.Add(genreToAdd);
                libraryDbContext.SaveChanges();
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return RedirectToAction("List");
        }
    }
}
