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

        public int Duration { get; private set; }

        public string Title { get; private set; }

        public string Path { get; private set; }

        public string Lirics { get; private set; }

        public string Genre { get; private set; }

        public Artist Artist { get; private set; }

        public Album Album { get; private set; }

    }
}
