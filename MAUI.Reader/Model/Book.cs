﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace MAUI.Reader.Model
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        
        // Mettez ici les propriété de votre livre: Nom, Autheur, Prix, Contenu et Genres associés
        // N'oublier pas qu'un livre peut avoir plusieur genres
        
        public string Name { get; set; }

        public  ICollection<Author> Authors { get; set; }

        public decimal Price { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}