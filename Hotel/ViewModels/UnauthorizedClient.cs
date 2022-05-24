using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel.Helps;
using Hotel.Models;
using Hotel.ViewModel;
using Hotel.Views;
using ModelsClasses;
using System.Windows.Media.Imaging;
using System.Drawing;
using Server;

namespace Hotel.ViewModels
{
    public class UnauthorizedClient : NotifyViewModel
    {
       
        ObservableCollection<TypeRoomsModelBinding> curentRooms = new ObservableCollection<TypeRoomsModelBinding>();
        public UnauthorizedClient()
        { }
        public  UserModel.UserType funct;
        public UnauthorizedClient(UserModel.UserType useType)
        {
            funct = useType;
            if (useType == UserModel.UserType.Admin)
            {
                visibility = true;
                visibilityAdmin = true;
            }
            else if (useType == UserModel.UserType.Employee)
            {
                visibility = true;
                visibilityAdmin = false;
            }
            else if(useType == UserModel.UserType.Customer)
            {

            }
            using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
            curentRooms = Helps.Convert.returnVector(register.GetAllRooms());
        }

        public ObservableCollection<TypeRoomsModelBinding> HotelsCurrent
        {
            get
            {
                return curentRooms;
            }
        }
        private DateTime selectedStartDate;
        public DateTime StartDate
        {
            get
            {
                return selectedStartDate;
            }
            set
            {

                selectedStartDate = value;
                NotifyPropertyChanged("StartDate");
            }
        }
        private DateTime selectEndDate;
        public DateTime EndDate
        {
            get
            {
                return selectEndDate;
            }
            set
            {
                selectEndDate = value;
                NotifyPropertyChanged("EndDate");
            }
        }

        private bool visibility = false;
        public bool Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                NotifyPropertyChanged("Visibility");
            }
        }
        private bool visibilityAdmin = false;
        public bool VisibilityAdmin
        {
            get
            {
                return visibilityAdmin;
            }
            set
            {
                visibilityAdmin = value;
                NotifyPropertyChanged("visibilityAdmin");
            }
        }
        private TypeRoomsModelBinding selectedRoom;
        public TypeRoomsModelBinding SelectedRoom
        {
            get
            {
                return selectedRoom;
            }
            set
            {
                selectedRoom = value;
                NotifyPropertyChanged("SelectedRoom");
                if (selectedRoom != null)
                {
                    DetaliiCamera detailRoom = new DetaliiCamera();
                    DetailRoomViewModel loginVM = new DetailRoomViewModel(SelectedRoom);
                    detailRoom.DataContext = loginVM;
                    App.Current.MainWindow.Close();
                    App.Current.MainWindow = detailRoom;
                    detailRoom.Show();
                }
            }
        }
        private ICommand ReservationsCommand;
        public ICommand ReservationsTabel
        {
            get
            {
                if (ReservationsCommand == null)
                {
                    ReservationsCommand = new RelayCommands(ReservationsFunction);
                }
                return ReservationsCommand;
            }
        }

        public void ReservationsFunction(object param)
        {
            ReservationsView loginWindow = new ReservationsView();
            ReservationsViewModel loginVM = new ReservationsViewModel();
            loginWindow.DataContext = loginVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = loginWindow;
            loginWindow.Show();
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
            MainWindow firstPage = new MainWindow();
            FirstPageViewModel firstPageModel = new FirstPageViewModel();
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();

        }
        private ICommand DeleteRoomCommand;
        public ICommand DeleteRoom
        {
            get
            {
                if (DeleteRoomCommand == null)
                {
                    DeleteRoomCommand = new RelayCommands(DeleteRoomFc);
                }
                return DeleteRoomCommand;
            }
        }
        public void DeleteRoomFc(object buttonClicked)
        {
            string indexRoom= (buttonClicked as Button).Content.ToString();
            MainWindow firstPage = new MainWindow();
            FirstPageViewModel firstPageModel = new FirstPageViewModel();
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
        private ICommand AddNewRommCommand;
        public ICommand AddRoom
        {
            get
            {
                if (AddNewRommCommand == null)
                {
                    AddNewRommCommand = new RelayCommands(AddNewRoomFc);
                }
                return AddNewRommCommand;
            }
        }
        public void AddNewRoomFc(object buttonClicked)
        {
            AddNewRoomView firstPage = new AddNewRoomView();
            AddNewRoomViewModel firstPageModel = new AddNewRoomViewModel(new TypeRoomsModelBinding(),false);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
        private ICommand AddServicesComand;
        public ICommand AddServices
        {
            get
            {
                if (AddServicesComand == null)
                {
                    AddServicesComand = new RelayCommands(AddServicesFc);
                }
                return AddServicesComand;
            }
        }
        public void AddServicesFc(object buttonClicked)
        {
            AddServicesView firstPage = new AddServicesView();
            AddNewServicesViewModel firstPageModel = new AddNewServicesViewModel();
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }

        private ICommand SearchCommand;
        public ICommand SearchBind
        {
            get
            {
                if (SearchCommand == null)
                {
                    SearchCommand = new RelayCommands(SearchFc);
                }
                return SearchCommand;
            }
        }
        public void SearchFc(object buttonClicked)
        {
            using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
            curentRooms = Helps.Convert.returnVector( register.GetRoomsByDate(StartDate,EndDate));

        }
        private ICommand DeleteFilterCommand;
        public ICommand DeleteFilter
        {
            get
            {
                if (DeleteFilterCommand == null)
                {
                    DeleteFilterCommand = new RelayCommands(DeleteFc);
                }
                return DeleteFilterCommand;
            }
        }
        public void DeleteFc(object buttonClicked)
        {           
            UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
            UnauthorizedClient firstPageModel = new UnauthorizedClient(funct);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
    }
}
