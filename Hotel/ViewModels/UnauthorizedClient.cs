using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Hotel.Helps;
using Hotel.Models;


namespace Hotel.ViewModels
{
    public class UnauthorizedClient : NotifyViewModel
    {

        ObservableCollection<Hotels> hotelsCurrent = new ObservableCollection<Hotels>();
        public UnauthorizedClient()
        { }
        public UnauthorizedClient(bool isEmployee)
        {
            Hotels temp1 = new Hotels();
            temp1.tipCamera = "Camera cu 2 paturi";
            temp1.pret = "100$";
            temp1.numarPersoane = "2";
            temp1.image = "C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img1.jpg";
            Hotels temp2 = new Hotels();
            temp2.tipCamera = "Camera cu 5 paturi";
            temp2.pret = "150$";
            temp2.numarPersoane = "10";
            temp2.image = "C:\\Users\\StefanPotiras\\Desktop\\ImageTest\\img2.jpg";
            hotelsCurrent.Add(temp1);
            hotelsCurrent.Add(temp2);
            visibility = isEmployee;
        }

        public ObservableCollection<Hotels> HotelsCurrent
        {
            get
            {
                return hotelsCurrent;
            }
        }
        private DateTime selectedStartDate;
        public DateTime StartDate
        {
            get
            {
                return selectedStartDate;
            }
            set
            {

                selectedStartDate = value;
                NotifyPropertyChanged("StartDate");
            }
        }
        private DateTime selectEndDate;
        public DateTime EndDate
        {
            get
            {
                return selectEndDate;
            }
            set
            {
                selectEndDate = value;
                NotifyPropertyChanged("EndDate");
            }
        }

        private bool visibility;
        public bool Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                NotifyPropertyChanged("Visibility");
            }
        }
    }
}
