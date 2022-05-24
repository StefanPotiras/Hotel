using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Hotel.Views;
using Hotel.Helps;
using Server;
using ModelsClasses;

namespace Hotel.ViewModels
{
    public class ReservationsViewModel : NotifyViewModel
    {
        ObservableCollection<ReservationsViewModelBind> reservationsCurrent = new ObservableCollection<ReservationsViewModelBind>();
        public ReservationsViewModel()
        {
            ObservableCollection<ReservationModel> reservationModels = new ObservableCollection<ReservationModel>();
            using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
            reservationsCurrent = Helps.Convert.returnVectorReserv(register.GetAllReservations());

        }
      


        public ObservableCollection<ReservationsViewModelBind> ReservationsCurrent
        {
            get
            {
                return reservationsCurrent;
            }
            set
            {
                reservationsCurrent = value;
            }
        }
    }
}
