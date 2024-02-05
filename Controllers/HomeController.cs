using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using System.Linq;

namespace MyWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _dbContext;

        public HomeController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "This is the About page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "This is the Contact page.";

            return View();
        }

        [HttpGet]
        public IActionResult GetVeriler()
        {
            var veriler = _dbContext.NemSicaklikVerileri.ToList();
            return View(veriler);
        }
    }
}
