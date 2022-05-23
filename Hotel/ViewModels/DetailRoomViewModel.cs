using System;
using System.Collections.Generic;
using System.Text;
using Hotel.ViewModel;
using Hotel.ViewModels;
using Hotel.Models;
using Hotel.Helps;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    public class DetailRoomViewModel : NotifyViewModel
    {
        private Images images = new Images();
        public DetailRoomViewModel()
        {

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
        public void NextMethod(object param)
        {
            int index = images.images.IndexOf(ImageSource);
            if (index < images.images.Count - 1)
            {
                ImageSource = images.images[++index];
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
            int index = images.images.IndexOf(ImageSource);
            if (index > 0)
            {
                ImageSource = images.images[--index];
            }
        }
    }
}
