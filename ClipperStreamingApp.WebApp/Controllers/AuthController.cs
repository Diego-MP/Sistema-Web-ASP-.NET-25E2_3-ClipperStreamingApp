using System.Security.Claims;
using ClipperStreamingApp.WebApp.Models;
using ClipperStreamingApp.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ClipperStreamingApp.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var (isSuccess, message, userId) = await _authService.LoginAsync(model.Username, model.Password);
            
            if (isSuccess && userId.HasValue && userId.Value > 0)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim("UserId", userId.Value.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60) };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var errorMessage = (isSuccess && !(userId.HasValue && userId.Value > 0))
                    ? "Ocorreu um erro ao obter os dados do utilizador. Tente novamente."
                    : message;

                ModelState.AddModelError(string.Empty, errorMessage ?? "Utilizador ou senha inválidos.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (isSuccess, message) = await _authService.RegisterAsync(model);

            if (isSuccess)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, message);
                return View(model);
            }
        }
    }
}
