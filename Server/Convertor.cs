using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Convertor
    {
        public static ICollection<T> EnumToCol<T> (IEnumerable<T> enumerable)
        {
            ICollection<T> col = new List<T>();
            foreach(T item in enumerable)
            {
                col.Add(item);
            }
            return col;
        }

        public static ObservableCollection<T> EnumToObsCol<T>(IEnumerable<T> enumerable)
        {
            ObservableCollection<T> col = new ObservableCollection<T>();
            foreach (T item in enumerable)
            {
                col.Add(item);
            }
            return col;
        }
    }
}
