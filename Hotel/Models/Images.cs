using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Imaging;

namespace Hotel.Models
{
    class Images
    {
        public ObservableCollection<BitmapImage> images { get; set; }
        public ObservableCollection<int>imagesBinary { get; set; }

    }
}
