using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using ACS_NexTrip.Models;
using Microsoft.Data.SqlClient; // Pilote pour SQL Server

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
            Connection.Open();
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
        }





        public async Task<bool> AddTrajetAsync(Trajet t)
        {
            try
            {

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
        }




        public async Task<bool> DeleteTrajetAsync(int id)
        {
            // Affiche l'ID dans la console "Sortie" (Output) de Visual Studio
            System.Diagnostics.Debug.WriteLine($"---> Tentative de suppression de l'ID : {id}");

            try
            {
                using (SqlCommand cmd = new SqlCommand("ps_DeleteTrajet", this.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // On utilise bien TON paramètre SQL : @IdTrajet
                    cmd.Parameters.AddWithValue("@IdTrajet", id);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                // Si SQL refuse (clé étrangère), l'erreur s'affichera ici
                System.Diagnostics.Debug.WriteLine("---> ERREUR SQL : " + ex.Message);
                return false;
            }
        }





        public async Task<List<Lieu>> GetLieuxAsync()
        {
            List<Lieu> liste = new List<Lieu>();
            try
            {
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

            return liste;
        }





        public async Task<bool> InscrireUtilisateurAsync(Utilisateur u)
        {
            try
            {
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
        }






        // Afficher les trajets 
        public async Task<List<Trajet>> GetTrajetsAsync()
        {
            List<Trajet> liste = new List<Trajet>();

            using (SqlCommand command = new SqlCommand("ps_GetTrajets", this.Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        liste.Add(new Trajet
                        {
                            // C'EST CETTE LIGNE QUI FAIT LE LIEN :
                            // On récupère l'ID que SQL a généré automatiquement
                            TRA_ID = Convert.ToInt32(reader["TRA_ID"]),

                            TRA_DATEDEPART = (DateTime)reader["TRA_DATEDEPART"],
                            TRA_HEUREDEPART = (TimeSpan)reader["TRA_HEUREDEPART"],
                            TRA_LIEU_DEPART = reader["TRA_LIEU_DEPART"].ToString(),
                            TRA_LIEU_ARRIVEE = reader["TRA_LIEU_ARRIVEE"].ToString(),
                            TYP_LIBELLE = reader["TYP_LIBELLE"].ToString(),
                            TRA_PRIX = Convert.ToDecimal(reader["TRA_PRIX"])
                        });
                    }
                }
            }
            return liste;
        }

        // Afficher les 3 prochains trajets 
        public async Task<List<Trajet>> GetNextTrajetsAsync()
        {
            List<Trajet> liste = new List<Trajet>();
            try
            {
                SqlCommand command = new SqlCommand("ps_GetNextTrajet", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    liste.Add(new Trajet
                    {
                        TRA_DATEDEPART = (DateTime)reader["TRA_DATEDEPART"],
                        TRA_DATEARRIVEE = (DateTime)reader["TRA_DATEARRIVEE"],
                        TRA_HEUREDEPART = (TimeSpan)reader["TRA_HEUREDEPART"],
                        TRA_HEUREARRIVEE = (TimeSpan)reader["TRA_HEUREARRIVEE"],
                        TRA_LIEU_DEPART = reader["TRA_LIEU_DEPART"].ToString(),
                        TRA_LIEU_ARRIVEE = reader["TRA_LIEU_ARRIVEE"].ToString(),
                        TYP_LIBELLE = reader["TYP_LIBELLE"].ToString(),
                        TRA_PRIX = Convert.ToDecimal(reader["TRA_PRIX"])
                    });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(">>> CRASH GetNextTrajetsAsync : " + ex.Message);
                System.Diagnostics.Debug.WriteLine(">>> DETAIL : " + ex.ToString());
            }
            return liste;
        }




        public async Task<List<Utilisateur>> GetUtilisateurAsync()
        {
            List<Utilisateur> liste = new List<Utilisateur>();
            using (SqlCommand command = new SqlCommand("ps_GetUtilisateur", this.Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdUtilisateur", 4);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        liste.Add(new Utilisateur
                        {
                            UTI_ID = (int)reader["UTI_ID"],
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
                            ROL_LIBELLE = (string?)reader["ROL_LIBELLE"],
                            LIE_ID = (int)reader["LIE_ID"], // Récupéré via la jointure
                            ROL_ID = (int)reader["ROL_ID"],
                        });
                    }
                }
            }
            return liste;
        }









        public async Task<List<TypeTransport>> GetTypesAsync()
        {
            List<TypeTransport> liste = new List<TypeTransport>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ps_GetTypes", this.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            liste.Add(new TypeTransport
                            {
                                TYP_ID = Convert.ToInt32(dr["TYP_ID"]),
                                TYP_LIBELLE = dr["TYP_LIBELLE"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERREUR GetTypesAsync : " + ex.Message);
                System.Diagnostics.Debug.WriteLine("DETAIL : " + ex.ToString());
            }
            return liste; // ✅ toujours retourner la liste, même vide en cas d'erreur
        }

        public async Task<bool> UpdateUtilisateurAsync(Utilisateur u)
        {
            try
            {
                // On s'assure que la connexion est ouverte
                this.Ouvrir();

                using (SqlCommand cmd = new SqlCommand("ps_UpdateUtilisateur", this.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Mapping des paramètres selon votre procédure stockée
                    // Attention : Assurez-vous que votre classe "Utilisateur" possède bien ces propriétés (UTI_ID, LIE_ID, ROL_ID)
                    cmd.Parameters.AddWithValue("@IdUtilisateur", u.UTI_ID);
                    cmd.Parameters.AddWithValue("@UTI_LOGIN", u.UTI_LOGIN ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UTI_PASSWORD", u.UTI_PASSWORD ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UTI_NOM", u.UTI_NOM ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UTI_PRENOM", u.UTI_PRENOM ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UTI_DATENAISSANCE", u.UTI_DATENAISSANCE);
                    cmd.Parameters.AddWithValue("@UTI_ADRESSE", u.UTI_ADRESSE ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UTI_CP", u.UTI_CP ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UTI_TEL", u.UTI_TEL ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UTI_EMAIL", u.UTI_EMAIL ?? (object)DBNull.Value);

                    // Si vous n'avez pas encore LIE_ID et ROL_ID dans votre modèle Utilisateur, 
                    // il faudra les ajouter pour que l'Update fonctionne.
                    cmd.Parameters.AddWithValue("@LIE_ID", u.LIE_ID);
                    cmd.Parameters.AddWithValue("@ROL_ID", u.ROL_ID);

                    // Exécution de la requête
                    int rows = await cmd.ExecuteNonQueryAsync();

                    // Retourne true si au moins une ligne a été modifiée
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erreur UpdateUtilisateur : " + ex.Message);
                return false;
            }
        }
    }
}