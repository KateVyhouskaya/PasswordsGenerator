using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PasswordGenerator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace PasswordGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static readonly Random rnd = new Random();

        static string GetPassword(int passwordSize)
        {
            const string alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return string.Join(string.Empty, Enumerable.Range(0, passwordSize).Select(x => alpha[rnd.Next(0, alpha.Length)]));
        }

        [HttpGet("/api")]
        public JsonResult GetPasswordsList([FromQuery(Name = "passwordSize")] int passwordSize, [FromQuery(Name = "passwordQuantity")] int passwordQuantity)
        {
            var passwords = Enumerable.Range(0, passwordQuantity).Select(x => GetPassword(passwordSize)).ToList();
            return Json(new { passwords = passwords });
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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
