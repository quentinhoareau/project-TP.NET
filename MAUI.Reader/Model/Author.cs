
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAUI.Reader.Model
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        
        // Mettez ici les propriété de votre livre: Nom, Autheur, Prix, Contenu et Genres associés
        // N'oublier pas qu'un livre peut avoir plusieur genres
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Genre> Book { get; set; } = new List<Genre>();
    }
}