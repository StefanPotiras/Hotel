using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsClasses;
namespace Server.Entities
{
    public class Reservation
    {
       

        public bool Deleted { get; set; }
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BeginDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        public ReservationState State { get; set; }

        public Customer Customer { get; set; }
        public ICollection<ExtraService> ExtraServices { get; set; }
        public ICollection<Room> Rooms { get; set; }
        //price?
    }
}
