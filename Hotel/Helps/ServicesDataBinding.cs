using ModelsClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Helps
{
    public class ServicesDataBinding:ServicesModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool checkBool=false;
        public bool CheckBool
        {
            get
            {
                return checkBool;
            }
            set
            {
                checkBool = value;
                NotifyPropertyChanged("checkBool");
            }
        }
        public int IdR
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
                NotifyPropertyChanged("IdR");
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
                PriceR = value;
                NotifyPropertyChanged("PriceR");
            }
        }

        public string NameR
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
                NotifyPropertyChanged("NameR");
            }
        }
    }
}
