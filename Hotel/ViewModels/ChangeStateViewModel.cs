using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hotel.Helps;
using Hotel.Models;
using Hotel.Views;
using ModelsClasses;
using Server;

namespace Hotel.ViewModels
{
    class ChangeStateViewModel:NotifyViewModel
    {
        public ChangeStateViewModel() {
        
        }
        ReservationState reservationStateF;
        UserModel.UserType UserType;
        int id;
        public ChangeStateViewModel(int idRez,ReservationState reservationState,UserModel.UserType userType)
        {
            id = idRez;
            UserType = userType;
            reservationStateF = reservationState;
            if (reservationState == ReservationState.Active)
                active = true;
            else if (reservationState == ReservationState.Paid)
                paid = true;
            else if (reservationState == ReservationState.Canceled)
                canceled = true;

        }
        private ICommand ChangeCommand;
        public ICommand Change
        {
            get
            {
                if (ChangeCommand == null)
                {
                    ChangeCommand = new RelayCommands(ChangeFunction);
                }
                return ChangeCommand;
            }
        }
        public void ChangeFunction(object param)
        {
            ReservationState reservationStateTemp=new ReservationState();

            if (Paid == true)
                reservationStateTemp = ReservationState.Paid;
            else if (Active == true)
                reservationStateTemp = ReservationState.Active;
            else if (Canceled == true)
                reservationStateTemp = ReservationState.Canceled;

            if(reservationStateTemp==reservationStateF)
            {
                ReservationsView firstPage = new ReservationsView();
                ReservationsViewModel firstPageModel = new ReservationsViewModel(UserType);
                firstPage.DataContext = firstPageModel;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = firstPage;
                firstPage.Show();
            }
            else
            {
                using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
                register.UpdateReservationStatus(id, reservationStateTemp);
                ReservationsView firstPage = new ReservationsView();
                ReservationsViewModel firstPageModel = new ReservationsViewModel(UserType);
                firstPage.DataContext = firstPageModel;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = firstPage;
                firstPage.Show();
            }
           
        }
        public bool paid=false;
        public bool Paid
        {
            get
            {
                return paid;
            }
            set
            {
                paid = value;
                if (value == true)
                {
                    Active = false;
                    Canceled = false;
                }
                NotifyPropertyChanged("Paid");
            }
        }
        public bool canceled=false;
        public bool Canceled
        {
            get
            {
                return canceled;
            }
            set
            {
                canceled = value;
                if (value == true)
                {
                    Active = false;
                    Paid = false;
                }
                NotifyPropertyChanged("Canceled");
            }
        }
        public bool active=false;
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                if (value == true)
                {
                    Paid = false;
                    Canceled = false;
                }
                NotifyPropertyChanged("Active");
            }
        }
    }
}
