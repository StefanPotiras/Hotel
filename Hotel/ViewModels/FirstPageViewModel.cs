using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Hotel.Helps;
using Hotel.Models;
namespace Hotel.ViewModels
{
    class FirstPageViewModel:NotifyViewModel
    {
        public bool CanExecuteCommandSignIn { get; set; } = false;

        private ICommand LoginCommand;
        public ICommand Logare
        {
            get
            {
                if (LoginCommand == null)
                {
                    LoginCommand = new RelayCommands(SignIn);
                }
                return LoginCommand;
            }
        }
        public void SignIn(object param)
        {
            Login loginWindow = new Login();
            LoginViewModel loginVM = new LoginViewModel();
            loginWindow.DataContext = loginVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = loginWindow;
            loginWindow.Show();
        }
        private ICommand RegisterComand;
        public ICommand Inregistrare
        {
            get
            {
                if (RegisterComand == null)
                {
                    RegisterComand = new RelayCommands(SingUP);
                }
                return RegisterComand;
            }
        }
        public void SingUP(object param)
        {
            SingUpModel loginWindow = new SingUpModel();
            SingUpViewModel loginVM = new SingUpViewModel();
            loginWindow.DataContext = loginVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = loginWindow;
            loginWindow.Show();
        }

        private ICommand NextCommand;
        public ICommand Next
        {
            get
            {
                if (NextCommand == null)
                {
                    NextCommand = new RelayCommands(NextFunction);
                }
                return NextCommand;
            }
        }
        public void NextFunction(object param)
        {
            UnauthorizedClientModel loginWindow = new UnauthorizedClientModel();
            UnauthorizedClient loginVM = new UnauthorizedClient(true);
            loginWindow.DataContext = loginVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = loginWindow;
            loginWindow.Show();
        }
    }
}
