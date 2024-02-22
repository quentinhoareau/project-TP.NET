using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ASP.Server.Models;

namespace ASP.Server.Service
{
    public class LibraryService
    {
        private readonly HttpClient client;
        private const string url = "https://openlibrary.org";
        public LibraryService()
        {
            // Initialisation du client HTTP
            var handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            
            client = new HttpClient(handler);
            client.BaseAddress = new Uri(url);
        }
        
        private async Task<HttpContent> _get(string urlToGet)
        {   
            var response =  await client.GetAsync(url+urlToGet);
            if (response.IsSuccessStatusCode)
            {
                return response.Content;
            }

            return null; 
        }
        
        public async Task<HttpContent> GetAuthorsByIsbn(string urlToGet)
        {
            return await _get(urlToGet+".json");
        }
        
        public async Task<Book> GetInfoBooksByIsbn(string isbn)
        {
            try
            {
                // Récupération des informations du livre à partir de l'ISBN
                var response = await _get($"/isbn/{isbn}.json");
                if (response != null)
                {
                    var content = await response.ReadAsStringAsync();
                    
                    // Mapping des informations du livre à partir du contenu JSON (Récupération du titre, description, etc.)
                    var book = Mapper.Mapper.ToBook(content);
                
                    // Récupération des auteurs à partir de la clé fourni par le contenu JSON
                    var key = Mapper.Mapper.ToAuthorKey(content);
                    var responseAuthors = await GetAuthorsByIsbn(key);
                    if (responseAuthors != null)
                    {
                        var authors = await responseAuthors.ReadAsStringAsync();
                        
                        // Mapping des auteurs à partir du contenu JSON
                        var listAuthors = Mapper.Mapper.ToAuthors(authors);
                        book.Authors = listAuthors;
                    }

                    // Mapping des genres à partir du contenu JSON
                    var genres = Mapper.Mapper.ToGenres(content);
                    if (genres.Count > 0)
                    {
                        book.Genres = genres;
                    }
                
                    return book;
                }
                return null;
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        public static Stats GetStatsOfBookById(Book book)
        {
            int maxWords = 0;
            int minWords = 0;
            double avgWords = 0;
            int medWords = 0;
            if (book != null)
            {
                var words = book.Content.Split(' ');
                maxWords = words.Max(p => p.Length);
                minWords = words.Min(p => p.Length);
                medWords = words.OrderBy(p => p.Length).ElementAt(words.Length / 2).Length;
                avgWords = double.Round(words.Average(p => p.Length), 2);
            }
            return new Stats()
            {
                MaxWords = maxWords,
                MinWords = minWords,
                MedWords = medWords,
                AvgWords = avgWords
            };
        }
    }
}
