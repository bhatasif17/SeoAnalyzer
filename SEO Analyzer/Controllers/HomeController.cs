/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SEO_Analyzer.Models;
using System.Diagnostics;

namespace SEO_Analyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
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
