using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class Reservation
    {
        public enum State
        {
            None, Active, Canceled, Paid
        }

        public bool Deleted { get; set; }
        public int Id { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public State ReservationState { get; set; }

        public Customer Customer { get; set; }
        public ICollection<ExtraService> ExtraServices { get; set; }
        public ICollection<Room> Rooms { get; set; }
        //price?
    }
}
