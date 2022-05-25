using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClasses
{
    public class RoomTypeNumberModel
    {
        public int RoomTypeId { get; set; }
        public ObservableCollection<int> RoomNumbers { get; set; }
        public int NumberOfTypeRooms { get; set; }
    }
}
