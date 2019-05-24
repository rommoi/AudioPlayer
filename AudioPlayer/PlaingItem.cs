using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    [Serializable]
    public class PlaingItem
    {

        public PlaingItem(double duration, string title, string path, bool? like)
        {
            Duration = duration;
            DurationMS = String.Format("{0}:{1}", (int)(duration / 60), Math.Round(duration % 60));
            Title = title;
            Path = path;
            Like = like;
        }


        public double Duration { get; private set; }

        public string DurationMS { get; private set; }

        public string Title { get; private set; }

        public string Path { get; private set; }

        public bool? Like { get; set; }

        public void Deconstruct(out double duration, out string durationMS, out string title, out string path, out bool? like)
        {
            duration = Duration;
            durationMS = DurationMS;
            title = Title;
            path = Path;
            like = Like;
        }
    }

    [Serializable]
    public class AudioItem : PlaingItem
    {

        public enum AudioGenre
        {
            UNKNOWN = 0,
            ROCK,
            POP,
            JAZZ,
            CLASSIC

        }

        public AudioGenre Genre { get; private set; }

        public Artist Artist { get; set; }

        public Album Album { get; set; }

        public AudioItem(double duration, string title, string path, bool? like, Artist artist, Album album, AudioGenre genre) : base(duration, title, path, like)
        {
            Artist = artist;
            Album = album;
            Genre = genre;
        }


    }



    public class VideoItem : PlaingItem
    {

        public enum VideoGenre
        {
            UNKNOWN = 0,
            HORROR,
            DRAMA,
            ACTION,
            COMEDY,
            FANTASTIC
        }

        public VideoGenre Genre { get; private set; }

        public string Name { get; set; }

        public DateTime Year { get; set; }

        public VideoItem(double duration, string title, string path, bool? like, string name, DateTime year, VideoGenre genre) : base(duration, title, path, like)
        {
            Year = year;
            Name = name;
            Genre = genre;
        }
    }
}
