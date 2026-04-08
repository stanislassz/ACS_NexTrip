using System;
using System.Collections.Generic;
using System.Text;

namespace ACS_NexTrip.Models
{
    public class Utilisateur
    {
        public int UTI_ID { get; set; }
        public string UTI_LOGIN { get; set; }
        public string UTI_PASSWORD { get; set; }
        public string UTI_NOM { get; set; }
        public string UTI_PRENOM { get; set; }
        public DateTime UTI_DATENAISSANCE { get; set; }
        public string UTI_ADRESSE { get; set; }
        public string UTI_CP { get; set; }
        public string UTI_TEL { get; set; }
        public string UTI_EMAIL { get; set; }
        public int LIE_ID { get; set; }    // L'ID pour enregistrer
        public string? LIE_LIBELLE { get; set; } // Le nom pour afficher
        public int ROL_ID { get; set; }    // L'ID pour enregistrer
        public string? ROL_LIBELLE { get; set; } // Le nom pour afficher
    }
}