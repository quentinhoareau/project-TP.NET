using System;
using System.Collections.Generic;
using ASP.Server.Models;
using Newtonsoft.Json;

namespace ASP.Server.Mapper;

public static class Mapper
{
    public static Book ToBook(string jsonContent)
    {
        var json = JsonConvert.DeserializeObject<dynamic>(jsonContent);

        var name = json.title ?? "No title available";
        var content = json.description ?? "No description available";
        
        var book = new Book
        {
            Name = name,
            Content = content,
            Price = 0,
        };
        
        return book;
    }
    
    public static string ToAuthorKey(string jsonContent)
    {
        var json = JsonConvert.DeserializeObject<dynamic>(jsonContent);

        var key = json.authors[0].author.key;
        
        return key;
    }
    
    public static List<Genre> ToGenres(string jsonContent)
    {
        var json = JsonConvert.DeserializeObject<dynamic>(jsonContent);
        
        var genres = new List<Genre>();
        foreach (string genre in json.subjects)
        {
            genres.Add(new Genre
            {
                Name = genre,
            });
            // On limite à 5 genres car l'API OpenLibrary retourne énormement de genres et on ne veut pas surcharger notre base de données
            if (genres.Count == 5)
            {
                break;
            }
        }
        
        return genres;
    }
    
    public static ICollection<Author> ToAuthors(string jsonContent)
    {
        var json = JsonConvert.DeserializeObject<dynamic>(jsonContent);

        var authors = new List<Author>();
        string fullName = json.name;
        var names = fullName.Split(' ');
        authors.Add(new Author
        {
            FirstName = names[0],
            LastName = names[1],
        });
        
        return authors;
    }
}