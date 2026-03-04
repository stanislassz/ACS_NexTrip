using ACS_NexTrip.Models;

namespace TravelManager.Services;

public interface IAuthenticationService
{
    User? CurrentUser { get; }
    bool IsAuthenticated { get; }
    event EventHandler<User?>? AuthenticationChanged;

    Task<bool> LoginAsync(string email, string password);
    Task LogoutAsync();
}