using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.Server.ViewModels
{
    public class CreateGenreViewModel
    {
        [Required (ErrorMessage = "You need to add a title to the book.")]
        public String Name { get; set; }
       
        public IEnumerable<int> Books { get; set; } = new List<int>();
        
        public IEnumerable<Book> AllBooks { get; init; }
    }
}
