using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Media.Imaging;

namespace Hotel.Models
{
    public class Images: INotifyPropertyChanged
    {
        public Images() { }
        public BitmapImage images = new BitmapImage();
        public int idImage;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public BitmapImage ImagesList
        {
            get
            {
                return images;
            }
            set
            {
                images = value;
                NotifyPropertyChanged("ImagesList");

            }
        }

        public int IdImage
        {
            get
            {
                return idImage;
            }
            set
            {
                idImage = value;
                NotifyPropertyChanged("IdImage");

            }
        }
    }
}
