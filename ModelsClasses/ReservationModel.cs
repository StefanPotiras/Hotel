using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Reserv
//{ Username,Price,StartDate,EndDate,Status,RoomsNumber,RoomsType,Extra}
namespace ModelsClasses
{
    public class ReservationModel
    {
        public int IdReserv { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationState State { get; set; }
        public int NumberOfRooms { get; set; }
        public ObservableCollection<ServicesModel> Services { get; set; }
        public ObservableCollection<RoomTypeNumberModel> AllRoomsWithType { get; set; }




    }
}
