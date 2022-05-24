using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server.Entities
{
    public class RoomPrice
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }

        public RoomType RoomType { get; set; }
    }
}
