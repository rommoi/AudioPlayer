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


            

            //int min, max, total = 0;
            //Song[] sl = CreateSongs(out min, out max, ref total);

            //Console.WriteLine($"{min}, \t {max}, \t {total}");

            Player player = new Player();

            List<string> files = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.TopDirectoryOnly).Where(s => new string[] { ".mp3", ".wav" }.Contains(Path.GetExtension(s))).ToList();
            foreach (var item in files)
            {
                //try to use ID3 for extract info about songs
                string[] parts = item.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                player.AddSong(new Song(0, parts[parts.Length - 1], item, String.Empty, String.Empty,
                new Artist(),
                new Album(
                    "---",
                    default(DateTime),
                    "---"
                    )
                ));
            }

            //Song[] s = CreateSongs();

            //foreach (var item in s)
            //{
            //    player.AddSong(item);
            //}

            //Console.WriteLine("Artist, empty constructor:");
            //Artist art1 = new Artist();                             //B5-Player5/10.Constructors
            //Console.WriteLine("Artist constructor with parameters:");
            //Artist art2 = new Artist("John", "Jack", "England");    //B5-Player5/10.Constructors
            
            //player.AddSong(s1);
            //player.AddSong(s2);
            //player.AddSong(s3);

            //var player = new Player();

            //player.Songs = new[] { song1, song2 };

            ////player.Play();

            while (true)
            {
                Console.WriteLine("Next action :");
                string cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "up":
                        player.VolumeUp();
                        break;
                    case "down":
                        player.VolumeDown();
                        break;
                    case "set volume":
                        Console.WriteLine("\ninput value :");
                        string val = Console.ReadLine();
                        int volumeValue = 0;
                        if (int.TryParse(val, out volumeValue))
                        {
                            player.VolumeChange(volumeValue);
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
                    case "quit":
                        player.Stop();

                        Environment.Exit(0);
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
