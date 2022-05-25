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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append('\n' + "Reservations:");

            foreach (Reservation reservation in Reservations)
            {
                sb.Append(reservation.ToString() + '\n');
            }

            return sb.ToString();
        }
    }
}
