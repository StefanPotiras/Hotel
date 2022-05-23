using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Hotel.Helps;
using Hotel.Models;
using ModelsClasses;
using Server;

namespace Hotel.ViewModels
{
    class LoginViewModel : NotifyViewModel
    {
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
        public string usernameTextBox;
        public string UsernameTextBox
        {
            get
            {
                return usernameTextBox;
            }
            set
            {
                usernameTextBox = value;
                NotifyPropertyChanged("UsernameTextBox");

            }
        }
        public string passwordTextBox;
        public string PasswordTextBox
        {
            get
            {
                return passwordTextBox;
            }
            set
            {
                passwordTextBox = value;
                NotifyPropertyChanged("PasswordTextBox");
            }
        }

        private ICommand LoginCommand;
        public ICommand Login
        {
            get
            {
                if (LoginCommand == null)
                {
                    LoginCommand = new RelayCommands(LoginFunction);
                }
                return LoginCommand;
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

        public void LoginFunction(object param)
        {
            if (usernameTextBox != null && passwordTextBox != null)
            {
                using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
                UserModel.UserType userType= register.Login(usernameTextBox,passwordTextBox);
                if (userType!=UserModel.UserType.None)
                { UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
                    UnauthorizedClient firstPageModel = new UnauthorizedClient(userType);
                    firstPage.DataContext = firstPageModel;
                    App.Current.MainWindow.Close();
                    App.Current.MainWindow = firstPage;
                    firstPage.Show();
                }
                else
                {
                    visibility = true;
                }
                
            }

        }
    }
}
