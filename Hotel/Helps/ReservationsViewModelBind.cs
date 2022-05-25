using Hotel.Models;
using Hotel.ViewModels;
using Hotel.Views;
using ModelsClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        private ICommand ChangeCommand;
        public ICommand Change
        {
            get
            {
                if (ChangeCommand == null)
                {
                    ChangeCommand = new RelayCommands(ChangeFc);
                }
                return ChangeCommand;
            }
        }
        public UserModel.UserType userType;
        public void ChangeFc(object buttonClicked)
        {
            string indexRoom = buttonClicked.ToString();
                    
            ChangeStateView firstPage = new ChangeStateView();
            ChangeStateViewModel firstPageModel = new ChangeStateViewModel(Int32.Parse(indexRoom),State,userType);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
        public int IdR
        {
            get
            {
                return IdReserv;
            }
            set
            {
                IdReserv = value;
                NotifyPropertyChanged("UserId");
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
                NotifyPropertyChanged("Username");
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
                return StartDate.Date.ToString();
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
                return EndDate.Date.ToString();
            }
            set
            {
                EndDate = DateTime.Parse(value);
                NotifyPropertyChanged("EndDate");
            }
        }

        public ModelsClasses.ReservationState StateR
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
