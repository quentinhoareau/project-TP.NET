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
        public ICommand NextPageCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }
        
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        private int _currentPageIndex = 0;
        private readonly int _itemsPerPage = 6;
        private int _totalBooks = 0;
        public ICommand ItemSelectedCommand { get; }

        public ListBooks()
        {
            NextPageCommand = new RelayCommand(NextPage, () => {
                return _totalBooks > (_currentPageIndex + 1) * _itemsPerPage;
            });
            PreviousPageCommand = new RelayCommand(PreviousPage, () => {
                return _currentPageIndex > 0;
            });
             LoadBooks(_itemsPerPage, _currentPageIndex * _itemsPerPage);
             
             ItemSelectedCommand = new Command<Book>(ShowDetails);
        }

        private async void LoadBooks(int limit, int offset)
        {
            LibraryService.BooksPaginate booksPaginate = await Ioc.Default.GetRequiredService<LibraryService>().GetAllBooks(limit, offset);
            _totalBooks = booksPaginate.TotalBooks;
            UpdatePagesButton();
            Books.Clear();
            foreach (var book in booksPaginate.Books)
            {
                Books.Add(book);
            }
        }
        
        public void UpdatePagesButton()
        {
            // NextPageCommand.
            // (NextPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
            // (PreviousPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        public void ShowDetails(Book book)
        {
            Ioc.Default.GetRequiredService<INavigationService>().Navigate<DetailsBook>(book);
        }
        private void NextPage()
        {
            _currentPageIndex++;
            LoadBooks(_itemsPerPage, _currentPageIndex * _itemsPerPage);
            UpdatePagesButton();
        }
        private void PreviousPage()
        {
            _currentPageIndex--;
            LoadBooks(_itemsPerPage, _currentPageIndex * _itemsPerPage);
            UpdatePagesButton();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
