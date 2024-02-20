﻿using ASP.Server.Database;
using ASP.Server.Dtos;
using ASP.Server.Models;
using ASP.Server.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ASP.Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Book, BookWithoutContentDto>();

            CreateMap<Genre, GenreDto>();
            CreateMap<Author, AuthorDto>();
        }
    }
}
