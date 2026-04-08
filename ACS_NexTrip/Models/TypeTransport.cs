using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_NexTrip.Models
{
    public class TypeTransport
    {
        public int TYP_ID { get; set; }
        public string TYP_LIBELLE { get; set; }

        public override string ToString() => TYP_LIBELLE;
    }
}
