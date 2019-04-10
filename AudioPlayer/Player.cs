using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Player
    {

        const int maxVolume = 300;

        private int _volume;

        public int Volume
        {
            get { return _volume; }
            set
            {
                if(value < 0)
                {
                    _volume = 0;
                }else if(value > maxVolume)
                {
                    _volume = maxVolume;
                }
                else
                {
                    _volume = value;
                }
                Console.WriteLine("\nVolume changed : {0}", _volume);
            }
        }


        public void VolumeUp()
        {
            Volume += 1;
            Console.WriteLine("\nVolume increased");
        }
        public void VolumeDown()
        {
            Volume -= 1;
            Console.WriteLine("\nVolume decreased");
        }
        public void VolumeChange(int value)
        {
            Volume = value;
            //Console.WriteLine("\nVolume changed : {0}", Volume);
        }



        

        private bool _locked = false;

        //public bool Locked
        //{
        //    get { return _locked; }
        //    set { _locked = value; }
        //}

        public void Lock()
        {
            _locked = true;
            Console.WriteLine("\nPlayer locked...");
        }
        public void UnLock()
        {
            _locked = false;
            Console.WriteLine("\nPlayer unlocked...");
        }


        private bool _playing = false;

        public bool Playing { get { return _playing; } }

        public bool Start()
        {
            if (!_locked)
            {
                if (currentSong != null)
                {


                    _playing = true;
                    Console.WriteLine("\nPlayer started.");

                    Console.WriteLine($"{currentSong.Title}, \t{currentSong.Artist.Name}, \t{currentSong.Album.Name}.");
                    /*foreach (var item in songList)
                    {
                        Console.WriteLine($"{item.Artist.Name}, \t{item.Album.Name}, \t{item.Title}");
                        Thread.Sleep(1000);
                    }*/
                }
            }
            return _playing;

        }
        public bool Stop()
        {
            if (!_locked)
            {
                _playing = false;
                Console.WriteLine("\nPlayer stopped.");
            }
            return _playing;
        }

        private List<Song> songList = new List<Song>();

        public void AddSong(Song song)
        {
            if (song != null)
            {
                songList.Add(song);
            }
        }

        private Song currentSong;

        public void SetSong()
        {
            Console.WriteLine("\nSongs:");
            int i = 1;
            foreach (var item in songList)
            {
                Console.WriteLine("\n" + i + " : " + item.Title);
                i++;
            }
            UInt32 selected = 0;
            Console.WriteLine("\nSelect song number:");
            UInt32.TryParse(Console.ReadLine(), out selected);
            if(selected != 0 && (selected-1) < songList.Count)
            {
                currentSong = songList[(int)(--selected)];
            }
        }
        //public Song[] Songs;

        ////public int Volume;

        //private bool Locked;
        //private int volume = 0;
        //private bool IsPlaying = false;
        //private const int _maxVolume = 100;




        //private int _volume;
        //public int Volume
        //{
        //    get
        //    {
        //        return _volume;
        //    }
        //    set
        //    {
        //        if(value > _maxVolume)
        //        {
        //            _volume = _maxVolume;
        //        }else if(value < 0)
        //        {
        //            _volume = 0;
        //        }
        //        else
        //        {
        //            _volume = value;
        //        }
        //    }

        //}

        //public void VolumeUp()
        //{
        //    if ((Volume + 5) > 100)
        //    {
        //        Volume = 100;
        //    }
        //    else
        //    {
        //        Volume += 5;
        //    }
        //    Console.WriteLine("Volume " + Volume);
        //}
        //public void VolumeDown()
        //{
        //    if((Volume-5) < 0)
        //    {
        //        Volume = 0;
        //    }
        //    else
        //    {
        //        Volume -= 5;
        //    }
        //    Console.WriteLine("Volume " + Volume);
        //}

        //public void VolumeChange(int step)
        //{

        //}

        //public bool Stop()
        //{
        //    IsPlaying = false;
        //    Console.WriteLine("Player stopped");
        //    return IsPlaying;
        //}

        //public bool Start()
        //{
        //    IsPlaying = true;
        //    Console.WriteLine("Player started");
        //    return IsPlaying;
        //}

        //public void Play()
        //{
        //    for (int i = 0; i < Songs.Length; i++)
        //    {
        //        Console.WriteLine("\n {0}", Songs[i].Title);
        //        Thread.Sleep(2500);
        //    }
        //}

        //public void LoadPlaylist()
        //{

        //}

        //public void SavePlaylist()
        //{

        //}

        //public void Lock()
        //{
        //    Locked = true;
        //    Console.WriteLine("\nPlayer is locked");
        //}
        //public void UnLock()
        //{
        //    Locked = false;
        //    Console.WriteLine("\nPlayer unlocked");
        //}
    }
}
