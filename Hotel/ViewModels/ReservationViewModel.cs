using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Hotel.Views;
using Hotel.Helps;
using Server;
using ModelsClasses;
using System.Windows.Input;

namespace Hotel.ViewModels
{
    public class ReservationsViewModel : NotifyViewModel
    {
        ObservableCollection<ReservationsViewModelBind> reservationsCurrent = new ObservableCollection<ReservationsViewModelBind>();
       public  ReservationsViewModel() { }
        UserModel.UserType userType;
        public ReservationsViewModel(UserModel.UserType userType)
        {
            this.userType = userType;
            ObservableCollection<ReservationModel> reservationModels = new ObservableCollection<ReservationModel>();
            using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
            reservationsCurrent = Helps.Convert.returnVectorReserv(register.GetAllReservations());
            foreach(var index in reservationsCurrent)
            {
                index.userType = userType;
            }

        }

        private ICommand BackCommand;
        public ICommand Back
        {
            get
            {
                if (BackCommand == null)
                {
                    BackCommand = new RelayCommands(BackFunction);
                }
                return BackCommand;
            }
        }
        public void BackFunction(object param)
        {
            UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
            UnauthorizedClient firstPageModel = new UnauthorizedClient(userType);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
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
