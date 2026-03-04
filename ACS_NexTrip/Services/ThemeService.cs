namespace ACS_NexTrip.Services;

public class ThemeService : IThemeService
{
    private bool _isDarkMode;

    public bool IsDarkMode
    {
        get => _isDarkMode;
        private set
        {
            if (_isDarkMode != value)
            {
                _isDarkMode = value;
                ApplyTheme();
                ThemeChanged?.Invoke(this, _isDarkMode);
            }
        }
    }

    public event EventHandler<bool>? ThemeChanged;

    public ThemeService()
    {
        // Charger le thème depuis les préférences
        _isDarkMode = Preferences.Default.Get("DarkMode", false);
        ApplyTheme();
    }

    public void ToggleTheme()
    {
        IsDarkMode = !IsDarkMode;
    }

    public void SetTheme(bool isDarkMode)
    {
        IsDarkMode = isDarkMode;
    }

    private void ApplyTheme()
    {
        // Sauvegarder la préférence
        Preferences.Default.Set("DarkMode", _isDarkMode);

        // Appliquer le thème à l'application
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = _isDarkMode ? AppTheme.Dark : AppTheme.Light;
        }
    }
}