using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Song
    {
        public Song(int duration, string title, string path, string lirics, string genre, Artist artist, Album album)
        {
            Duration = duration;
            Title = title;
            Path = path;
            Lirics = lirics;
            Genre = genre;
            Artist = artist;
            Album = album;
        }
        public Song(Artist artist, Album album, int duration = 0, string title = "", string path = "", string genre = "unknown")
        {

        }

        public double Duration { get; set; }

        public string DurationMinSec { get; set; }

        public string Title { get; set; }

        public string Path { get; set; }

        public string Lirics { get; set; }

        public string Genre { get; set; }

        public Artist Artist { get; set; }

        public Album Album { get; set; }

    }
}
