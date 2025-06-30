using ClipperStreamingApp.WebApp.Models;

namespace ClipperStreamingApp.WebApp.Services;

public interface IAuthService
{
    Task<(bool IsSuccess, string Message, int? UserId)> LoginAsync(string username, string password);
    Task<(bool IsSuccess, string Message)> RegisterAsync(RegisterViewModel model);
}