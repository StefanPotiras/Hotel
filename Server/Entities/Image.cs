using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public bool Deleted { get; set; }

        public RoomType RoomType { get; set; }
    }
}
