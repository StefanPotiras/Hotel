using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class Customer : User
    {
        public bool Deleted { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
