using Microsoft.Data.SqlClient; // Pilote pour SQL Server

namespace ACS_NexTrip.Services
{
    public class ConnexionBD
    {
        // La propriété qui contient l'objet de connexion SQL
        public SqlConnection Connection { get; set; }

        public ConnexionBD()
        {
            // Configuration basée sur ta capture d'écran SSMS
            var builder = new SqlConnectionStringBuilder
            {
                // Nom du serveur copié de ton image
                DataSource = @"STAN",
                InitialCatalog = "ACS_VOYAGE",

                // On passe en Authentification SQL Server
                IntegratedSecurity = false,
                UserID = "sa",
                Password = "sa", // Remplace par ton vrai mot de passe

                TrustServerCertificate = true
            };

            // Initialisation de la connexion avec la chaîne générée
            Connection = new SqlConnection(builder.ConnectionString);
        }

        // Méthode pour ouvrir la connexion avant une requête
        public void Ouvrir()
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
                Connection.Open();
        }

        // Méthode pour fermer la connexion après usage
        public void Fermer()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
                Connection.Close();
        }
    }
}