using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Hotel.Helps;
using Hotel.Models;
using Hotel.ViewModel;
using Hotel.Views;

namespace Hotel.ViewModels
{
    public class UnauthorizedClient : NotifyViewModel
    {

        ObservableCollection<Hotels> hotelsCurrent = new ObservableCollection<Hotels>();
        public UnauthorizedClient()
        { }
        public UnauthorizedClient(bool isEmployee)
        {
            Hotels temp1 = new Hotels();
            temp1.tipCamera = "Camera cu 2 paturi";
            temp1.pret = "100$";
            temp1.numarPersoane = "2";

            ObservableCollection<string> images = new ObservableCollection<string>();
            images.Add("C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img1.jpg");

            temp1.images = images;
            //temp1.imagesRoom.images.Add("C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img1.jpg");
            Hotels temp2 = new Hotels();
            temp2.tipCamera = "Camera cu 5 paturi";
            temp2.pret = "150$";
            temp2.numarPersoane = "10";
            // temp2.imagesRoom.images.Add("C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img1.jpg");
            ObservableCollection<string> images2 = new ObservableCollection<string>();
            images2.Add("C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img2.jpg");
            temp2.images = images2;
            hotelsCurrent.Add(temp1);
            hotelsCurrent.Add(temp2);
            visibility = isEmployee;
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

        private bool visibility;
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
    }
}
