using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public enum GenreType
    {
        UNKNOWN = 0,
        ROCK,
        POP,
        JAZZ,
        CLASSIC
        
    }

    public class Song
    {
        
        public Song(int duration, string title, string path, string lirics, GenreType genre, Artist artist, Album album)
        {
            Duration = duration;
            Title = title;
            Path = path;
            Lirics = lirics;
            Genre = genre;
            Artist = artist;
            Album = album;
        }
        public Song(bool? like, Artist artist, Album album, int duration = 0, string title = "", string path = "", string lirics = "unknown",GenreType genre = GenreType.UNKNOWN)
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

        public void Deconstruct(out string title, out GenreType genre, out bool? like, out double duration, out string durationMinSec)
        {
            title = Title;
            genre = Genre;
            like = Like;
            duration = Duration;
            durationMinSec = DurationMinSec;
        }

        public double Duration { get; set; }

        public string DurationMinSec { get; set; }

        public string Title { get; set; }

        public string Path { get; set; }

        public string Lirics { get; set; }

        public GenreType Genre { get; set; }

        public Artist Artist { get; set; }

        public Album Album { get; set; }

        public bool? Like { get; set; }

    }
}
