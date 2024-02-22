using System.Collections.Generic;

namespace ASP.Server.Dtos
{
    public class BookWithoutContentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AuthorDto> Authors { get; set; }
        public double Price { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
    }
}
