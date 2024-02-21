using System.Collections.ObjectModel;
using MAUI.Reader.Model;
using MAUI.Reader.Resources.Constants;

namespace MAUI.Reader.Service
{
    public class LibraryService
    {
        private readonly RestClient _restClient = new RestClient();
  
        // C'est aussi ici que vous ajouterez les requête réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
        // Faite bien attention a ce que votre requête réseau ne bloque pas l'interface 
        public LibraryService()
        {
            
        }

        public async Task<List<Book>> GetAllBooks ()
        {
            var response = await _restClient._get(Constants.BookTopic);
            if (response == null)
            {
                new List<Book>();
            }
            string content = await response.ReadAsStringAsync();
            Console.Write(content);
            return Mapper.Mapper.ToBooks(content);
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
