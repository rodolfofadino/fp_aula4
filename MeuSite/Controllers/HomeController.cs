using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MeuSite.Models;

namespace MeuSite.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var recipe = _repository.List(3);
            if (!recipe.Any())
                return View("NoRecipes");

            return View(recipe);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Repository : IRepository
    {
        public List<Recipe> List(int total)
        {
            //demora bastante e vai no banco
            return new List<Recipe>() { new Recipe() { Id = 123, Name = "Bolo" } };
        }
    }
    public interface IRepository
    {
        List<Recipe> List(int total);
    }
}
