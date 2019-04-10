using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Album
    {
        public Album(string name, int year, string path)
        {
            Name = name;
            Year = year;
            Path = path;
        }
        public string Name { get; private set; }

        public int Year { get; private set; }

        public string Path { get; private set; }

    }
}
