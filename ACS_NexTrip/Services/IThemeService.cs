namespace ACS_NexTrip.Services;

public interface IThemeService
{
    bool IsDarkMode { get; }
    event EventHandler<bool>? ThemeChanged;

    void ToggleTheme();
    void SetTheme(bool isDarkMode);
}