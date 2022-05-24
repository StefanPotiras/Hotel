using System;
using System.Collections.Generic;
using System.Text;
using Hotel.ViewModel;
using Hotel.ViewModels;
using Hotel.Models;
using Hotel.Helps;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Collections.ObjectModel;
using ModelsClasses;

namespace Hotel.ViewModel
{
    public class DetailRoomViewModel : NotifyViewModel
    {
        private Images images = new Images();
        public DetailRoomViewModel()
        { }
        TypeRoomsModelBinding typeRoomsModelBinding = new TypeRoomsModelBinding();
        public DetailRoomViewModel(TypeRoomsModelBinding typeRoomsModelBinding)
        {
           this.typeRoomsModelBinding = typeRoomsModelBinding;
            string descFinal="";
            foreach(var index in typeRoomsModelBinding.Features)
            {
                descFinal += index+",";
            }
            featuresR = descFinal;

            imageSource = typeRoomsModelBinding.allImages[0];
        }
        public string featuresR;
        public string FeaturesR
        {
            get
            {
                return featuresR;
            }
            set
            {
                featuresR = value;
                NotifyPropertyChanged("Features");
            }
        }
        private BitmapImage imageSource;
        public BitmapImage ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;              
                NotifyPropertyChanged("ImageSource");
            }
        }
        public ObservableCollection<BitmapImage> ImagesRoom
        {
            get
            {
                return typeRoomsModelBinding.allImages;
            }
            set
            {
                typeRoomsModelBinding.allImages = value;
                NotifyPropertyChanged("ImagesRoom");
            }
        }


        public int IDR
        {
            get
            {
                return typeRoomsModelBinding.Id;
            }
            set
            {
                typeRoomsModelBinding.Id = value;
                NotifyPropertyChanged("ID");
            }
        }

        public decimal PriceR
        {
            get
            {
                return typeRoomsModelBinding.Price;
            }
            set
            {
                typeRoomsModelBinding.Price = value;
                NotifyPropertyChanged("Price");
            }
        }

        public string DescriptionR
        {
            get
            {
                return typeRoomsModelBinding.Description;
            }
            set
            {
                typeRoomsModelBinding.Description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public int CapacityR
        {
            get
            {
                return typeRoomsModelBinding.Capacity;
            }
            set
            {
                typeRoomsModelBinding.Capacity = value;
                NotifyPropertyChanged("CapacityR");
            }
        }

        public string RoomTitleR
        {
            get
            {
                return typeRoomsModelBinding.RoomTitle;
            }
            set
            {
                typeRoomsModelBinding.RoomTitle = value;
                NotifyPropertyChanged("RoomTitle");
            }
        }

        public int NumberOfRoomsR
        {
            get
            {
                return typeRoomsModelBinding.NumberOfRooms;
            }
            set
            {
                typeRoomsModelBinding.NumberOfRooms = value;
                NotifyPropertyChanged("NumberOfRooms");
            }
        }

       


        public ObservableCollection<ImageModel> ImagesR
        {
            get
            {
                return typeRoomsModelBinding.Images;
            }
            set
            {
                typeRoomsModelBinding.Images = value;
                NotifyPropertyChanged("Images");
            }
        }
        private ICommand nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new RelayCommands(NextMethod);
                }
                return nextCommand;
            }
        }

        public void NextMethod(object param)
        {
            int index = typeRoomsModelBinding.allImages.IndexOf(ImageSource);
            if (index < typeRoomsModelBinding.allImages.Count - 1)
            {
                ImageSource = typeRoomsModelBinding.allImages[++index];
            }
        }

        private ICommand prevCommand;
        public ICommand PrevCommand
        {
            get
            {
                if (prevCommand == null)
                {
                    prevCommand = new RelayCommands(PrevMethod);
                }
                return prevCommand;
            }
        }
        public void PrevMethod(object param)
        {
            int index = typeRoomsModelBinding.allImages.IndexOf(ImageSource);
            if (index > 0)
            {
                ImageSource = typeRoomsModelBinding.allImages[--index];
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
            UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
            UnauthorizedClient firstPageModel = new UnauthorizedClient(UserModel.UserType.Admin);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
    }
}
