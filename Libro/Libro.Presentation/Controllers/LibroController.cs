﻿using Microsoft.AspNetCore.Mvc;

namespace Libro.Presentation.Controllers
{
    public class LibroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
