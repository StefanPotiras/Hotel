using Hotel.Helps;
using ModelsClasses;
using Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hotel.ViewModels
{
    public class SingUpViewModel : NotifyViewModel
    {
        public SingUpViewModel() { }

        bool canSendRequest = false;


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
                if (usernameTextBox == null)
                    canSendRequest = false;
                else
                    canSendRequest = true;
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
                if (passwordTextBox == null)
                    canSendRequest = false;
                else
                    canSendRequest = true;
            }
        }
        public string firstNameTextBox;
        public string FirstNameTextBox
        {
            get
            {
                return firstNameTextBox;
            }
            set
            {
                firstNameTextBox = value;
                NotifyPropertyChanged("FirstNameTextBox");
                if (firstNameTextBox == null)
                    canSendRequest = false;
                else
                    canSendRequest = true;
            }
        }
        public string secondNameTextBox;
        public string SecondNameTextBox
        {
            get
            {
                return secondNameTextBox;
            }
            set
            {
                secondNameTextBox = value;
                NotifyPropertyChanged("SecondNameTextBox");
                if (secondNameTextBox == null)
                    canSendRequest = false;
                else
                    canSendRequest = true;
            }
        }
        public string selectedFunction;
        public string SelectedFunction
        {
            get
            {
                return selectedFunction;
            }
            set
            {
                selectedFunction = value;
                NotifyPropertyChanged("SelectedFunction");
                if (selectedFunction == null)
                    canSendRequest = false;
                else
                    canSendRequest = true;
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
        private ICommand RegisterCommand;
        public ICommand Register
        {
            get
            {
                if (RegisterCommand == null)
                {
                    RegisterCommand = new RelayCommands(RegisterFunction);
                }
                return RegisterCommand;
            }
        }

        public void RegisterFunction(object param)
        {
            if (usernameTextBox == null || passwordTextBox == null || firstNameTextBox == null || secondNameTextBox == null || selectedFunction == null)
            {
                Visibility = true;
            }
            else
            {
                UserModel.UserType userType = new UserModel.UserType();
                if (selectedFunction == "Admin")
                {
                    userType = UserModel.UserType.Admin;
                }
                else if (selectedFunction == "Employee")
                {
                    userType = UserModel.UserType.Employee;
                }
                else if (selectedFunction == "Customer")
                {
                    userType = UserModel.UserType.Customer;
                }
                using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
                register.Register(new UserModel { FirstName = firstNameTextBox, LastName = secondNameTextBox, Password = passwordTextBox, Username = usernameTextBox, Type = userType });
                MainWindow firstPageWindow = new MainWindow();
                FirstPageViewModel firstV = new FirstPageViewModel();
                firstPageWindow.DataContext = firstV;
                App.Current.MainWindow.Close();
                App.Current.MainWindow = firstPageWindow;
                firstPageWindow.Show();
            }

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
