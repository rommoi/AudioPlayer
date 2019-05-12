using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioPlayer.OutputSkins;




namespace AudioPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            


            //Player player = new Player(new ColorOutputSkin("redd"));
            PlayerBase<AudioItem> player = new Player(SkinFactory.CreateSkin("", ""));
            //Player player = new Player(new MyEyes_____());



            try
            {
                while (true)
                {
                    player.LabelOutput("Next action:");
                    //Console.WriteLine("Next action :");
                    string cmd = Console.ReadLine();
                    switch (cmd)
                    {

                        case "set volume":
                            player.Volume = player.Volume;
                            break;
                        case "play":
                            player.Play();
                            //player.Playing = true; //error. Property or indexer 'Player.Plaing' cannot be assigned to -- it is read only
                            break;
                        case "stop":
                            player.Stop();
                            break;
                        case "lock":
                            player.LockUnLock = true;
                            break;
                        case "unlock":
                            player.LockUnLock = false;
                            break;
                        case "set song":
                            player.SetPlayingItem();
                            break;
                        case "pause":
                            player.Pause();
                            break;
                        case "show":
                            player.ShowPlaylist();
                            break;
                        case "sort":
                            player.SortPlayList_ByTitle();
                            break;
                        case "genre filter":
                            //Console.WriteLine("Input Genre you want..");
                            //player.SongListGenreSort(Console.ReadLine().Trim().ToUpper());
                            (player as Player).Filter_ByGenre();
                            break;
                        case "shuffle":
                            player.ShufflePlayList();
                            break;
                        case "quit":
                            player.ClosePlayer();

                            Environment.Exit(0);
                            //return;
                            break;

                        case "set like":
                            player.SetLike();
                            break;
                        case "set skin":
                            player.SetSkin();
                            break;

                        case "load":
                            Console.WriteLine("Enter path:");
                            player.Load(Console.ReadLine());
                            break;
                        case "clear":
                            player.Clear();
                            break;
                        case "save playlist":
                            Console.WriteLine("Enter playlist name:");
                            player.SavePlaylist(Console.ReadLine());
                            break;
                        case "load playlist":
                            Console.WriteLine("Enter path:");
                            player.LoadPlaylist(Console.ReadLine());
                            break;

                        default:
                            player.LabelOutput("Unknown command...");

                            break;
                    }



                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.ReadLine();
            }
            Console.WriteLine();


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
