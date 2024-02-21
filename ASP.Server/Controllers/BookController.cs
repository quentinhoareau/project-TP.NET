using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using ASP.Server.Models;
using ASP.Server.ViewModels;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.OpenApi.Any;

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
            ViewBag.FilterBy = filterBy;
            if (filterBy == "author")
            {
                listBooks = listBooks.OrderBy(p => p.Authors.Select(a => a.FullName).FirstOrDefault()).ToList();
                ViewBag.FilterOptions = libraryDbContext.Authors;
            }
            else if (filterBy == "genre")
            {
                listBooks = listBooks.OrderBy(p => p.Genres.Select(g => g.Name).FirstOrDefault()).ToList();
                ViewBag.FilterOptions = libraryDbContext.Genres;
            }
            return View(new FilterBookViewModel()
            {
                Books = listBooks,
            });
        }
        
        public ActionResult<int> GetBookCount()
        {
            return libraryDbContext.Books.Count();
        }
        
        public ActionResult<int> GetBookCountByAuthor(int authorId)
        {
            return libraryDbContext.Books.Where(p => p.Authors.Any(a => a.Id == authorId)).Count();
        }
        
        public ActionResult<object> GetStatsOfBookById(int bookId)
        {
            int maxWords = 0;
            int minWords = 0;
            double avgWords = 0;
            int averageWords;
            var book = libraryDbContext.Books.Find(bookId);
            if (book != null)
            {
                var words = book.Content.Split(' ');
                maxWords = words.Max(p => p.Length);
                minWords = words.Min(p => p.Length);
                avgWords = words.Average(p => p.Length);
            }
            return new
            {
                MaxWords = maxWords,
                MinWords = minWords,
                AvgWords = avgWords
            };
        }
        
        public ActionResult<IEnumerable<Book>> Index(string filterBy)
        {
            return RedirectToAction("List", new { filterBy=filterBy});
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
            var bookToUpdate = libraryDbContext.Books
                .Include(p => p.Genres).Include(p => p.Authors).FirstOrDefault(p => p.Id == book.Id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
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
