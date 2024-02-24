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
        private int PageSize = 6;
        private int _currentPage = 1;
        
        public ObservableCollection<Book> Books { get; private set; } = new ObservableCollection<Book>();
        public ICommand ItemSelectedCommand { get; }
        public bool IsNextButtonEnabled { get; set; } = true;
        public bool IsPreviousButtonEnabled { get; set; } = false;

        public ListBooks()
        {
             LoadBooks();
             
             ItemSelectedCommand = new Command<Book>(ShowDetails);
        }
        private void UpdateBooks(List<Book> allBooks)
        {
            Books.Clear();
            foreach (var book in allBooks)
            {
                Books.Add(book);
            }
        }
        [RelayCommand]
        public void NextPage()
        {
            _currentPage++;
            LoadBooks();
            UpdateButtonStates();
        }
        [RelayCommand]
        public void PreviousPage()
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadBooks();
                UpdateButtonStates();
            }
        }
        private void UpdateButtonStates()
        {
            // Mettre à jour l'état des boutons en fonction de la page actuelle
            IsNextButtonEnabled = _currentPage < PageSize;
            IsPreviousButtonEnabled = _currentPage > 1;

        }


        private async void LoadBooks()
        {
            int offset = (_currentPage - 1) * PageSize;
            int limit = PageSize;
            LibraryService.BooksPaginate booksPaginate = await Ioc.Default.GetRequiredService<LibraryService>().GetAllBooks(limit, offset);
            PageSize = booksPaginate.Books.Count();
            UpdateBooks(booksPaginate.Books);
        }

        [RelayCommand]
        public void ShowDetails(Book book)
        {
            Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(book);
        }
    }
}
