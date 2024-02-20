using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace ASP.Server.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
