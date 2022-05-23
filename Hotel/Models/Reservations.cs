using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hotel.Models
{
 
    public class Reservations : INotifyPropertyChanged
    {
        public Reservations() { }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string name;
        public string dateStart;
        public string dateEnd;
        public int roomNumber;
        public string cameraType;
        public status reservationStatus;

        public string CameraType
        {
            get
            {
                return cameraType;
            }
            set
            {
                cameraType = value;
                NotifyPropertyChanged("tipCamera");
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public string DateStart
        {
            get
            {
                return dateStart;
            }
            set
            {
                dateStart = value;
                NotifyPropertyChanged("DateStart");
            }
        }
        public string DateEnd
        {
            get
            {
                return dateEnd;
            }
            set
            {
                dateEnd = value;
                NotifyPropertyChanged("dateStart");
            }
        }
        public int RoomNumber
        {
            get
            {
                return roomNumber;
            }
            set
            {
                roomNumber = value;
                NotifyPropertyChanged("RoomNumber");
            }
        }
        public status ReservationsStatus
        {
            get
            {
                return reservationStatus;
            }
            set
            {
                reservationStatus = value;
                NotifyPropertyChanged("EeservationStatus");
            }
        }
        public enum status
        {
            platit, neplatit, anulat,
        }
    }
}
