using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_NexTrip.Models
{
    public class Lieu
    {
        public int LIE_ID { get; set; }
        public string LIE_LIBELLE { get; set; }

        // Optionnel : ce qui s'affichera par défaut si on ne précise pas
        public override string ToString() => LIE_LIBELLE;
    }
}
