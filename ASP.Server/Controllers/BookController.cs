using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using System.Collections.Generic;
using System.Linq;
using ASP.Server.Models;
using ASP.Server.Service;
using ASP.Server.ViewModels;
using AutoMapper;

namespace ASP.Server.Controllers
{
    public class BookController(LibraryDbContext libraryDbContext, IMapper mapper) : Controller
    {
        private readonly LibraryDbContext libraryDbContext = libraryDbContext;

        public ActionResult<IEnumerable<Book>> List([FromQuery] string filterBy = "author", [FromQuery] int BookCountByAuthor = -1)
        {
            // récupérer les livres dans la base de données pour qu'elle puisse être affiché
            var listBooks = libraryDbContext.Books
                .Include(p => p.Genres)
                .Include(a => a.Authors).ToList();
            ViewBag.FilterBy = filterBy;
            if(BookCountByAuthor != -1)
            {
                ViewBag.BookCountByAuthor = BookCountByAuthor;
            }
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
                Authors = libraryDbContext.Authors,
                NbBooks = GetBookCount(),
            });
        }
        
        public int GetBookCount()
        {
            return libraryDbContext.Books.Count();
        }
        
        public ActionResult<int> GetBookCountByAuthor(int authorId)
        {
            var count = libraryDbContext.Books.Where(p => p.Authors.Any(a => a.Id == authorId)).Count();
            return RedirectToAction("List", new { BookCountByAuthor = count });
        }
        
        public ActionResult<IEnumerable<Book>> Index(string filterBy)
        {
            return RedirectToAction("List", new { filterBy=filterBy});
        }

        public ActionResult<CreateBookViewModel> Create(CreateBookViewModel book)
        {
            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookViewModel()
            {
                AllGenres = libraryDbContext.Genres, 
                AllAuthors = libraryDbContext.Authors
            });
        }
        
        public ActionResult<CreateBookISBNViewModel> CreateIsbn(CreateBookISBNViewModel book)
        {
            return View(new CreateBookISBNViewModel());
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
        
        public ActionResult<CreateBookISBNViewModel> InsertIsbn(CreateBookISBNViewModel bookIsbn)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // utilisation d'un service pour mapper les objets et récupérer proprement les données.
                    LibraryService libraryService = new LibraryService();
                    var book = libraryService.GetInfoBooksByIsbn(bookIsbn.Isbn).Result;
                    if (book != null)
                    {
                        // Vérifier si les genres existent déjà dans la base de données
                        if (libraryDbContext.Genres.Where(p => book.Genres.Select(g => g.Name).Equals(p.Name)).ToList().Count != 0)
                        {
                            // S'il l'existe, on les récupère de la base de données
                            book.Genres = libraryDbContext.Genres.Where(p => book.Genres.Select(g => g.Name).Contains(p.Name)).ToList();
                        }

                        // Vérifier si les auteurs existent déjà dans la base de données 
                        var authors = libraryDbContext.Authors
                            .Where(p => book.Authors.Select(a => a.LastName).Contains(p.LastName)).ToList().Count;
                        if (authors != 0)
                        {
                            // S'il l'existe, on les récupère de la base de données
                            book.Authors = libraryDbContext.Authors.Where(p => book.Authors.Select(a => a.LastName).Contains(p.LastName)).ToList();
                        }
                        libraryDbContext.Books.Add(book);
                        libraryDbContext.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("CreateIsbn");
                    }
                    return RedirectToAction("List");
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                    return RedirectToAction("CreateIsbn");
                }
            }
            return RedirectToAction("CreateIsbn");
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
        
            // Il faut interroger la base pour récupérer tous les genres, pour que l'utilisateur puisse les sélectionner
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
