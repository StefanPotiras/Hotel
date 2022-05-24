using ModelsClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Helps
{
    public class ReservationsViewModelBind: ReservationModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
        public string UsernameR
        {
            get
            {
                return Username;
            }
            set
            {
                Username = value;
                NotifyPropertyChanged("ID");
            }
        }
        public decimal PriceR
        {
            get
            {
                return Price;
            }
            set
            {
                Price = value;
                NotifyPropertyChanged("Price");
            }
        }
        public string StartDateR
        {
            get
            {
                return StartDate.ToString();
            }
            set
            {
                StartDate = DateTime.Parse(value);
                NotifyPropertyChanged("StartDate");
            }
        }
        public string EndDateR
        {
            get
            {
                return EndDate.ToString();
            }
            set
            {
                EndDate = DateTime.Parse(value);
                NotifyPropertyChanged("EndDate");
            }
        }

        public ReservationState StateR
        {
            get
            {
                return State;
            }
            set
            {
                State =value;
                NotifyPropertyChanged("State");
            }
        }
        public int NumberOfRoomsR
        {
            get
            {
                return NumberOfRooms;
            }
            set
            {
                NumberOfRooms = value;
                NotifyPropertyChanged("NumberOfRooms");
            }
        }
    }
}
