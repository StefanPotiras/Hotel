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

namespace Hotel.ViewModels
{
    public class UnauthorizedClient : NotifyViewModel
    {

        ObservableCollection<Hotels> hotelsCurrent = new ObservableCollection<Hotels>();
        public UnauthorizedClient()
        { }
        public UnauthorizedClient(UserModel.UserType useType)
        {
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
            Hotels temp1 = new Hotels(visibilityAdmin);
            temp1.tipCamera = "Camera cu 2 paturi";
            temp1.pret = "100$";
            temp1.numarPersoane = "2";
            temp1.nrRoom = "3";
            ObservableCollection<string> images = new ObservableCollection<string>();
            images.Add("C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img1.jpg");

            temp1.images = images;
            //temp1.imagesRoom.images.Add("C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img1.jpg");
            Hotels temp2 = new Hotels(visibilityAdmin);
            temp2.tipCamera = "Camera cu 5 paturi";
            temp2.pret = "150$";
            temp2.numarPersoane = "10";
            // temp2.imagesRoom.images.Add("C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img1.jpg");
            ObservableCollection<string> images2 = new ObservableCollection<string>();
            images2.Add("C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img2.jpg");
            temp2.images = images2;
            hotelsCurrent.Add(temp1);
            hotelsCurrent.Add(temp2);

        }

        public ObservableCollection<Hotels> HotelsCurrent
        {
            get
            {
                return hotelsCurrent;
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
        private Hotels selectedRoom;
        public Hotels SelectedRoom
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
                    DetailRoomViewModel loginVM = new DetailRoomViewModel();
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
            AddNewRoomViewModel firstPageModel = new AddNewRoomViewModel();
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
    }
}
