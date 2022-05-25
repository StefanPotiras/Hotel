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
        public UserModel.UserType funct;
        public ReservationModel reservationModel = new ReservationModel();
        public UnauthorizedClient(UserModel.UserType useType, ObservableCollection<TypeRoomsModelBinding> curentRooms2 = null,
          bool isFilter = false, DateTime StartDate1 = new DateTime(), DateTime EndDate2 = new DateTime(),
          bool isAdd = false, int indexCamera = -1,string username="", ReservationModel reservationModel2=null)
        {
            
            if (isAdd)
            {
                using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
                curentRooms = Helps.Convert.returnVector(register.GetRoomsByDate(StartDate1, EndDate2));

                decimal price = 0;
                foreach(var index in curentRooms)
                {
                    if(index.Id==indexCamera)
                    {
                        price = index.Price;
                    }
                }


                reservationModel = reservationModel2;
                reservationModel.EndDate = EndDate2;
                reservationModel.StartDate = StartDate1;
                reservationModel.Username = username;
                if (reservationModel.AllRoomsWithType.Count > 0)
                {
                    bool ok = false;
                    foreach (var index in reservationModel.AllRoomsWithType)
                    {
                        if (index.RoomTypeId == indexCamera)
                        {
                            ok = true;
                            index.NumberOfTypeRooms = index.NumberOfTypeRooms + 1;
                            reservationModel.Price += price;
                        }
                    }
                    if (ok == false)
                    {
                        reservationModel.AllRoomsWithType.Add(new RoomTypeNumberModel(indexCamera, 1));
                        reservationModel.Price += price;
                    }
                }
                else
                {
                    reservationModel.AllRoomsWithType.Add(new RoomTypeNumberModel(indexCamera, 1));
                    reservationModel.Price = price;
                }


                
               
              
                //reservationModel.AllRoomsWithType.Add(new RoomTypeNumberModel(indexCamera, reservationModel.AllRoomsWithType.Count+1));
                
            }
            if (isFilter)
            {
                curentRooms = curentRooms2;
                Helps.Convert.setTrue2(ref curentRooms);
            }
            else if(!isAdd)
            {
                using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
                curentRooms = Helps.Convert.returnVector(register.GetAllRooms());
            }
            funct = useType;
            if (useType == UserModel.UserType.Admin)
            {
                Helps.Convert.setTrue(ref curentRooms);
                visibility = true;
                visibilityAdmin = true;

            }
            else if (useType == UserModel.UserType.Employee)
            {
                visibility = true;
                visibilityAdmin = false;
            }
            else if (useType == UserModel.UserType.Customer)
            {
                visibility = true;
                Helps.Convert.setTrue2(ref curentRooms);
                visibilityAdmin = false;
                visibilityClient = true;
            }
            else
            {
                visibility = false;
                visibilityAdmin = false;
            }

            foreach(var index in curentRooms)
            {
                index.DateStart = StartDate1;
                index.DateEnd = EndDate2;
                index.reservationModel = reservationModel;
            }
            nrRoomsInRev = reservationModel.AllRoomsWithType.Count;
            foreach (var index in reservationModel.AllRoomsWithType)
            {
                if (index.NumberOfTypeRooms > 1)
                    nrRoomsInRev = nrRoomsInRev + index.NumberOfTypeRooms - 1;
            }
           
        }
        public ICommand AddReservCommand;
        public ICommand AddReserv
        {
            get
            {
                if (AddReservCommand == null)
                {
                    AddReservCommand = new RelayCommands(ReservFunct);
                }
                return AddReservCommand;
            }
        }
        public void ReservFunct(object param)
        {
            MainWindow firstPage = new MainWindow();
            FirstPageViewModel firstPageModel = new FirstPageViewModel();
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();

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
        private bool visibilityClient = false;
        public bool VisibilityClient
        {
            get
            {
                return visibilityClient;
            }
            set
            {
                visibilityClient = value;
                NotifyPropertyChanged("VisibilityClient");
            }
        }
        //NrRoomsInRev

        private int nrRoomsInRev ;
        public int NrRoomsInRev
        {
            get
            {
                return nrRoomsInRev;
            }
            set
            {
                nrRoomsInRev = value;
                NotifyPropertyChanged("NrRoomsInRev");
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
            ReservationsViewModel loginVM = new ReservationsViewModel(funct);
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
            string indexRoom = (buttonClicked as Button).Content.ToString();
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
            AddNewRoomViewModel firstPageModel = new AddNewRoomViewModel(new TypeRoomsModelBinding(), false);
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
            curentRooms = Helps.Convert.returnVector(register.GetRoomsByDate(StartDate, EndDate));
            foreach (var index in curentRooms)
            {
                index.DateEnd = EndDate;
                index.DateStart = StartDate;
            }
            UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
            UnauthorizedClient firstPageModel = new UnauthorizedClient(funct, curentRooms, true);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
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
            UnauthorizedClient firstPageModel = new UnauthorizedClient(funct, curentRooms, false);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
    }
}
