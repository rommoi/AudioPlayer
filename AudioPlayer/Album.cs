using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Album
    {
        public Album()
        {
            Name = "unknown";
            Year = DateTime.MinValue;
            Path = "";
        }
        public Album(string name, DateTime year, string path)
        {
            Name = name;
            Year = year;
            Path = path;
        }
        public string Name { get; private set; }

        public DateTime Year { get; private set; }

        public string Path { get; private set; }

    }
}
