using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient; // Pilote pour SQL Server
using ACS_NexTrip.Models;

namespace ACS_NexTrip.Services
{
    public class ConnexionBD
    {
        // La propriété qui contient l'objet de connexion SQL
        public SqlConnection Connection { get; set; }

        public ConnexionBD()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = @"SIO-TCA",
                InitialCatalog = "ACS_VOYAGE",
                IntegratedSecurity = false,
                UserID = "sa",
                Password = "Info76240#", 
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


        // Afficher les trajets 
        public async Task<List<Trajet>> GetTrajetsAsync()
        {
            List<Trajet> liste = new List<Trajet>();

            this.Ouvrir();

            string queryString = "ps_GetTrajets";
            SqlCommand command = new SqlCommand(queryString, this.Connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                liste.Add(new Trajet
                {
                    TRA_DATEDEPART = (DateTime)reader["TRA_DATEDEPART"],
                    TRA_LIEU_DEPART = reader["TRA_LIEU_DEPART"].ToString(),
                    TRA_LIEU_ARRIVEE = reader["TRA_LIEU_ARRIVEE"].ToString(),
                    TYP_LIBELLE = reader["TYP_LIBELLE"].ToString(),
                    TRA_PRIX = Convert.ToDecimal(reader["TRA_PRIX"])
                });
            }

            reader.Close();
            this.Fermer();

            return liste;
        }

        public async Task<List<Utilisateur>> GetUtilisateurAsync()
        {
            List<Utilisateur> liste = new List<Utilisateur>();

            this.Ouvrir();

            string queryString = "ps_GetUtilisateur";
            SqlCommand command = new SqlCommand(queryString, this.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdUtilisateur", 4);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            reader.Read();
            
            liste.Add(new Utilisateur
            {
                UTI_LOGIN = (string?)reader["UTI_LOGIN"],
                UTI_PASSWORD = (string?)reader["UTI_PASSWORD"],
                UTI_NOM = (string?)reader["UTI_NOM"],
                UTI_PRENOM = (string?)reader["UTI_PRENOM"],
                UTI_DATENAISSANCE = (DateTime)reader["UTI_DATENAISSANCE"],
                UTI_ADRESSE = (string?)reader["UTI_ADRESSE"],
                UTI_CP = (string?)reader["UTI_CP"],
                UTI_TEL = (string?)reader["UTI_TEL"],
                UTI_EMAIL = (string?)reader["UTI_EMAIL"],
                LIE_LIBELLE = (string?)reader["LIE_LIBELLE"],
                ROL_LIBELLE = (string?)reader["ROL_LIBELLE"]
            });
            

            reader.Close();
            this.Fermer();

            return liste;
        }
    }
}