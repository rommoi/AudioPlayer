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

            try
            {
                Run();
                //GC.Collect();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                
            }

        }
        static void Run()
        {
            using(PlayerView pv = new PlayerView(new Player()))
            //using (PlayerBase<AudioItem> player = new Player())
            {

                pv.Load(Environment.CurrentDirectory);
                
                while (true)
                {
                    //player.LabelOutput("Next action:");
                    //Console.WriteLine("Next action :");
                    string cmd = Console.ReadLine();
                    switch (cmd)
                    {

                        case "set volume":
                            //player.Volume = player.Volume;
                            break;
                        case "play":
                            pv.Play();
                            //player.Play();
                            //player.Playing = true; //error. Property or indexer 'Player.Plaing' cannot be assigned to -- it is read only
                            break;
                        case "stop":
                            pv.Stop();
                            break;
                        case "lock":
                            //player.LockUnLock = true;
                            break;
                        case "unlock":
                            //player.LockUnLock = false;
                            break;
                        case "set song":
                            //player.SetPlayingItem();
                            break;
                        case "pause":
                            //player.Pause();
                            break;
                        case "show":
                            //player.ShowPlaylist();
                            break;
                        case "sort":
                            //player.SortPlayList_ByTitle();
                            break;
                        case "genre filter":
                            //Console.WriteLine("Input Genre you want..");
                            //player.SongListGenreSort(Console.ReadLine().Trim().ToUpper());
                            //(player as Player).Filter_ByGenre();
                            break;
                        case "shuffle":
                            //player.ShufflePlayList();
                            break;
                        case "quit":
                            //player.ClosePlayer();
                            //player.Dispose();

                            //Environment.Exit(0);
                            return;
                            //break;

                        case "set like":
                            //player.SetLike();
                            break;
                        case "set skin":
                            //player.SetSkin();
                            break;

                        case "load":
                            Console.WriteLine("Enter path:");
                            //player.Load(Console.ReadLine());
                            pv.Load(Console.ReadLine());
                            break;
                        case "clear":
                            //player.Clear();
                            break;
                        case "save playlist":
                            //Console.WriteLine("Enter playlist name:");
                            //player.SavePlaylist(Console.ReadLine());
                            //break;
                        case "load playlist":
                            //Console.WriteLine("Enter path:");
                            //player.LoadPlaylist(Console.ReadLine());
                            //break;

                        default:
                            //player.LabelOutput("Unknown command...");
                            Console.WriteLine("Unknown command...");
                            break;
                    }



                }


            }
            
        }
        
    }






    class PlayerView : IDisposable
    {
        PlayerBase<AudioItem> player;

        public PlayerView(PlayerBase<AudioItem> p)
        {
            player = p;

            player.LockStateChanged += (sender, e) =>
            {
                this.UpdateScreen(player);
                //Console.WriteLine($"Lock state changed event : {e.IsLocked}");
            };
            player.VolumeChanged += (sender, e) =>
            {
                this.UpdateScreen(player);
                //Console.WriteLine($"Volume changed event : {e.Volume}");
            };
            player.CollectionChanged += (sender, e) =>
            {
                this.UpdateScreen(player);
                //Console.WriteLine($"Colection changed event");
            };
            player.PlayerPlayingStateChanged += (sender, e) =>
            {
                this.UpdateScreen(player);
                //Console.WriteLine($"Started/Stopped event : {e.IsPlaying}");
            };
            player.PlaingItemStarted += (sender, e) =>
            {
                this.UpdateScreen(player);
                //Console.WriteLine($"Started song event : {e.ItemTitle} {e.ItemDuration} {e.ItemDurationMinSec}");
            };

            //player.Load(Environment.CurrentDirectory);
        }

        public void UpdateScreen(PlayerBase<AudioItem> player)
        {
            Console.Clear();

            int i = 0;
            foreach (var item in player.GetPlaingItems())
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                if (player.GetSelectedItemIndex() == i)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                
                 Console.WriteLine(String.Format("{0,2} : {1, 10}  {2, -10}  {3,-25}", ++i, item.Genre, item.Like == true ? "Like!" : (item.Like == false ? "Dislike" : "neutral"), item.Title.CutString(20)));
                
            }
            Console.WriteLine();

            //status string
            Console.WriteLine($"Player Status: lock : {ConvertBoolToString.Convert( player.LockUnLock)}; volume : {player.Volume} ");
            
            Console.WriteLine($"Current Song: {player.GetCurrentPlaingItem<AudioItem>().Item1}  {player.GetCurrentPlaingItem<AudioItem>().Item2}  {player.GetCurrentPlaingItem<AudioItem>().Item3}");
            Console.WriteLine(  );

            Console.WriteLine("Active commands");
            Console.WriteLine("Play (play)");
            Console.WriteLine("Load (load)");
            Console.WriteLine("Stop (stop)");
        }
        public async void Play()
        {
            await player.Play();
        }

        public async void Load(string path)
        {
            if (Directory.Exists(path))
            {
                await player.Load(path);
                
            }
        }

        public void Stop()
        {
            player.Stop();
        }

        public void Dispose()
        {
            player.Dispose();
        }
    }

    static class ConvertBoolToString
    {
        public static string Convert(bool value)
        {
            if (value) return "Locked";
            return "UnLocked";
        }
    }

}
    

