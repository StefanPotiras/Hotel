using Hotel.Helps;
using Hotel.Models;
using Hotel.Views;
using ModelsClasses;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModels
{
    class AddNewServicesViewModel : NotifyViewModel
    {
        public AddNewServicesViewModel() { }
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
            UnauthorizedClient firstPageModel = new UnauthorizedClient(UserModel.UserType.Admin);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
        public string sericesTextBox;
        public string ServicesTextBox
        {
            get
            {
                return sericesTextBox;
            }
            set
            {
                sericesTextBox = value;
                NotifyPropertyChanged("UsernameTextBox");

            }
        }
        public string priceTextBox;
        public string PriceTextBox
        {
            get
            {
                return priceTextBox;
            }
            set
            {
                priceTextBox = value;
                NotifyPropertyChanged("PasswordTextBox");
            }
        }

        private ICommand SendCommand;
        public ICommand Send
        {
            get
            {
                if (SendCommand == null)
                {
                    SendCommand = new RelayCommands(SendFunction);
                }
                return SendCommand;
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

        public void SendFunction(object param)
        {
            if(priceTextBox!=null && sericesTextBox!=null)
            {
                ServicesModel servicesModel = new ServicesModel();
                servicesModel.Name = sericesTextBox;
                servicesModel.Price = Decimal.Parse(priceTextBox);
                using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
                register.AddService(servicesModel);
                UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
                UnauthorizedClient firstPageModel = new UnauthorizedClient(UserModel.UserType.Admin);
                firstPage.DataContext = firstPageModel;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = firstPage;
                firstPage.Show();
            }
            else
            {
                Visibility = true;
            }

        }
    }
}
