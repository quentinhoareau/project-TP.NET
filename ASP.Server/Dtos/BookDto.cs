using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace ASP.Server.Dtos
{
    public class BookDto : BookWithoutContentDto
    {
        public string Content { get; set; }
    }
}
