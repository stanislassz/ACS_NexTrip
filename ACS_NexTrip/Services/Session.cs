using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS_NexTrip.Models;

namespace ACS_NexTrip.Services
{
    public static class Session
    {
        public static Utilisateur? CurrentUser { get; set; }
    }
}
