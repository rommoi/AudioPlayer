using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Band
    {
        public Band(string title, string genre, int year, bool isexist)
        {

        }

        public string Title { get; private set; }

        public string Genre { get; private set; }

        public int Year { get; private set; }

        public bool IsExist { get; private set; }
    }
}
