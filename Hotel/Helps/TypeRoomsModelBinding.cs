﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ModelsClasses;

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
