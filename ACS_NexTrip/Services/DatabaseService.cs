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
                DataSource = @"2SIO-MAL\MSSQLSERVER01",
                InitialCatalog = "ACS_VOYAGE",
                IntegratedSecurity = false,
                UserID = "sa",
                Password = "SLAMbest@2024",
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




        public async Task<bool> VerifierConnexionAsync(string login, string password)
        {
            try
            {
                this.Ouvrir();
                using (SqlCommand cmd = new SqlCommand("ps_Connexion", this.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        // HasRows renvoie true si la PS a trouvé un utilisateur correspondant
                        return reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erreur Login SQL : " + ex.Message);
                return false;
            }
            finally
            {
                this.Fermer();
            }
        }





        public async Task<bool> AddTrajetAsync(Trajet t)
        {
            try
            {
                this.Ouvrir(); // Utilise ta méthode habituelle pour ouvrir la connexion

                using (SqlCommand cmd = new SqlCommand("ps_AddTrajet", this.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // On lie les paramètres de ta PS avec les propriétés de l'objet Trajet
                    cmd.Parameters.AddWithValue("@TRA_DATEDEPART", t.TRA_DATEDEPART);
                    cmd.Parameters.AddWithValue("@TRA_DATEARRIVEE", t.TRA_DATEARRIVEE);
                    cmd.Parameters.AddWithValue("@TRA_HEUREDEPART", t.TRA_HEUREDEPART);
                    cmd.Parameters.AddWithValue("@TRA_HEUREARRIVEE", t.TRA_HEUREARRIVEE);
                    cmd.Parameters.AddWithValue("@TRA_LIEU_DEPART", t.TRA_LIEU_DEPART_ID);
                    cmd.Parameters.AddWithValue("@TRA_LIEU_ARRIVEE", t.TRA_LIEU_ARRIVEE_ID);
                    cmd.Parameters.AddWithValue("@TYP_ID", t.TYP_ID);
                    cmd.Parameters.AddWithValue("@TRA_PRIX", t.TRA_PRIX);

                    // ExecuteNonQuery retourne le nombre de lignes affectées
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                // Debugger ici en cas d'erreur SQL
                Console.WriteLine("Erreur SQL : " + ex.Message);
                return false;
            }
            finally
            {
                this.Fermer(); // Toujours refermer la connexion
            }
        }






        public async Task<List<Lieu>> GetLieuxAsync()
        {
            List<Lieu> liste = new List<Lieu>();
            try
            {
                this.Ouvrir();
                using (SqlCommand cmd = new SqlCommand("ps_GetLieux", this.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            liste.Add(new Lieu
                            {
                                LIE_ID = Convert.ToInt32(dr["LIE_ID"]),
                                LIE_LIBELLE = dr["LIE_LIBELLE"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { this.Fermer(); }

            return liste;
        }





        public async Task<bool> InscrireUtilisateurAsync(Utilisateur u)
        {
            try
            {
                this.Ouvrir();
                using (SqlCommand cmd = new SqlCommand("ps_Inscription", this.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Dans InscrireUtilisateurAsync
                    cmd.Parameters.AddWithValue("@Login", u.UTI_LOGIN);   
                    cmd.Parameters.AddWithValue("@Password", u.UTI_PASSWORD);
                    cmd.Parameters.AddWithValue("@Nom", u.UTI_NOM);
                    cmd.Parameters.AddWithValue("@Prenom", u.UTI_PRENOM);
                    cmd.Parameters.AddWithValue("@Email", u.UTI_EMAIL);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erreur SQL : " + ex.Message);
                return false;
            }
            finally { this.Fermer(); }
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