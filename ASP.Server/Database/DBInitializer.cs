using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ASP.Server.Models;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {
            if (bookDbContext.Books.Any())
                return; // La base de données a déjà été initialisée.

            // Création des genres
            Author SB, JRR, EC, JK, SK;
            bookDbContext.Authors.AddRange(
                SB = new Author() { FirstName = "Stendhal", LastName = "Beyle" },
                JRR = new Author() { FirstName = "J.R.R.", LastName = "Tolkien" },
                EC =new Author() { FirstName = "Eoin", LastName = "Colfer" },
                JK =new Author() { FirstName = "J.K.", LastName = "Rowling" },
                SK =new Author() { FirstName = "Stephen", LastName = "King" }
            );
            bookDbContext.SaveChanges(); // Sauvegarde pour obtenir des ID pour les genres
            
            
            // Création des auteurs
            Genre SF, Classic, Romance, Thriller, Fantasy;
            bookDbContext.Genre.AddRange(
                SF = new Genre() { Name = "Science Fiction" },
                Classic = new Genre() { Name = "Classic" },
                Romance = new Genre() { Name = "Romance" },
                Thriller = new Genre() { Name = "Thriller" },
                Fantasy = new Genre() { Name = "Fantasy" }
            );
            bookDbContext.SaveChanges(); // Sauvegarde pour obtenir des ID pour les genres

            // Création des livres avec affectation des genres
            bookDbContext.Books.AddRange(
                new Book() {
                    Name = "Le rouge et le noir",
                    Authors = new List<Author>() { SB },
                    Price = 10.99m,
                    Content = "Julien Sorel, jeune homme ambitieux, est engagé comme précepteur chez le maire de Verrières, M. de Rênal. Il séduit la femme de son employeur, puis la fille de celui-ci, Mathilde.",
                    Genres = new List<Genre>() { Classic },
                },
                new Book() {
                    Name = "Le seigneur des anneaux",
                    Authors = new List<Author>() { JRR },
                    Price = 15.99m,
                    Content = "Un anneau pour les gouverner tous, un anneau pour les trouver, un anneau pour les amener tous et dans les ténèbres les lier",
                    Genres = new List<Genre>() { SF } 
                },
                new Book() {
                    Name = "Artémis Fowl",
                    Authors = new List<Author>() { EC, SB },
                    Price = 12.99m,
                    Content = "Artémis Fowl est un génie de douze ans, un peu garçon, un peu adulte, et surtout... un peu voleur. Il a découvert l'existence des fées et compte bien s'en servir pour s'enrichir.",
                    Genres = new List<Genre>() { SF, Thriller } 
                },
                new Book() {
                    Name = "Harry Potter",
                    Authors = new List<Author>() { SK },
                    Price = 14.99m,
                    Content = "Harry Potter est un jeune orphelin qui découvre à l'âge de onze ans qu'il est un sorcier. Il est invité à étudier la magie à Poudlard, une école de sorcellerie.",
                    Genres = new List<Genre>() { Fantasy } 
                }
            );
            bookDbContext.SaveChanges(); // Sauvegarde finale avec les relations mises à jour
        }
    }
}
