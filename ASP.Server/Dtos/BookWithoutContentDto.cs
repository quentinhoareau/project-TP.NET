using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Newtonsoft.Json;

namespace ASP.Server.Dtos
{
    public class BookWithoutContentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AuthorDto> Authors { get; set; }
        public decimal Price { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
    }
}
