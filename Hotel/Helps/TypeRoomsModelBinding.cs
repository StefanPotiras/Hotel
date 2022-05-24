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

            UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
            UnauthorizedClient firstPageModel = new UnauthorizedClient(UserModel.UserType.Admin);
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
    }
}
