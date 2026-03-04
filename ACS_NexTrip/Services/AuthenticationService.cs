using ACS_NexTrip.Models;

namespace TravelManager.Services;

public class AuthenticationService : IAuthenticationService
{
    private User? _currentUser;

    public User? CurrentUser
    {
        get => _currentUser;
        private set
        {
            _currentUser = value;
            AuthenticationChanged?.Invoke(this, _currentUser);
        }
    }

    public bool IsAuthenticated => CurrentUser != null;

    public event EventHandler<User?>? AuthenticationChanged;

    public Task<bool> LoginAsync(string email, string password)
    {
        // Simulation de connexion
        if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
        {
            CurrentUser = new User
            {
                Id = 1,
                Email = email,
                FirstName = "Jean",
                LastName = "Dupont",
                Role = UserRole.Admin,
                Status = UserStatus.Actif,
                LastConnection = DateTime.Now
            };
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public Task LogoutAsync()
    {
        CurrentUser = null;
        return Task.CompletedTask;
    }
}