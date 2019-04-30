using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMPLib;

namespace AudioPlayer
{
    class Player
    {

        //const int _maxVolume = 300;

        WMPLib.WindowsMediaPlayer _wmp;

        //private int _volume;
        public Player()
        {
            _wmp = new WMPLib.WindowsMediaPlayer();
            _wmp.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(_wmp_PlayStateChange);
        }

        public int Volume
        {
            get { return _wmp.settings.volume; }
            set
            {
                _wmp.settings.volume = value;
                Console.WriteLine("\nVolume changed : {0}", _wmp.settings.volume);
            }
        }

        private bool _paused = false;
        double _time = 0.0;
        public void Pause()
        {
            if (_paused)
            {
                _paused = false;
                _wmp.controls.currentPosition = _time;
                _wmp.controls.play();
            }
            else
            {
                _paused = true;
                _time = _wmp.controls.currentPosition;
                _wmp.controls.pause();
            }
            
        }
        
        

        //public void VolumeUp()
        //{
        //    Volume += 1;
        //    Console.WriteLine("\nVolume increased");
        //}
        //public void VolumeDown()
        //{
        //    Volume -= 1;
        //    Console.WriteLine("\nVolume decreased");
        //}
        //public void VolumeChange(int value)
        //{
        //    Volume = value;
        //    //Console.WriteLine("\nVolume changed : {0}", Volume);
        //}



        

        private bool _locked = false;

        public bool IsLocked
        {
            get { return _locked; }
            
        }

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
                if (_currentSong != null)
                {
                    if (Playing)
                    {
                        Stop();
                    }

                    _playing = true;
                    Console.WriteLine("\nPlayer started.");

                    _wmp.URL = _currentSong.Path;

                    //Console.WriteLine($"Name : {_wmp.currentMedia.name}. Duration : {_wmp.currentMedia.durationString}");
                    GetSongData(_currentSong);


                    try
                    {

                        _wmp.controls.play();
                        
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _songList.Remove(_currentSong);
                    }
                  
                }
                else
                {
                    Console.WriteLine("Please choose a song.");
                }
            }
            else
            {
                Console.WriteLine("Player locked... Unlock it to play song.");
            }
            return _playing;

        }
        private void GetSongData(Song s)
        {
             var (title, genre, _, duration, _) = s;
            Console.WriteLine($"{title.CutString(30)} \t {genre} \t {duration}");
            //return ();
        }

        private void _wmp_PlayStateChange(int NewState)
        {
            //Console.WriteLine("-------------" + NewState + " \t" + (WMPPlayState)NewState);
            //Console.WriteLine("-------------" + _wmp.playState);
            switch ((WMPPlayState)NewState)
            {
                case WMPPlayState.wmppsPlaying:

                    _currentSong.DurationMinSec = _wmp.currentMedia.durationString;
                    _currentSong.Duration = _wmp.currentMedia.duration;
                    //Console.WriteLine($"{_currentSong.Title}, \t{_currentSong.Artist.Name}, \t{_currentSong.Album.Name}.");

                    //Console.WriteLine($"Name : {_wmp.currentMedia.name}. Duration : {_wmp.currentMedia.durationString}");
                    break;
                case WMPPlayState.wmppsStopped:
                    //_wmp.PlayStateChange -= _wmp_PlayStateChange;
                    break;
            }
        }

        
        public void Stop()
        {
            if (!_locked)
            {
                _playing = false;
                
                _wmp.controls.stop();
                _wmp.PlayStateChange -= _wmp_PlayStateChange;
                Console.WriteLine("\nPlayer stopped.");
            }

            //_wmp.PlayStateChange -= playerChenged;

            //return _playing;

            
        }

        private List<Song> _songList = new List<Song>();

        public void AddSong(Song song)  //methos get 1 song and add 1 song
        {
            if (song != null)
            {
                _songList.Add(song);
            }
        }

        public void AddSong(List<Song> songList) //overloading, get List<> and add it containment
        {
            foreach (var item in songList)
            {
                this._songList.Add(item);
            }
        }
        public void AddSong(Song[] songList)  //overloading, get array and add songs
        {
            foreach (var item in songList)
            {
                this._songList.Add(item);
            }
        }
        //public void AddSong(params Song[] songList) // the same as previous
        //{
        //    foreach (var item in songList)
        //    {
        //        this.songList.Add(item);
        //    }
        //}

        private Song _currentSong;

        public void SetSong()
        {
            Console.WriteLine("\nSongs:");
            //int i = 1;
            //foreach (var item in _songList)
            //{
            //    Console.WriteLine("\n" + i + " : " + item.Title + " " + "\t" + item.Artist.Name);

            //    i++;
            //}
            SongListShow();

            UInt32 selected = 0;
            Console.WriteLine("\nSelect song number:");
            UInt32.TryParse(Console.ReadLine(), out selected);
            if(selected != 0 && (selected-1) < _songList.Count)
            {
                selected--;
                _currentSong = _songList[(int)(selected)];
                
            }
        }

        public void SongListShuffle()
        {
            _songList.ShuffleSongs();
            //Random rnd = new Random();
            //for (int i = 0; i < 100; i++)
            //{
            //    int index1 = rnd.Next(_songList.Count);
            //    int index2 = rnd.Next(_songList.Count);

            //    Song tempVar = _songList[index1];
            //    _songList[index1] = _songList[index2];
            //    _songList[index2] = tempVar;

            //}
        }
        public void SongListSort()
        {
            _songList.SongSorter();
        }
        //public void SongListSort() //sort only by first title char
        //{
        //    bool _isSorted = false;
        //    while (!_isSorted)
        //    {
        //        Song tempVar = null;
        //        for (int i = 0; i < _songList.Count - 1; i++)
        //        {
                    
        //            if ( char.ToLower( _songList[i].Title[0]) >= char.ToLower(_songList[i+1].Title[0]))
        //            {
                       
        //                tempVar = _songList[i];
        //                _songList[i] = _songList[i + 1];
        //                _songList[i + 1] = tempVar;
        //            }
        //            _isSorted = true;
        //            for (int j = 0; j < _songList.Count - 1; j++)
        //            {
        //                if ( char.ToLower(_songList[j].Title[0]) > char.ToLower(_songList[j + 1].Title[0])) _isSorted = false;
        //            }
        //        }
        //    }
        //}
        public void SongListGenreSort(string genre)
        {
            string g = genre;
            
            GenreType gt = (GenreType)Enum.Parse(typeof(GenreType), genre);
            List<Song> filteredSongList = new List<Song>();
            foreach (var item in _songList)
            {
                if(item.Genre == gt)
                {
                    filteredSongList.Add(item);
                }
            }
            _songList = filteredSongList;

            //bool _isSorted = false;
            //while (!_isSorted)
            //{
            //    Song tempVar = null;
            //    for (int i = 0; i < _songList.Count - 1; i++)
            //    {

            //        if (_songList[i].Genre >= _songList[i + 1].Genre)
            //        {

            //            tempVar = _songList[i];
            //            _songList[i] = _songList[i + 1];
            //            _songList[i + 1] = tempVar;
            //        }
            //        _isSorted = true;
            //        for (int j = 0; j < _songList.Count - 1; j++)
            //        {
            //            if (_songList[j].Genre > _songList[j + 1].Genre) _isSorted = false;
            //        }
            //    }
            //}
        }
        public void SongListShow()
        {
            int i = 1;
            Console.WriteLine(new string('|', 100));
            foreach (var item in _songList)
            {
                if(item.Like == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }else if(item.Like == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                
                Console.WriteLine(i + " : "+ item.Genre + " \t\t" + item.Title.CutString(20));
                Console.ForegroundColor = ConsoleColor.White;
                i++;
            }
            Console.WriteLine(new string('|', 50));
        }
        public void SetLike()
        {
            Console.WriteLine("Input songs number..");
            int num;
            int.TryParse(Console.ReadLine().Trim(), out num);
            

            if(--num > 0 && num < _songList.Count)
            {
                Console.WriteLine("Input like, dislike or neutral..");
                var state = Console.ReadLine().Trim();

                if (String.Compare(state, "like") == 0)
                {
                    _songList[num].Like = true;
                }else if(String.Compare(state, "dislike") == 0)
                {
                    _songList[num].Like = false;
                }
                else if(String.Compare(state, "neutral") == 0)
                {
                    _songList[num].Like = null;
                }
            }
        }
        
    }
}
