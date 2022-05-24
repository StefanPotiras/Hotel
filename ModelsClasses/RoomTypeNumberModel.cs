using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClasses
{
     class RoomTypeNumberModel
    {
        public string RoomsType { get; set; }
        public ObservableColection<int> RoomsNumber { get; set; }
    }
}
