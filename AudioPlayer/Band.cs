using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    [Serializable]
    class Band
    {
        
        public Band(DateTime year, string title = "unknown", string genre = "unknown", bool isexist = false)
        {
            Title = title;
            Genre = genre;
            IsExist = isexist;
            Year = year;
        }
        public Band(string title, string genre, DateTime year, bool isexist)
        {
            Title = title;
            Genre = genre;
            IsExist = isexist;
            Year = year;
        }

        public string Title { get; private set; }

        public string Genre { get; private set; }

        public DateTime Year { get; private set; }

        public bool IsExist { get; private set; }
    }
}
