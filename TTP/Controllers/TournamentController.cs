using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TTP.Domain;
using TTP.Models;
using TTP.Settings;

namespace TTP.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ApplicationContext _context;
        private AdminSettings _settings;

        public TournamentController(ApplicationContext context, AdminSettings settings)
        {
            _context = context;
            _settings = settings;
        }

        // GET: TournamentController
        public async Task<IActionResult> Index()
        {
            var tournaments = await _context.Tournaments
                .Where(_ => _.Date > DateTime.Now)
                .Include(_ => _.Players)
                .Select(_ => new TournnamentViewModel
                {
                    Date = _.Date,
                    Description = _.Description,
                    Id = _.Id,
                    MaxPlayers = _.MaxPlayers,
                    MaxRaiting = _.MaxRaiting,
                    Name = _.Name,
                    TournamentType = _.TournamentType,
                    MinPlayers = _.MinPlayers,
                    Count = _.Players == null ? 0 : _.Players.Count()
                })
                .ToListAsync();

            return View("Tournaments", tournaments);
        }

        // GET: TournamentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var tournament = await _context.Tournaments.Include(_ => _.Players).FirstOrDefaultAsync(_ => _.Id == id);

            if (tournament is null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = new TournnamentViewModel
            {
                Date = tournament.Date,
                Description = tournament.Description,
                Id = tournament.Id,
                MaxPlayers = tournament.MaxPlayers,
                Name= tournament.Name,
                MaxRaiting= tournament.MaxRaiting,
                MinPlayers= tournament.MinPlayers,
                TournamentType= tournament.TournamentType
            };

            if (tournament.Players != null)
                viewModel.Players = tournament.Players;

            return View("Tournament", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(int id, string typeTournament, string nameFirst, string phoneFirst, string nameLast = null)
        {
            var errorModel = new ErrorViewModel();

            var tournament = await _context.Tournaments.Include(_ => _.Players).FirstOrDefaultAsync(_ => _.Id == id);

            if (tournament is null)
            {
                errorModel.ErrorMessage = "Турнир не найден";

                return View("Error", errorModel);
            }

            if (string.IsNullOrEmpty(nameFirst) || string.IsNullOrEmpty(phoneFirst))
            {
                errorModel.ErrorMessage = "Имя или номер телефона не заполнены";

                return View("Error", errorModel);
            }

            if (string.Equals(typeTournament, "Командный", StringComparison.OrdinalIgnoreCase) && (string.IsNullOrEmpty(nameFirst) || string.IsNullOrEmpty(phoneFirst) || string.IsNullOrEmpty(nameLast)))
            {
                errorModel.ErrorMessage = "Заполнены не все поля";

                return View("Error", errorModel);
            }

            nameFirst = nameFirst.Trim();

            if (tournament.Players.Any(_ => string.Equals(_.FullName, nameFirst, StringComparison.OrdinalIgnoreCase)))
            {
                return await Details(id);
            }

            tournament.Players ??= new List<Player>();

            var teamId = Guid.NewGuid();

            var playerFirst = new Player
            {
                FullName= nameFirst,
                Phone= phoneFirst,
                Raiting = 0,
                TeamId= teamId,
                UserId = 0
            };

            playerFirst.Raiting = await GetRaiting(nameFirst);

            tournament.Players.Add(playerFirst);

            if (typeTournament == "КОМАНДНЫЙ")
            {
                var playerLast = new Player
                {
                    FullName = nameLast,
                    Phone = phoneFirst,
                    Raiting = 0,
                    TeamId = teamId,
                    UserId = 0
                };

                playerLast.Raiting = await GetRaiting(nameLast);

                tournament.Players.Add(playerLast);
            }

            await _context.SaveChangesAsync();

            return await Details(id);
        }

        // GET: TournamentController/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Без админки пока что, чисто по ключу
        /// </summary>
        /// <param name="date"></param>
        /// <param name="description"></param>
        /// <param name="minPlayers"></param>
        /// <param name="maxPlayers"></param>
        /// <param name="maxRaiting"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(DateTime date, string description, int minPlayers, int maxPlayers, int maxRaiting, string name, string type, string key)
        {
            if (key.Trim() == _settings.Key)
            {
                var tournament = new Tournament
                {
                    Date = date,
                    Description = description,
                    MinPlayers = minPlayers,
                    MaxPlayers = maxPlayers,
                    MaxRaiting = maxRaiting,
                    Name = name,
                    TournamentType = type
                };

                await _context.Tournaments.AddAsync(tournament);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // POST: TournamentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TournamentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TournamentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TournamentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TournamentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<int> GetRaiting(string name)
        {
            var httpClient = new HttpClient();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("search", name)
            });

            string[] s = new string []{ "<dfn>", "<\\/dfn>" };

            var response = await httpClient.PostAsync("https://rttf.ru/?ajax", formContent);

            var stream = await response.Content.ReadAsByteArrayAsync();

            var result = Encoding.UTF8.GetString(stream, 0, stream.Length);

            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(result);

            var raitings = htmlDoc.DocumentNode.InnerHtml.Split("<dfn>").ToList();

            var dfn = raitings[1].Split("<\\/dfn>").First();

            if (int.TryParse(dfn, out int raiting))
            {
                return raiting;
            }
            else
            {
                return 0;
            }
        }
    }
}
