using Hotel.Helps;
using Hotel.Models;
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
        TypeRoomsModelBinding typeRoomsModelBinding = new TypeRoomsModelBinding();
        public AddNewRoomViewModel() { }
        bool isUpdate;
        public AddNewRoomViewModel(TypeRoomsModelBinding typeRoomsModelBinding, bool isUpdate)
        {
            this.isUpdate = isUpdate;
            if (isUpdate == true)
            {
                this.typeRoomsModelBinding = typeRoomsModelBinding;
                roomNumberTextBox = typeRoomsModelBinding.NumberOfRooms.ToString();
                priceTextBox = typeRoomsModelBinding.Price.ToString();
                descriptionTextBox = typeRoomsModelBinding.Description;
                capacityTextBox = typeRoomsModelBinding.Capacity.ToString();
                selectedType = typeRoomsModelBinding.RoomTitle;
                foreach (var index in typeRoomsModelBinding.Features)
                {
                    if (index.Name == "Gratar")
                        gratar = true;
                    if (index.Name == "Foisor")
                        foisor = true;
                    if (index.Name == "Balcon")
                        balcon = true;
                    if (index.Name == "MasinaDeSpalat")
                        masinaDeSpalat = true;
                    if (index.Name == "Bucatarie")
                        Bucatarie = true;
                }
                ImageNumber = typeRoomsModelBinding.Images.Count;
            }


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
        public ObservableCollection<ObservableCollection<byte>> matrixImages = new ObservableCollection<ObservableCollection<byte>>();
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
                string test = imgTemp.Remove(0, 8);
                vectorImg.Add(Image.FromFile(test));
                ImageNumber++;
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
        private ICommand RemoveLastImg;
        public ICommand RemoveLast
        {
            get
            {
                if (RemoveLastImg == null)
                {
                    RemoveLastImg = new RelayCommands(RemoveLastFc);
                }
                return RemoveLastImg;
            }
        }
        public void RemoveLastFc(object param)
        {
            if (vectorImg.Count > 0)
            {
                vectorImg.RemoveAt(vectorImg.Count - 1);
                ImageNumber--;
            }
            else
            if (typeRoomsModelBinding.Images.Count > 0)
            {
                typeRoomsModelBinding.Images.RemoveAt(vectorImg.Count - 1);
                ImageNumber--;
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
                Features.Add(new FeatureModel { Name = "Gratar" });
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
                ImageModels.Add(new ImageModel { Data = Test.converterDemo((Image)index) });
            }
            roomTypeModel.Images = ImageModels;
            using Request register = new Request(@"Server = localhost\SQLEXPRESS; Database = Hotel; Trusted_Connection = True; ");
            if (isUpdate != true)
            {
                register.AddRoomType(roomTypeModel);
            }
            else
            {
                foreach (var index in typeRoomsModelBinding.Images)
                {
                    ImageModels.Add(new ImageModel { Data = index.Data });
                }
                roomTypeModel.Id = typeRoomsModelBinding.Id;
                if (roomTypeModel.Capacity != typeRoomsModelBinding.Capacity || roomTypeModel.Price != typeRoomsModelBinding.Price ||
                    roomTypeModel.Description != typeRoomsModelBinding.Description || roomTypeModel.NumberOfRooms != typeRoomsModelBinding.NumberOfRooms ||
                    roomTypeModel.RoomTitle != typeRoomsModelBinding.RoomTitle || roomTypeModel.Images.Count != typeRoomsModelBinding.Images.Count ||
                    roomTypeModel.Features.Count != typeRoomsModelBinding.Features.Count)
                {
                    register.UpdateRoomType(roomTypeModel);
                }
            }
            UnauthorizedClientModel firstPage = new UnauthorizedClientModel();
            UnauthorizedClient firstPageModel = new UnauthorizedClient(UserModel.UserType.Admin);
            firstPage.DataContext = firstPageModel;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = firstPage;
            firstPage.Show();
        }
        public int imageNumber = 0;
        public int ImageNumber
        {
            get
            {
                return imageNumber;
            }
            set
            {
                imageNumber = value;
                NotifyPropertyChanged("imageNumber");
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
