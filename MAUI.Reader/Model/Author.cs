namespace MAUI.Reader.Model
{
    // A vous de completer ce qu'est un Livre !!
    // /!\ ATTENTION ! Si vous récupéré les livres depuis votre serveur, cette classe ne sert plus a rien !
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Genre> Book { get; set; } = new List<Genre>();
        public String FullName => $"{FirstName} {LastName}";
    }
}