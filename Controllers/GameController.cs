using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stream.Models;
using Stream.ViewModels.Dto;
using Stream.ViewModels.ViewModels;
using Stream.Services.Interfaces;

namespace Stream.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var games = await _gameService.SearchGamesAsync(searchQuery, pageNumber, pageSize);
            var totalGames = await _gameService.GetTotalCountAsync(searchQuery);

            var viewModel = new GameViewModel
            {
                Games = _mapper.Map<IEnumerable<GameDto>>(games),
                SearchQuery = searchQuery,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalGames = totalGames ?? 0
            };

            if (IsAjaxRequest())
            {
                return PartialView("_GameTablePartial", viewModel.Games);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new GameViewModel
            {
                Platforms = GetPlatforms(),
                Genres = GetGenres()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Platforms = GetPlatforms();
                viewModel.Genres = GetGenres();
                return View(viewModel);
            }

            var game = _mapper.Map<Stream.Models.Game>(viewModel.Game);
            await _gameService.AddAsync(game);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            var viewModel = new GameViewModel
            {
                Game = _mapper.Map<GameDto>(game),
                Platforms = GetPlatforms(),
                Genres = GetGenres()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GameViewModel viewModel)
        {
            if (id != viewModel.Game.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                viewModel.Platforms = GetPlatforms();
                viewModel.Genres = GetGenres();
                return View(viewModel);
            }

            var game = _mapper.Map<Stream.Models.Game>(viewModel.Game);
            await _gameService.UpdateAsync(game);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            var gameDto = _mapper.Map<GameDto>(game);
            return View(gameDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _gameService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> GetPlatforms()
        {
            return Enum.GetValues(typeof(Platform))
                .Cast<Platform>()
                .Select(p => new SelectListItem { Value = p.ToString(), Text = p.ToString() });
        }

        private IEnumerable<SelectListItem> GetGenres()
        {
            return Enum.GetValues(typeof(Genre))
                .Cast<Genre>()
                .Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() });
        }

        public bool IsAjaxRequest()
        {
            return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}