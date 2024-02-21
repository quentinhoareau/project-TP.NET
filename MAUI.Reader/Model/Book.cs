

using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUI.Reader.Model
{
    // A vous de completer ce qu'est un Livre !!
    // /!\ ATTENTION ! Si vous récupéré les livres depuis votre serveur, cette classe ne sert plus a rien !
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public double Price { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}
