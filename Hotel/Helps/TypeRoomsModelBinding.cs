using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Hotel.Models;
using Hotel.ViewModels;
using Hotel.Views;
using ModelsClasses;
using Server;

namespace Hotel.Helps
{
    public class TypeRoomsModelBinding : RoomTypeModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ObservableCollection<BitmapImage> allImages = new ObservableCollection<BitmapImage>();
        public TypeRoomsModelBinding()
        {
            foreach (var index in Images )
            {
                allImages.Add(Test.ToImage(index.Data)) ;
            }
        }
        public void convertImages()
        {
            foreach (var index in Images)
            {
                allImages.Add(Test.ToImage(index.Data));
            }
        }
        //AddRoomInReserv

        private ICommand AddRoomInReservComm;
        public ICommand AddRoomInReserv
        {
            get
            {
                if (AddRoomInReservComm == null)
                {
                    AddRoomInReservComm = new RelayCommands(AddRoomInReservFc);
                }
                return AddRoomInReservComm;
            }
        }
        public void AddRoomInReservFc(object buttonClicked)
        {
            string indexRoom = buttonClicked.ToString();         
            ObservableCollection<TypeRoomsModelBinding> curentRooms2 = new ObservableCollection<TypeRoomsModelBinding>();
            UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
            UnauthorizedClient firstPageModel = new UnauthorizedClient(UserModel.UserType.Customer, curentRooms2);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }





        private ICommand DeleteRoomCommand;
        public ICommand DeleteRoom
        {
            get
            {
                if (DeleteRoomCommand == null)
                {
                    DeleteRoomCommand = new RelayCommands(DeleteRoomFc);
                }
                return DeleteRoomCommand;
            }
        }
        public void DeleteRoomFc(object buttonClicked)
        {
            string indexRoom = buttonClicked.ToString();
            using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
            register.DeleteRoomType(Int32.Parse(indexRoom));
            ObservableCollection<TypeRoomsModelBinding> curentRooms2=new ObservableCollection<TypeRoomsModelBinding>();
            UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
            UnauthorizedClient firstPageModel = new UnauthorizedClient(UserModel.UserType.Admin, curentRooms2);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
        private ICommand ModifyRoomComand;
        public ICommand ModifyRoom
        {
            get
            {
                if (ModifyRoomComand == null)
                {
                    ModifyRoomComand = new RelayCommands(ModifiyFc);
                }
                return ModifyRoomComand;
            }
        }
        public void ModifiyFc(object buttonClicked)
        {
            TypeRoomsModelBinding typeRoomsModelBinding = new TypeRoomsModelBinding();
            typeRoomsModelBinding.Id = Id;
            typeRoomsModelBinding.Price = Price;
            typeRoomsModelBinding.Description = Description;
            typeRoomsModelBinding.Capacity = Capacity;
            typeRoomsModelBinding.NumberOfRooms = NumberOfRooms;
            typeRoomsModelBinding.Features = Features;
            typeRoomsModelBinding.Images = Images;
            typeRoomsModelBinding.RoomTitle = RoomTitle;
            typeRoomsModelBinding.convertImages();


            AddNewRoomView firstPage = new AddNewRoomView();       
            AddNewRoomViewModel firstPageModel = new AddNewRoomViewModel(typeRoomsModelBinding,true);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
        public ObservableCollection<BitmapImage> ImagesRoom
        {
            get
            {
                return allImages;
            }
            set
            {
                allImages = value;
                NotifyPropertyChanged("ImagesRoom");
            }
        }


        public int IDR
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
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
        
        public string DescriptionR
        {
            get
            {
                return Description;
            }
            set
            {
                Description = value;
                NotifyPropertyChanged("Description");
            }
        }
       
        public int CapacityR
        {
            get
            {
                return Capacity;
            }
            set
            {
                Capacity = value;
                NotifyPropertyChanged("CapacityR");
            }
        }
       
        public string RoomTitleR
        {
            get
            {
                return RoomTitle;
            }
            set
            {
                RoomTitle = value;
                NotifyPropertyChanged("RoomTitle");
            }
        }
        public DateTime DateStart;
        public DateTime DateEnd;
       
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

        public ObservableCollection<FeatureModel> FeaturesR
        {
            get
            {
                return Features;
            }
            set
            {
                Features = value;
                NotifyPropertyChanged("Features");
            }
        }

        public ReservationModel reservationModel;

        public ReservationModel ReservationModelPac
        {
            get
            {
                return reservationModel;
            }
            set
            {
                reservationModel = value;
                NotifyPropertyChanged("ReservationModelPac");
            }
        }



        public ObservableCollection<ImageModel> ImagesR
        {
            get
            {
                return Images;
            }
            set
            {
                Images = value;
                NotifyPropertyChanged("Images");
            }
        }

        public  bool visibilityAdmin = false;
        public bool VisibilityAdmin
        {
            get
            {
                return visibilityAdmin;
            }
            set
            {
                visibilityAdmin = value;
                NotifyPropertyChanged("visibilityAdmin");
            }
        }

        public bool visibilityClient = false;
        public bool VisibilityClient
        {
            get
            {
                return visibilityClient;
            }
            set
            {
                visibilityClient = value;
                NotifyPropertyChanged("VisibilityClient");
            }
        }
    }
}
