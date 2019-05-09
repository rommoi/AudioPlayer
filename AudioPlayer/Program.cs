﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
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

            //List<string> files = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.TopDirectoryOnly).Where(s => new string[] { ".mp3", ".wav" }.Contains(Path.GetExtension(s))).ToList();
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
            //Random rnd = new Random();
            //var arr = new bool?[3] { false, true, null};

            //List<Song> songsList = new List<Song>();
            //foreach (var item in files)
            //{
            //    string[] parts = item.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                
            //    Song s = new Song(arr[rnd.Next(3)], new Artist(), new Album(), title:parts[parts.Length - 1], path: parts[parts.Length - 1], genre:(GenreType)rnd.Next(5));
            //    songsList.Add(s);
            //}
            //player.AddSong(songsList);  //using overloaded method
            

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
                        return;
                        
                    case "set like":
                        player.SetLike();
                        break;
                    case "set skin":
                        player.SetSkin(); 
                        break;
                    default:
                        player.LabelOutput("Unknown command...");
                        
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
