using System.Collections.ObjectModel;
using MAUI.Reader.Model;
using MAUI.Reader.Resources.Constants;

namespace MAUI.Reader.Service
{
    public class LibraryService
    {
        
        public class BooksPaginate
        {
            public List<Book> Books { get; set; }
            public int TotalBooks { get; set; }
        }
        
        
        private readonly RestClient _restClient = new RestClient();
  
        // C'est aussi ici que vous ajouterez les requête réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
        // Faite bien attention a ce que votre requête réseau ne bloque pas l'interface 
        public LibraryService()
        {
            
        }

        public async Task<BooksPaginate> GetAllBooks (int limit = 10, int offset = 0)
        {
            HttpContent response  = await _restClient._get(Constants.BookTopic+"?limit="+limit+"&offset="+offset);
            if (response == null)
            {
                new List<Book>();
            }
            string content = await response.ReadAsStringAsync();
            Console.Write(content);
            
            IEnumerable<string> values;
            var totalBooks = 0;
            
            if (response.Headers.TryGetValues("Pagination", out values))
            {
                var paginationHeader = values.First();
                var parts = paginationHeader.Split('/');
                totalBooks = int.Parse(parts[1]) ;
            }
            
            
            return new BooksPaginate()
            {
                Books = Mapper.Mapper.ToBooks(content),
                TotalBooks = totalBooks
            };
            
        }

        public async Task<Book> GetBookById(int id)
        {
            var response = await _restClient._get(Constants.BookTopic + $"/{id}");
            if (response == null)
            {
                return new Book();
            }

            return Mapper.Mapper.ToBook(await response.ReadAsStringAsync());
            
        }
    }
}
