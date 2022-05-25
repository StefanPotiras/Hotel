using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClasses
{
    public class RoomTypeNumberModel
    {   public RoomTypeNumberModel() { }
        public RoomTypeNumberModel(int index,int nr) {
            RoomTypeId = index;
            NumberOfTypeRooms = nr;
        }
        public int RoomTypeId { get; set; }
        public ObservableCollection<int> RoomNumbers { get; set; }
        public int NumberOfTypeRooms { get; set; }
    }
}
