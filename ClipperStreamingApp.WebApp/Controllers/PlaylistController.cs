using ClipperStreamingApp.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
// Adicione estes usings para o logout
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ClipperStreamingApp.WebApp.Controllers
{
    [Authorize]
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }
        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue("UserId");
            
            if (!int.TryParse(userIdString, out var userId))
            {
                TempData["ErrorMessage"] = "Sua sessão está desatualizada. Por favor, faça o login novamente.";

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return RedirectToAction("Login", "Auth");
            }

            var playlists = await _playlistService.GetUserPlaylistsAsync(userId);
            return View(playlists);
        }

        public async Task<IActionResult> Details(int id)
        {
            var playlistDetails = await _playlistService.GetPlaylistDetailsAsync(id);
            if (playlistDetails == null)
            {
                return NotFound();
            }
            return View(playlistDetails);
        }
    }
}


