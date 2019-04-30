using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public static class Helpers
    {
        public static string CutString(this string str, uint lenght)
        {

            if (str.Length > lenght)
            {
                return str.Substring(0, (int)lenght) + "...";
            }
            return str;
        }
    }
    public static class SongLSortExtension
    {
        public static void SongSorter(this List<Song> list)
        {
            bool _isSorted = false;
            while (!_isSorted)
            {
                Song tempVar = null;
                for (int i = 0; i < list.Count - 1; i++)
                {

                    if (char.ToLower(list[i].Title[0]) >= char.ToLower(list[i + 1].Title[0]))
                    {

                        tempVar = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = tempVar;
                    }
                    _isSorted = true;
                    for (int j = 0; j < list.Count - 1; j++)
                    {
                        if (char.ToLower(list[j].Title[0]) > char.ToLower(list[j + 1].Title[0])) _isSorted = false;
                    }
                }
            }

        }
    }
    public static class ShuffleSongList
    {
        public static void ShuffleSongs(this List<Song> list)
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int index1 = rnd.Next(list.Count);
                int index2 = rnd.Next(list.Count);

                Song tempVar = list[index1];
                list[index1] = list[index2];
                list[index2] = tempVar;

            }
        }
    }
    public static class SongDeconstructor
    {
        public static void Deconstruct(this Song s, out string title, out GenreType genre, out bool? like, out double duration, out string durationMinSec)
        {
            title = s.Title;
            genre = s.Genre;
            like = s.Like;
            duration = s.Duration;
            durationMinSec = s.DurationMinSec;
        }
    }
}
