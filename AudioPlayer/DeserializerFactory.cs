using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public static class DeserializerFactory
    {
        public static IEnumerable<AudioItem> DeserializeObjects(string filePath)
        {
            switch (Path.GetExtension(filePath))
            {
                case ".pplbin":
                    BinaryFormatter bf = new BinaryFormatter();

                    string[] pth = filePath.Split('\\');
                    List<AudioItem> list = new List<AudioItem>();
                    if (File.Exists(filePath))
                    {
                        if (File.Exists(filePath))
                        {
                            using (FileStream fs = new FileStream(pth[pth.Length - 1], FileMode.OpenOrCreate))
                            {
                                while (fs.Position != fs.Length)
                                {
                                    AudioItem audio = (AudioItem)bf.Deserialize(fs);
                                    list.Add(audio);
                                }
                            }
                        }
                    }
                    return list;
                    
                case ".pplxml":
                    return null;
                    //break;
                default:
                    Console.WriteLine("Unsupported format...");
                    return null;
                    //break;
            }
        }
    }
}
