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
    public class AuthorController(LibraryDbContext libraryDbContext, IMapper mapper) : Controller
    {
        private readonly LibraryDbContext libraryDbContext = libraryDbContext;

        public ActionResult<IEnumerable<Author>> List()
        {
            // récupérer les auteurs dans la base de données pour qu'elle puisse être affiché
            var listAuthors = libraryDbContext.Authors.ToList();
            return View(listAuthors);
        }
        
        public ActionResult<CreateAuthorViewModel> Create(CreateAuthorViewModel author)
        {
            return View(new CreateAuthorViewModel{
                AllBooks = libraryDbContext.Books.ToList(),
            });
        }
        
        public ActionResult<CreateAuthorViewModel> Insert(CreateAuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                var authorToAdd = new Author()
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Book = libraryDbContext.Books.Where(a => author.Books.Contains(a.Id)).ToList(),
                };
                libraryDbContext.Add(authorToAdd);
                libraryDbContext.SaveChanges();
            }
            else
            {
                return RedirectToAction("Create");
            }
            return RedirectToAction("List");
        }
        
        public ActionResult<CreateAuthorViewModel> Edit(int id)
        {
            var author = libraryDbContext.Authors.Include(b => b.Book).FirstOrDefault(p => p.Id == id);
            return View("Edit", new EditAuthorViewModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Books = author.Book.Select(p => p.Id),
                AllBooks = libraryDbContext.Books.ToList(),
            });
        }
        
        public ActionResult<CreateAuthorViewModel> Update(EditAuthorViewModel author)
        {
            var authorToUpdate = libraryDbContext.Authors.Include(b => b.Book).FirstOrDefault(p => p.Id == author.Id);
            if (authorToUpdate == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                authorToUpdate.FirstName = author.FirstName;
                authorToUpdate.LastName = author.LastName;
                authorToUpdate.Book = libraryDbContext.Books.Where(p => author.Books.Contains(p.Id)).ToList();
                libraryDbContext.SaveChanges();
            }
            else
            {
                return RedirectToAction("Edit", new { id = author.Id });
            }
            return RedirectToAction("List");
        }
    }
}
