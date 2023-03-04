using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics;
using TTP.Domain;
using TTP.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TTP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _context.Services
                .Select(_ => new ServiceViewModel
                {
                    Name = _.Name,
                    PathImage = _.ImagePath,
                    Price = _.Price
                })
                .ToListAsync();

            var coaches = await _context.Coachs
                .Select(_ => new CoachViewModel
                {
                    Description = _.Description,
                    ImagePath = _.ImagePath,
                    Level = _.Level,
                    Name = _.Name,
                })
                .ToListAsync();

            var gallerrys = new List<GalleryViewModel>();

            var tournaments = new List<TournnamentViewModel> { };

            var viewModel = new IndexViewModel
            {
                Services = services,
                Coaches = coaches,
                Gallerys = gallerrys,
                Tournnaments = tournaments
            };

            return View(viewModel);
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