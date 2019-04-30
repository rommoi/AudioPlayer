using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public class Artist
    {
        public Artist()
        {
            Name = "unknown";
            Nickname = "unknown";
            Coutry = "unknown";
        }
        public Artist(string name, string nickname, string country)
        {
            Name = name;
            Nickname = nickname;
            Coutry = country;
        }
        public string Name { get; private set; }
        public string Nickname { get; private set; }
        public string Coutry { get; private set; }

        //public Artist GetArtistCopy {
        //    get
        //    {
        //        return (Artist)this.MemberwiseClone();
        //    }
        //}
    }
}
