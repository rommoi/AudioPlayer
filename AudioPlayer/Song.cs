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
        public Song(bool? like, Artist artist, Album album, int duration = 0, string title = "", string path = "", string lirics = "unknown",string genre = "unknown")
        {
            Duration = duration;
            Title = title;
            Path = path;
            Lirics = lirics;
            Genre = genre;
            Artist = artist;
            Album = album;
            Like = like;
        }

        public double Duration { get; set; }

        public string DurationMinSec { get; set; }

        public string Title { get; set; }

        public string Path { get; set; }

        public string Lirics { get; set; }

        public string Genre { get; set; }

        public Artist Artist { get; set; }

        public Album Album { get; set; }

        public bool? Like { get; set; }

    }
}
