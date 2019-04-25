using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace AudioPlayer
{
    class Program
    {
        static void Main(string[] args)
        {


            

            
            Player player = new Player();

            List<string> files = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.TopDirectoryOnly).Where(s => new string[] { ".mp3", ".wav" }.Contains(Path.GetExtension(s))).ToList();
            //foreach (var item in files)
            //{
            //    //try to use ID3 for extract info about songs
            //    string[] parts = item.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            //    player.AddSong(new Song(0, parts[parts.Length - 1], item, String.Empty, String.Empty,            //using add methos
            //    new Artist(),
            //    new Album(
            //        "---",
            //        default(DateTime),
            //        "---"
            //        )
            //    ));
            //}
            Random rnd = new Random();
            var arr = new bool?[3] { false, true, null};

            List<Song> songsList = new List<Song>();
            foreach (var item in files)
            {
                string[] parts = item.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                
                Song s = new Song(arr[rnd.Next(3)], new Artist(), new Album(), title:parts[parts.Length - 1], path: parts[parts.Length - 1], genre:(GenreType)rnd.Next(5));
                songsList.Add(s);
            }
            player.AddSong(songsList);  //using overloaded method
            

            while (true)
            {
                Console.WriteLine("Next action :");
                string cmd = Console.ReadLine();
                switch (cmd)
                {
                    
                    case "set volume":
                        Console.WriteLine("\ninput value :");
                        string val = Console.ReadLine();
                        int volumeValue = 0;
                        if (int.TryParse(val, out volumeValue))
                        {
                            player.Volume = (volumeValue);
                        }
                        break;
                    case "play":
                        player.Start();
                        //player.Playing = true; //error. Property or indexer 'Player.Plaing' cannot be assigned to -- it is read only
                        break;
                    case "stop":
                        player.Stop();
                        break;
                    case "lock":
                        player.Lock();
                        break;
                    case "unlock":
                        player.UnLock();
                        break;
                    case "set song":
                        player.SetSong();
                        break;
                    case "pause":
                        player.Pause();
                        break;
                    case "show":
                        player.SongListShow();
                        break;
                    case "sort":
                        player.SongListSort();
                        break;
                    case "genresort":
                        Console.WriteLine("Input Genre you want..");
                        player.SongListGenreSort(Console.ReadLine().Trim().ToUpper());
                        break;
                    case "shuffle":
                        player.SongListShuffle();
                        break;
                    case "quit":
                        player.Stop();
                        //player = null;

                        Environment.Exit(0);
                        break;
                    case "setlike":
                        player.SetLike();
                        break;
                    default:
                        Console.WriteLine("Unknown command...");
                        break;
                }


                
            }



                //Console.ReadLine();
        }
        
        //private static Song[] CreateSongs(out int min, out int max, ref int total)
        //{
        //    Song[] songArray = new Song[5];
        //    Random rnd = new Random();
        //    int minDuration = int.MaxValue, maxDuration = int.MinValue, totalDuration = 0;
        //    for (int i = 0; i < songArray.Length; i++)
        //    {
        //        var song1 = new Song(0, "", "", "", "", new Artist(), new Album("", 0, ""));
        //        song1.Title = "10";
        //        song1.Duration = rnd.Next(5000);
        //        song1.Artist = new Artist();

        //        totalDuration += song1.Duration;

        //        minDuration = song1.Duration < minDuration ? song1.Duration : minDuration;
        //        maxDuration = song1.Duration > maxDuration ? song1.Duration : maxDuration;

        //        songArray[i] = song1;

        //        Console.WriteLine($"{song1.Duration}.");
        //    }

        //    min = minDuration;
        //    max = maxDuration;
        //    total = totalDuration;

        //    return songArray;
        //}


    }
    
}
