using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class Room
    {
        [Key]
        public int RoomNo { get; set; }
        public bool Deleted { get; set; }

        public RoomType RoomType { get; set; }
        //public ICollection<Feature> ExtraServices { get;set;}
        public ICollection<Reservation> Reservations { get; set; }
    }
}
