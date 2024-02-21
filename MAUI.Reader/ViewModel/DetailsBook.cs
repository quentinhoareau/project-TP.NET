using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.DependencyInjection;
using MAUI.Reader.Model;
using MAUI.Reader.Service;

namespace MAUI.Reader.ViewModel
{
    public partial class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Une commande permet de recevoir des évènement de l'IHM
        public ICommand ReadBook2Command { get; }
      
        public DetailsBook(Book book)
        {
            CurrentBook = book;
            LoadBookToRead(book);
            ReadBook2Command =  new Command<Book>(ReadTheBook);
        }

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public Book CurrentBook { get; }
        public Book BookToRead { get; set; }
        public void ReadTheBook(Book book)
        {
            /* A vous de définir la commande */
            Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(BookToRead);
            
        }

        private async void LoadBookToRead(Book book)
        {
            BookToRead = await Ioc.Default.GetRequiredService<LibraryService>().GetBookById(book.Id);
        }
        
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new Book()) { }
    }
}
