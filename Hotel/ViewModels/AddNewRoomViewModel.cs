using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ViewModels
{
    class AddNewRoomViewModel : NotifyViewModel
    {
        public AddNewRoomViewModel()
        {
            roomNumberTextBox = "Test";
            bucatarie = true;
            gratar = true;
            foisor = true;
            selectedType = "DoubleRoom";
        }
        public string roomNumberTextBox;
        public string priceTextBox;
        public string descriptionTextBox;
        public string capacityTextBox;
        public string selectedType;
        public bool gratar;
        public bool foisor;
        public bool balcon;
        public bool masinaDeSpalat;
        public bool bucatarie;
        public ObservableCollection<ObservableCollection<byte>> matrixImages=new ObservableCollection<ObservableCollection<byte>>();


        public int ImageNumber
        {
            get
            { 
                return matrixImages.Count;
            }
            
           
        }
        public string PriceTextBox
        {
            get
            {
                return priceTextBox;
            }
            set
            {
                priceTextBox = value;
                NotifyPropertyChanged("priceTextBox");
            }
        }

        public string DescriptionTextBox
        {
            get
            {
                return descriptionTextBox;
            }
            set
            {
                descriptionTextBox = value;
                NotifyPropertyChanged("descriptionTextBox");
            }
        }


        public string CapacityTextBox
        {
            get
            {
                return capacityTextBox;
            }
            set
            {
                capacityTextBox = value;
                NotifyPropertyChanged("SelectedType");
            }
        }
        public string SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                selectedType = value;
                NotifyPropertyChanged("SelectedType");
            }
        }

        public bool Gratar
        {
            get
            {
                return gratar;
            }
            set
            {
                gratar = value;
                NotifyPropertyChanged("Gratar");
            }
        }

        public bool Foisor
        {
            get
            {
                return foisor;
            }
            set
            {
                foisor = value;
                NotifyPropertyChanged("Foisor");
            }
        }

        public bool Balcon
        {
            get
            {
                return balcon;
            }
            set
            {
                balcon = value;
                NotifyPropertyChanged("Balcon");
            }
        }
        public string RoomNumberTextBox
        {
            get
            {
                return roomNumberTextBox;
            }
            set
            {
                roomNumberTextBox = value;
                NotifyPropertyChanged("PasswordTextBox");
            }
        }
        public bool Bucatarie
        {
            get
            {
                return bucatarie;
            }
            set
            {
                bucatarie = value;
                NotifyPropertyChanged("Bucatarie");
            }
        }
        public bool MasinaDeSpalat
        {
            get
            {
                return masinaDeSpalat;
            }
            set
            {
                masinaDeSpalat = value;
                NotifyPropertyChanged("MasinaDeSpalat");
            }
        }
    }
}
