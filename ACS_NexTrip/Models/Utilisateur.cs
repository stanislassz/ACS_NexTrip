using System;
using System.Collections.Generic;
using System.Text;

namespace ACS_NexTrip.Models
{
    public class Utilisateur
    {
        // Ces noms correspondent aux colonnes de ton MCD
        public int UTI_ID { get; set; }
        public string UTI_Login { get; set; }
        public string UTI_Password { get; set; }
        public string UTI_Nom { get; set; }
        public string UTI_Prenom { get; set; }
        public string UTI_Email { get; set; }

        // Pour savoir s'il est Voyageur ou Gestionnaire (La relation "Faire parti")
        public int ROL_Id { get; set; }
    }
}