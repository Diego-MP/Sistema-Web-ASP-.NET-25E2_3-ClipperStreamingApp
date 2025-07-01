using System.Security.Claims;
using ClipperStreamingApp.WebApp.Models;
using ClipperStreamingApp.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClipperStreamingApp.WebApp.Controllers;

[Authorize] 
    public class AssinaturaController : Controller
    {
        private readonly IAssinaturaService _assinaturaService;

        public AssinaturaController(IAssinaturaService assinaturaService)
        {
            _assinaturaService = assinaturaService;
        }

        public async Task<IActionResult> Index()
        {
            var planos = await _assinaturaService.GetPlanosAsync();
            var viewModel = new AssinaturaViewModel
            {
                PlanosDisponiveis = planos
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assinar(AssinaturaViewModel model)
        {
            if (!model.SelectedPlanoId.HasValue)
            {
                ModelState.AddModelError("", "Por favor, selecione um plano para continuar.");
                model.PlanosDisponiveis = await _assinaturaService.GetPlanosAsync();
                return View("Index", model);
            }

            var userIdString = User.FindFirstValue("UserId");

            if (!int.TryParse(userIdString, out var userId))
            {
                TempData["ErrorMessage"] = "Sua sessão expirou. Por favor, faça o login novamente.";
                return RedirectToAction("Login", "Auth");
            }

            var (isSuccess, message) = await _assinaturaService.AssinarPlanoAsync(userId, model.SelectedPlanoId.Value);

            if (isSuccess)
            {
                TempData["SuccessMessage"] = message ?? "Plano assinado com sucesso!";
            }
            else
            {
                TempData["ErrorMessage"] = message ?? "Ocorreu um erro ao assinar o plano.";
            }

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public async Task<IActionResult> Historico()
        {
            var contaIdString = User.FindFirstValue("UserId"); 
            if (!int.TryParse(contaIdString, out var contaId) || contaId <= 0)
            {
                TempData["ErrorMessage"] = "Não foi possível obter os dados da sua conta. Por favor, faça o login novamente.";
                return RedirectToAction("Login", "Auth");
            }

            var historico = await _assinaturaService.GetHistoricoAssinaturasAsync(contaId);
            return View(historico);
        }
    }
