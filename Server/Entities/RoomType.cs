using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Server.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public string RoomTitle { get; set; }
        public bool Deleted { get; set; }
        public int Capacity { get; set; }

        public ICollection<RoomPrice> Prices { get; set; }
        public ICollection <Room> Rooms { get; set; }
        public ICollection <Feature> Features { get; set; }
        public ICollection<Image> Images { get; set; }
    }

}
