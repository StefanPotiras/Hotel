using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Imaging;

namespace Hotel.Models
{
    public class Images
    {
        public Images() { }
        public ObservableCollection<BitmapImage> images { get; set; }    
    }
}
