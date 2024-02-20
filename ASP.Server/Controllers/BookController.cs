using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ASP.Server.Models;
using ASP.Server.ViewModels;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace ASP.Server.Controllers
{
    public class BookController(LibraryDbContext libraryDbContext, IMapper mapper) : Controller
    {
        private readonly LibraryDbContext libraryDbContext = libraryDbContext;

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            IEnumerable<Book> ListBooks = libraryDbContext.Books.
                Include(p => p.Genres)
                .Include(a => a.Authors).ToList();
            return View(ListBooks);
        }

        public ActionResult<CreateBookViewModel> Create(CreateBookViewModel book)
        {
            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookViewModel() { AllGenres = libraryDbContext.Genres, AllAuthors = libraryDbContext.Authors});
        }

        public ActionResult<CreateBookViewModel> Insert(CreateBookViewModel book)
        {
            if (ModelState.IsValid)
            {
                var bookToAdd = new Book()
                {
                    Name = book.Name,
                    Content = book.Content,
                    Authors = libraryDbContext.Authors.Where(a => book.Authors.Contains(a.Id)).ToList(),
                    Price = book.Price,
                    Genres = libraryDbContext.Genres.Where(p => book.Genres.Contains(p.Id)).ToList()
                };
                libraryDbContext.Add(bookToAdd);
                libraryDbContext.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult<CreateBookViewModel> Edit(int id)
        {
            var bookToUpdate = libraryDbContext.Books
                .Include(p => p.Genres)
                .Include(a => a.Authors).FirstOrDefault(p => p.Id == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
        
            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new EditBookViewModel(){
                Id = id,
                Name = bookToUpdate.Name,
                Content = bookToUpdate.Content,
                Authors = bookToUpdate.Authors.Select(p => p.Id),
                Price = bookToUpdate.Price,
                Genres = bookToUpdate.Genres.Select(p => p.Id),
                AllGenres = libraryDbContext.Genres,
                AllAuthors = libraryDbContext.Authors
            });
        }
        
        public ActionResult<EditBookViewModel> Update(EditBookViewModel book)
        {
            var bookToUpdate = libraryDbContext.Books.Include(p => p.Genres).FirstOrDefault(p => p.Id == book.Id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                bookToUpdate.Id = book.Id;
                bookToUpdate.Name = book.Name;
                bookToUpdate.Content = book.Content;
                bookToUpdate.Authors = libraryDbContext.Authors.Where(p => book.Authors.Contains(p.Id)).ToList();
                bookToUpdate.Price = book.Price;
                bookToUpdate.Genres = libraryDbContext.Genres.Where(p => book.Genres.Contains(p.Id)).ToList();
                libraryDbContext.SaveChanges();
            }
            return RedirectToAction("List");
        }
        
        public ActionResult<CreateBookViewModel> Delete(int id, CreateBookViewModel book)
        {
            var bookToDelete = libraryDbContext.Books.Find(id);

            if (bookToDelete == null)
            {
                return NotFound();
            }
            libraryDbContext.Remove(bookToDelete);
            libraryDbContext.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
