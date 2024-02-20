using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.Server.ViewModels
{
    // Un ViewModel représente le contenu envoyé à la vu mais aussi le contenu récupéré a travers une requète HTTP:
    // -> /api/1.0.0/xxx?prop1=123&prop2=azerty&prop3[]=0&prop3[]=1
    public class EditBookViewModel
    {
        public int Id { get; set; }
        
        [Required (ErrorMessage = "You need to add a title to the book.")]
        public String Name { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre

        // Liste des genres séléctionné par l'utilisateur
        [Required(ErrorMessage = "You need at least 1 genre"), MinLength(1)]
        public IEnumerable<int> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init; }
        
        [Required (ErrorMessage = "You need to add a content to the book.")]
        public String Content { get; set; }
        
        [Required (ErrorMessage = "You need to add a author to the book.")]
        public IEnumerable<int> Authors { get; set; }
        public IEnumerable<Author> AllAuthors { get; init; }
        
        [Required (ErrorMessage = "You need to add a price to the book.")]
        public Decimal Price { get; set; }
        
    }
}
