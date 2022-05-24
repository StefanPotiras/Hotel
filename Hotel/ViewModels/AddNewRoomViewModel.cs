using Hotel.Helps;
using ModelsClasses;
using Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
        public ObservableCollection<Image> vectorImg = new ObservableCollection<Image>();
        string imgTemp;
        public string ImgTemp
        {

            get
            {
                return imgTemp;
            }
            set
            {
                
                imgTemp = value;
                string test = imgTemp.Remove(0,8);
                vectorImg.Add(Image.FromFile(test));
                NotifyPropertyChanged("imgTemp");
            }


        }
        private ICommand AddRommComand;
        public ICommand AddRoom
        {
            get
            {
                if (AddRommComand == null)
                {
                    AddRommComand = new RelayCommands(AddRoomFunction);
                }
                return AddRommComand;
            }
        }

        public void AddRoomFunction(object param)
        {
            RoomTypeModel roomTypeModel = new RoomTypeModel();
            roomTypeModel.Capacity = Int32.Parse(CapacityTextBox);
            roomTypeModel.NumberOfRooms = Int32.Parse(RoomNumberTextBox);
            roomTypeModel.Description = DescriptionTextBox;
            roomTypeModel.RoomTitle = SelectedType;
            roomTypeModel.Price = Decimal.Parse(PriceTextBox);
            ObservableCollection<FeatureModel> Features = new ObservableCollection<FeatureModel>();
            if (!gratar)
                Features.Add(new FeatureModel{ Name = "Gratar" });
            if (!foisor)
                Features.Add(new FeatureModel { Name = "Foisor" });
            if (!balcon)
                Features.Add(new FeatureModel { Name = "Balcon" });
            if (!masinaDeSpalat)
                Features.Add(new FeatureModel { Name = "MasinaDeSpalat" });
            if (!Bucatarie)
                Features.Add(new FeatureModel { Name = "Bucatarie" });
           
            roomTypeModel.Features = Features;
            ObservableCollection<ImageModel> ImageModels = new ObservableCollection<ImageModel>();
            foreach (var index in vectorImg)
            {
                ImageModels.Add(new ImageModel { Data=Test.converterDemo((Image)index)});
            }
            roomTypeModel.Images = ImageModels;


            using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
            register.AddRoomType(roomTypeModel);
        }

        public int ImageNumber
        {
            get
            { 
                return vectorImg.Count;
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
