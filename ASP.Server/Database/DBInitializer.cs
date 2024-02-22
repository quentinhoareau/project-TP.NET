using System.Collections.Generic;
using System.Linq;
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
            Author SB, JRR, EC, JK, SK, GO, HL, JA, FSF, JDSS, CSL;
            bookDbContext.Authors.AddRange(
                SB = new Author() { FirstName = "Stendhal", LastName = "Beyle" },
                JRR = new Author() { FirstName = "J.R.R.", LastName = "Tolkien" },
                EC =new Author() { FirstName = "Eoin", LastName = "Colfer" },
                JK =new Author() { FirstName = "J.K.", LastName = "Rowling" },
                SK =new Author() { FirstName = "Stephen", LastName = "King" },
                GO = new Author() { FirstName = "George", LastName = "Orwell" },
                HL = new Author() { FirstName = "Harper", LastName = "Lee" },
                JA = new Author() { FirstName = "Jane", LastName = "Austen" },
                FSF = new Author() { FirstName = "F. Scott", LastName = "Fitzgerald" },
                JDSS = new Author() { FirstName = "J.D.", LastName = "Salinger" },
                CSL = new Author() { FirstName = "C.S.", LastName = "Lewis" }
            );
            bookDbContext.SaveChanges(); // Sauvegarde pour obtenir des ID pour les genres
            
            // Création des auteurs
            Genre SF, Classic, Romance, Thriller, Fantasy, Adventure;
            bookDbContext.Genres.AddRange(
                SF = new Genre() { Name = "Science Fiction"},
                Classic = new Genre() { Name = "Classic" },
                Romance = new Genre() { Name = "Romance" },
                Thriller = new Genre() { Name = "Thriller" },
                Fantasy = new Genre() { Name = "Fantasy" },
                Adventure = new Genre() { Name = "Adventure" }
            );
            bookDbContext.SaveChanges(); // Sauvegarde pour obtenir des ID pour les genres

            // Création des livres avec affectation des genres
            bookDbContext.Books.AddRange(
                new Book() {
                    Name = "1984",
                    Authors = new List<Author>() { GO },
                    Price = 9.99,
                    Content = "1984 est un roman dystopique où la liberté individuelle est réprimée par un régime totalitaire. Il suit l'histoire de Winston Smith, un homme qui résiste à la domination du Parti.",
                    Genres = new List<Genre>() { SF, Classic },
                },
                new Book() {
                    Name = "To Kill a Mockingbird",
                    Authors = new List<Author>() { HL },
                    Price = 11.49,
                    Content = "To Kill a Mockingbird est un roman qui aborde des questions de race et d'injustice dans le sud des États-Unis pendant les années 1930, à travers les yeux de la jeune Scout Finch.",
                    Genres = new List<Genre>() { SF, Classic },
                },
                new Book() {
                    Name = "Le rouge et le noir",
                    Authors = new List<Author>() { SB },
                    Price = 10.99,
                    Content = "Julien Sorel, jeune homme ambitieux, est engagé comme précepteur chez le maire de Verrières, M. de Rênal. Il séduit la femme de son employeur, puis la fille de celui-ci, Mathilde.",
                    Genres = new List<Genre>() { Classic },
                },
                new Book() {
                    Name = "Le seigneur des anneaux",
                    Authors = new List<Author>() { JRR },
                    Price = 15.99,
                    Content = "Un anneau pour les gouverner tous, un anneau pour les trouver, un anneau pour les amener tous et dans les ténèbres les lier",
                    Genres = new List<Genre>() { SF } 
                },
                new Book() {
                    Name = "Artémis Fowl",
                    Authors = new List<Author>() { EC, SB },
                    Price = 12.99,
                    Content = "Artémis Fowl est un génie de douze ans, un peu garçon, un peu adulte, et surtout... un peu voleur. Il a découvert l'existence des fées et compte bien s'en servir pour s'enrichir.",
                    Genres = new List<Genre>() { SF, Thriller } 
                },
                new Book() {
                    Name = "Harry Potter",
                    Authors = new List<Author>() { SK },
                    Price = 14.99,
                    Content = "Harry Potter est un jeune orphelin qui découvre à l'âge de onze ans qu'il est un sorcier. Il est invité à étudier la magie à Poudlard, une école de sorcellerie.",
                    Genres = new List<Genre>() { Fantasy, Classic },
                },
                new Book() {
                    Name = "Pride and Prejudice",
                    Authors = new List<Author>() { JA },
                    Price = 8.99,
                    Content = "Pride and Prejudice suit l'histoire d'Elizabeth Bennet alors qu'elle navigue à travers les complexités de l'amour, de la classe sociale et de la morale dans la société anglaise du XIXe siècle.",
                    Genres = new List<Genre>() { Romance, Classic },
                },
                new Book() {
                    Name = "The Great Gatsby",
                    Authors = new List<Author>() { FSF },
                    Price = 10.99,
                    Content = "The Great Gatsby est un récit sur l'illusion du rêve américain dans les années folles. Il explore les thèmes de l'amour, de la richesse, de l'obsession et de la trahison.",
                    Genres = new List<Genre>() { SF, Classic },
                },
                new Book() {
                    Name = "The Catcher in the Rye",
                    Authors = new List<Author>() { JDSS },
                    Price = 9.49,
                    Content = "The Catcher in the Rye suit les aventures de Holden Caulfield, un adolescent rebelle qui cherche un sens à sa vie à travers des expériences désillusionnantes dans la société.",
                    Genres = new List<Genre>() { SF },
                },
                new Book() {
                    Name = "The Hobbit",
                    Authors = new List<Author>() { JK },
                    Price = 11.99,
                    Content = "The Hobbit suit Bilbo Baggins, un hobbit tranquille, alors qu'il est entraîné dans une quête épique pour reconquérir le trésor gardé par le dragon Smaug.",
                    Genres = new List<Genre>() { Fantasy, Adventure },
                },
                new Book() {
                    Name = "The Chronicles of Narnia",
                    Authors = new List<Author>() { CSL },
                    Price = 13.99,
                    Content = "The Chronicles of Narnia est une série de sept romans qui suivent les aventures d'enfants qui découvrent un monde magique appelé Narnia, rempli de créatures mythiques et de merveilles.",
                    Genres = new List<Genre>() { Fantasy, Adventure },
                },
                new Book() {
                    Name = "Crime and Punishment",
                    Authors = new List<Author>() { SB },
                    Price = 12.99,
                    Content = "Crime and Punishment suit l'histoire de Rodion Raskolnikov, un étudiant russe tourmenté par sa conscience après avoir commis un meurtre. Le roman explore les thèmes de la culpabilité et de la rédemption.",
                    Genres = new List<Genre>() { SF },
                },
                new Book() {
                    Name = "The Picture of Dorian Gray",
                    Authors = new List<Author>() {  },
                    Price = 10.49,
                    Content = "The Picture of Dorian Gray raconte l'histoire de Dorian Gray, un jeune homme dont le portrait vieillit à sa place alors qu'il reste éternellement jeune. Le roman explore les thèmes de la vanité, de la morale et de la décadence.",
                    Genres = new List<Genre>() { Classic },
                },
                new Book() {
                    Name = "Moby-Dick",
                    Authors = new List<Author>() { FSF },
                    Price = 11.99,
                    Content = "Moby-Dick suit le capitaine Achab dans sa quête obsessionnelle de vengeance contre le cachalot blanc géant Moby Dick. Le roman explore les thèmes de l'obsession, de la folie et de la destinée.",
                    Genres = new List<Genre>() { Adventure, Classic },
                },
                new Book() {
                    Name = "Brave New World",
                    Authors = new List<Author>() { JA },
                    Price = 9.99,
                    Content = "Brave New World est un roman dystopique qui explore une société future où la technologie et le conditionnement social ont éliminé les souffrances et les inégalités, mais au prix de la liberté individuelle.",
                    Genres = new List<Genre>() {  SF },
                },
                new Book() {
                    Name = "The Count of Monte Cristo",
                    Authors = new List<Author>() { SK },
                    Price = 13.99,
                    Content = "The Count of Monte Cristo suit l'histoire d'Edmond Dantès, un homme injustement emprisonné qui cherche vengeance contre ceux qui l'ont trahi. Le roman est un récit d'aventure et de justice.",
                    Genres = new List<Genre>() { Adventure, Classic },
                }
            );
            bookDbContext.SaveChanges();
        }
    }
}
