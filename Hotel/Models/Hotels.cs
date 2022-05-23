using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

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

        public string image;
        public string tipCamera;
        public string pret;
        public string numarPersoane;
        public string numberOfRooms;

        public ObservableCollection<string> images { get; set; }



        public ObservableCollection<string> ImagesRoom
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
