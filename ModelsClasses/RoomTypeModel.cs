using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClasses
{
    public class RoomTypeModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string RoomTitle { get; set; }
        public int NumberOfRooms { get; set; }
        public ObservableCollection<FeatureModel> Features { get; set; }
        public ObservableCollection<ImageModel> Images { get; set; }
    }
}
