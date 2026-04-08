using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_NexTrip.Models
{
    public class Trajet
    {
        public int TRA_ID { get; set; }
        public DateTime TRA_DATEDEPART { get; set; }
        public DateTime TRA_DATEARRIVEE { get; set; } // Obligatoire pour ton code
        public TimeSpan TRA_HEUREDEPART { get; set; } // Obligatoire
        public TimeSpan TRA_HEUREARRIVEE { get; set; } // Obligatoire
        public int TRA_LIEU_DEPART_ID { get; set; }  // L'ID pour la base
        public int TRA_LIEU_ARRIVEE_ID { get; set; } // L'ID pour la base
        public int TYP_ID { get; set; }
        public decimal TRA_PRIX { get; set; }

        // Libellés pour l'affichage (JOIN)
        public string TRA_LIEU_DEPART { get; set; }
        public string TRA_LIEU_ARRIVEE { get; set; }
        public string TYP_LIBELLE { get; set; }

        public string AffichageDepart => $"{TRA_DATEDEPART:dd/MM/yyyy} {TRA_HEUREDEPART:hh\\:mm}";
    }
}
