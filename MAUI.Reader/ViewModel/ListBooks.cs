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
        public ListBooks()
        {
            ItemSelectedCommand = new Command(OnItemSelectedCommand);
            LoadBooks();
        }
        public ICommand ItemSelectedCommand { get; private set; }
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        private Book _selectedBook;
        private async void LoadBooks()
        {
            var books = await Ioc.Default.GetRequiredService<LibraryService>().GetAllBooks();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }
        
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (_selectedBook != value)
                {
                    _selectedBook = value;
                    OnPropertyChanged(nameof(SelectedBook));
                }
            }
        }
        public void OnItemSelectedCommand(object book)
        {
            Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(SelectedBook);
        }

        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!

    

        [RelayCommand]
        public void CounterClicked()
        {

            Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(new Book());
        }
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Mettez à jour la propriété SelectedBook lorsque la sélection change
            SelectedBook = e.SelectedItem as Book;
        }
    }
}