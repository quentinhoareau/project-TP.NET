using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MAUI.Reader.Model;
using MAUI.Reader.Service;
using System.Windows.Input;

namespace MAUI.Reader.ViewModel
{
    public partial class ListBooks : INotifyPropertyChanged
    {
        public ICommand ItemSelectedCommand { get; }
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        private Book _selectedBook;
        
        public ListBooks()
        {
            LoadBooks();
            ItemSelectedCommand = new Command<Book>(ShowDetails);
        }
    
        private async void LoadBooks()
        {
            var books = await Ioc.Default.GetRequiredService<LibraryService>().GetAllBooks();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        [RelayCommand]
        public void ShowDetails(Book book)
        {
            //TODO : Replace Books[0] by the "book" parameter
            Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(book);
        }
    }
}