using System;
using System.Collections.Generic;
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


            Song s1 = new Song(360000, "Another Brick in the wall", @"C:\", "lalala", "rock",
                new Artist(
                    "Pink Floyd",
                    String.Empty,
                    "England"
                    ), 
                new Album(
                    "Pink Floyd Album #1",
                    1982,
                    @"C:\"
                    )
                );
            Song s2 = new Song(360000, "Bohemian Rhapsody", @"C:\", "lululu", "rock",
                new Artist(
                    "Queen",
                    String.Empty,
                    "England"
                    ), 
                new Album(
                    "Bohemian Rhapsody Album",
                    1986,
                    @"C:\"
                    )
                );

            Player player = new Player();

            player.AddSong(s1);
            player.AddSong(s2);

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
                    case "set":
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
                    default:
                        Console.WriteLine("Unknown command...");
                        break;
                }


                
            }



                Console.ReadLine();
        }
    }
    
}
