﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using ASP.Server.ViewModels;
using AutoMapper;

namespace ASP.Server.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IMapper mapper) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IMapper mapper = mapper;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
