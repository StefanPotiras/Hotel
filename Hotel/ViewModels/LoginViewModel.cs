using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Hotel.Helps;
using Hotel.Models;

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


        public void LoginFunction(object param)
        {
            if (usernameTextBox != null && passwordTextBox != null)
            {
                string result = "angajat";
                if (result == "angajat")
                {
                    UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
                    UnauthorizedClient firstPageModel = new UnauthorizedClient(true);
                    firstPage.DataContext = firstPageModel;
                    App.Current.MainWindow.Close();
                    App.Current.MainWindow = firstPage;
                    firstPage.Show();
                }
                else if (result == "admin")
                {

                }
                else if (result == "client")
                {

                }
            }

        }
    }
}
