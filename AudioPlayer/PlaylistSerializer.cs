using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AudioPlayer
{
    public static class PlaylistSerializer
    {
        public static void BinSerialize<T>(this IEnumerable<T> items, string name)
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream fs = new FileStream($"{name}.pplbin", FileMode.OpenOrCreate))
            {
                foreach (var item in items)
                {
                    bf.Serialize(fs, item);
                }

            }
        }
        

        public static void XmlSerialize<T>(this IEnumerable<T> items, string name)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));

            using(FileStream fs = new FileStream($"{name}.pplxml", FileMode.OpenOrCreate))
            {
                xmlSer.Serialize(fs, items);
            }
        }
        
    }
}
