

using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUI.Reader.Model
{
    public class Genre: ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}