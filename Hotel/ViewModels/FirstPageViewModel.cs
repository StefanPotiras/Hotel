using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Hotel.Helps;
using Hotel.Models;
using ModelsClasses;

namespace Hotel.ViewModels
{
    class FirstPageViewModel : NotifyViewModel
    {
        private const string V = "C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img2.jpg";

        public FirstPageViewModel()
        {
           
        }
      
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
            UnauthorizedClientModel roomsWondow = new UnauthorizedClientModel();
            UnauthorizedClient loginVM = new UnauthorizedClient(UserModel.UserType.None);
            roomsWondow.DataContext = loginVM;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = roomsWondow;
            roomsWondow.Show();
        }
        private ICommand StaiAsaC;
        public ICommand StaiAsa
        {
            get
            {
                if (StaiAsaC == null)
                {
                    StaiAsaC = new RelayCommands(test123);
                }
                return StaiAsaC;
            }
        }
       async public void test123(object param)
        { 
           
        }
        public string restApi;
        public string RestApi
        {
            get
            {
                return restApi;
            }
            set
            {
                restApi = value;
                NotifyPropertyChanged("restApi");

            }
        }
    }
}
