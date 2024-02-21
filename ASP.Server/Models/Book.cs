using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.Server.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        
        // Mettez ici les propriété de votre livre: Nom, Autheur, Prix, Contenu et Genres associés
        // N'oublier pas qu'un livre peut avoir plusieur genres
        
        public string Name { get; set; }

        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

        public double Price { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}