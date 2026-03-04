namespace ACS_NexTrip.Models;

public class Utilisateur
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public DateTime? LastConnection { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}

public enum UserRole
{
    Admin,
    Gestionnaire,
    Voyageur
}

public enum UserStatus
{
    Actif,
    Inactif
}