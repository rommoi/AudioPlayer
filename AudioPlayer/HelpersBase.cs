using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public static class HelpersBase
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection) where T : PlaingItem
        {
            Random rnd = new Random();
            List<T> list = collection.ToList();
            for (int i = 0; i < 100; i++)
            {
                int index1 = rnd.Next(collection.Count());
                int index2 = rnd.Next(collection.Count());
                
                var tempVar = list[index1];
                list[index1] = list[index2];
                list[index2] = tempVar;

            }
            return list;

            //return collection.Shuffle();

        }
        public static IEnumerable<T> SortByTitle<T>(this IEnumerable<T> collection) where T : PlaingItem
        {
            bool _isSorted = false;
            List<T> l = collection.ToList<T>();
            while (!_isSorted)
            {
                T tempVar = null;
                for (int i = 0; i < l.Count - 1; i++)
                {

                    if (char.ToLower(l[i].Title[0]) >= char.ToLower(l[i + 1].Title[0]))
                    {

                        tempVar = l[i];
                        l[i] = l[i + 1];
                        l[i + 1] = tempVar;
                    }
                    _isSorted = true;
                    for (int j = 0; j < l.Count - 1; j++)
                    {
                        if (char.ToLower(l[j].Title[0]) > char.ToLower(l[j + 1].Title[0])) _isSorted = false;
                    }
                }
            }
            return l;
        }
        public static IEnumerable<T> FilterAudio_ByGenre<T>(this IEnumerable<T> collection, AudioItem.AudioGenre genre) where T : AudioItem
        {
            
            return collection.Where(x => x.Genre == genre).Select(x => x).ToList();
        }
        public static IEnumerable<T> FilterVideo_ByGenre<T>(this IEnumerable<T> collection, VideoItem.VideoGenre genre) where T : VideoItem
        {

            return collection.Where(x => x.Genre == genre).Select(x => x).ToList();
        }
        
    } 
    
}
