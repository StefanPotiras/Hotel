using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


Reserv
{ Username,Price,StartDate,EndDate,Status,RoomsNumber,RoomsType,Extra}
namespace ModelsClasses
{
    class ReservationModel
    {
        

        public string Username { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationState State { get; set; }
        public int NumberOfRooms { get; set; }


    }
}
