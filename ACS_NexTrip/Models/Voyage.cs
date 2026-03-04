namespace ACS_NexTrip.Models;

public class Voyage
{
    public int Id { get; set; }
    public TransportType Type { get; set; }
    public string Departure { get; set; } = string.Empty;
    public string Arrival { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string Traveler { get; set; } = string.Empty;
    public TripStatus Status { get; set; }
    public string? Remarks { get; set; }

    public string RouteDisplay => $"{Departure} → {Arrival}";
    public string DateTimeDisplay => $"{Date:dd/MM/yyyy} {Time:hh\\:mm}";
}

public enum TransportType
{
    Avion,
    Train,
    Bus,
    Métro
}

public enum TripStatus
{
    Prévu,
    EnCours,
    Terminé,
    Annulé
}