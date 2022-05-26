using Hotel.Helps;
using Hotel.Models;
using Hotel.Views;
using ModelsClasses;
using Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModels
{
    public class MakeReservationViewModel:NotifyViewModel
    {
        public ObservableCollection<ServicesDataBinding> servicesDataBindings = new ObservableCollection<ServicesDataBinding>();
        ReservationModel reservationModel = new ReservationModel();
        string username;
        public MakeReservationViewModel() { }
        public MakeReservationViewModel(ReservationModel reservationModel2,string username)
        {
            this.username = username;
            using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
            servicesDataBindings = Helps.Convert.returnVector3(register.GetServices());
            priceT = reservationModel2.Price;
            reservationModel = reservationModel2;
            totalRooms = reservationModel.AllRoomsWithType.Count;
            foreach (var index in reservationModel.AllRoomsWithType)
            {
                if (index.NumberOfTypeRooms > 1)
                    totalRooms = totalRooms + index.NumberOfTypeRooms - 1;
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
            UnauthorizedClient firstPageModel = new UnauthorizedClient();
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }

        public ICommand SendCommand;
        public ICommand Send
        {
            get
            {
                if (SendCommand == null)
                {
                    SendCommand = new RelayCommands(SendFc);
                }
                return SendCommand;
            }
        }
        public void SendFc(object param)
        {

            foreach(var index in servicesDataBindings)
            {
                if(index.CheckBool==true)
                {
                    reservationModel.Services.Add(new ServicesModel{ Name=index.Name,Price=index.Price,Id=index.Id });
                }
            }
            using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
            register.AddReservation(reservationModel);


            //MakeRevervationView firstPage = new MakeRevervationView();
            //MakeReservationViewModel firstPageModel = new MakeReservationViewModel(reservationModel);
            //firstPage.DataContext = firstPageModel;
            //App.Current.MainWindow.Close();
            //App.Current.MainWindow = firstPage;
            //firstPage.Show();

        }

        public ICommand DropCommand;
        public ICommand Drop
        {
            get
            {
                if (DropCommand == null)
                {
                    DropCommand = new RelayCommands(DropFc);
                }
                return DropCommand;
            }
        }
        public void DropFc(object param)
        {
            UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
            UnauthorizedClient firstPageModel = new UnauthorizedClient(UserModel.UserType.Customer,username:username);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();

        }









        public decimal priceT;
        public decimal PriceT
        {
            get
            {
                return priceT;
            }
            set
            {
                priceT = value;
                NotifyPropertyChanged("priceT");
            }
        }
        public decimal totalRooms;
        public decimal TotalRooms
        {
            get
            {
                return totalRooms;
            }
            set
            {
                totalRooms = value;
                NotifyPropertyChanged("totalRooms");
            }
        }
        public ObservableCollection<ServicesDataBinding> ServicesDataBindings
        {
            get
            {
                return servicesDataBindings;
            }
            set
            {
                servicesDataBindings = value;
                NotifyPropertyChanged("servicesDataBindings");
            }
        }

    }
}
