using ClipperStreamingApp.WebApp.Models;
using ClipperStreamingApp.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClipperStreamingApp.WebApp.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IPlaylistService _playlistService;

        public SearchController(ISearchService searchService, IPlaylistService playlistService)
        {
            _searchService = searchService;
            _playlistService = playlistService;
        }

        public IActionResult Index()
        {
            return View(new SearchViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Query))
            {
                return View(new SearchViewModel());
            }

            var bandasTask = _searchService.SearchBandasAsync(model.Query);
            var musicasTask = _searchService.SearchMusicasAsync(model.Query);

            await Task.WhenAll(bandasTask, musicasTask);

            var searchResultModel = new SearchViewModel
            {
                Query = model.Query,
                BandasResult = await bandasTask,
                MusicasResult = await musicasTask
            };

            return View(searchResultModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToPlaylist(SearchViewModel model)
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));

            bool success = false;
            if (model.SelectedItemType == "Musica")
            {
                success = await _playlistService.AddMusicaToPlaylistAsync(model.SelectedPlaylistId, model.SelectedItemId);
            }
            else if (model.SelectedItemType == "Banda")
            {
                success = await _playlistService.AddBandaToPlaylistAsync(model.SelectedPlaylistId, model.SelectedItemId);
            }

            if (success)
            {
                TempData["SuccessMessage"] = "Item adicionado à playlist com sucesso!";
            }
            else
            {
                TempData["ErrorMessage"] = "Não foi possível adicionar o item à playlist.";
            }

            return RedirectToAction("Index", new { query = model.Query });
        }
    }
}
