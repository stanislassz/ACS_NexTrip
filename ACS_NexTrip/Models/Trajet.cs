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
        public string TRA_LIEU_DEPART { get; set; }
        public string TRA_LIEU_ARRIVEE { get; set; }
        public string TYP_LIBELLE { get; set; }
        public decimal TRA_PRIX { get; set; }

        public string RecapTrajet => $"{TRA_LIEU_DEPART} ➔ {TRA_LIEU_ARRIVEE}";
    }
}
