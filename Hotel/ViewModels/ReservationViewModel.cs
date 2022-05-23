using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Hotel.Views;
namespace Hotel.ViewModels
{
    public class ReservationsViewModel : NotifyViewModel
    {
        public ReservationsViewModel()
        {

            Reservations reservations = new Reservations();
            reservations.cameraType = "mare";
            reservations.dateStart = "12.10.21";
            reservations.name = "Stefan";
            reservations.roomNumber = 102;
            reservations.reservationStatus = EnumClass.neplatit;
            reservationsCurrent.Add(reservations);
        }
        ObservableCollection<Reservations> reservationsCurrent = new ObservableCollection<Reservations>();


        public ObservableCollection<Reservations> ReservationsCurrent
        {
            get
            {
                return reservationsCurrent;
            }
        }
    }
}
