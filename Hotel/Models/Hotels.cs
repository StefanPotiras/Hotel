using Hotel.Helps;
using Hotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Hotel.Models
{
    public class Hotels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public Hotels(bool vis)
        {
            VisibilityAdmin = vis;
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
            string indexRoom = buttonClicked.ToString();
            MainWindow firstPage = new MainWindow();
            FirstPageViewModel firstPageModel = new FirstPageViewModel();
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
        public string image;
        public string tipCamera;
        public string pret;
        public string numarPersoane;
        public string numberOfRooms;
        public string nrRoom;
        public bool vizibilityAdmin;

        public ObservableCollection<BitmapImage> images { get; set; }
        public string NrRoom
        {
            get
            {
                return nrRoom;
            }
            set
            {
                nrRoom = value;
                NotifyPropertyChanged("nrRoom");
            }
        }
        public bool VisibilityAdmin
        {
            get
            {
                return vizibilityAdmin;
            }
            set
            {
                vizibilityAdmin = value;
                NotifyPropertyChanged("vizibilityAdmin");
            }
        }

        public ObservableCollection<BitmapImage> ImagesRoom
        {
            get
            {
                return images;
            }
            set
            {
                images = value;
                NotifyPropertyChanged("tipCamera");
            }
        }
        public string TipCamera
        {
            get
            {
                return tipCamera;
            }
            set
            {
                tipCamera = value;
                NotifyPropertyChanged("tipCamera");
            }
        }
        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                NotifyPropertyChanged("Image");
            }
        }
        public string Pret
        {
            get
            {
                return pret;
            }
            set
            {
                pret = value;
                NotifyPropertyChanged("Pret");
            }
        }
        public string NumarPersoane
        {
            get
            {
                return numarPersoane;
            }
            set
            {
                numarPersoane = value;
                NotifyPropertyChanged("NumarPersoane");
            }
        }
        public string NumberOfRooms
        {
            get
            {
                return numberOfRooms;
            }
            set
            {
                numberOfRooms = value;
                NotifyPropertyChanged("NumberOfRooms");
            }
        }
    }
}
