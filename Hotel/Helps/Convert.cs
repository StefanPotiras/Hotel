using ModelsClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Helps
{
    public static class Convert
    {
        public static ObservableCollection<TypeRoomsModelBinding>returnVector(ObservableCollection<RoomTypeModel> temp)
        {
            ObservableCollection<TypeRoomsModelBinding> tempArray = new ObservableCollection<TypeRoomsModelBinding>();
            foreach(var index in temp)
            {
                TypeRoomsModelBinding variab = new TypeRoomsModelBinding();
                variab.Capacity = index.Capacity;
                variab.Price = index.Price;
                variab.RoomTitle = index.RoomTitle;
                variab.Description = index.Description;
                variab.Images = index.Images;
                variab.NumberOfRooms = index.NumberOfRooms;
                variab.Id = index.Id;
                variab.Features = index.Features;
                variab.convertImages();
                tempArray.Add(variab);
            }
            return tempArray;
        }

        public static ObservableCollection<ReservationsViewModelBind> returnVectorReserv(ObservableCollection<ReservationModel> temp)
        {
            ObservableCollection<ReservationsViewModelBind> tempArray = new ObservableCollection<ReservationsViewModelBind>();
            foreach (var index in temp)
            {
                ReservationsViewModelBind variab = new ReservationsViewModelBind();
                variab.Username = index.Username;
                variab.UserId = index.UserId;
                variab.Price = index.Price;
                variab.StartDate = index.StartDate;
                variab.EndDate = index.EndDate;
                variab.State = index.State;
                variab.NumberOfRooms = index.NumberOfRooms;
               
                tempArray.Add(variab);
            }
            return tempArray;
        }
    }
}
