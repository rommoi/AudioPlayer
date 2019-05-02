using AudioPlayer.OutputSkins;
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
        Skin _skin;

        //private int _volume;
        public Player(Skin skin)
        {
            _wmp = new WMPLib.WindowsMediaPlayer();
            _wmp.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(_wmp_PlayStateChange);
            _skin = skin;

            //_skin.Clear();
        }

        
        public void LabelOutput(string label)
        {
            _skin.Render(label);
        }

        public int Volume
        {
            get { return _wmp.settings.volume; }
            set
            {
                _skin.Render("\ninput value :");
                //Console.WriteLine("\ninput value :");
                string val = Console.ReadLine();
                int volumeValue = 0;
                if (int.TryParse(val, out volumeValue))
                {
                    _wmp.settings.volume = value;
                    //Console.WriteLine("\nVolume changed : {0}", _wmp.settings.volume);
                    _skin.Render($"\nVolume changed : {_wmp.settings.volume}");
                }
                
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
            _skin.Render("\nPlayer locked...");
            //Console.WriteLine("\nPlayer locked...");
        }
        public void UnLock()
        {
            _locked = false;
            _skin.Render("\nPlayer unlocked...");
            //Console.WriteLine("\nPlayer unlocked...");
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
                    _skin.Render("\nPlayer started.");
                    //Console.WriteLine("\nPlayer started.");

                    _wmp.URL = _currentSong.Path;

                    //Console.WriteLine($"Name : {_wmp.currentMedia.name}. Duration : {_wmp.currentMedia.durationString}");
                    GetSongData(_currentSong);


                    try
                    {

                        _wmp.controls.play();
                        
                    }
                    catch(Exception ex)
                    {
                        _skin.Render(ex.Message);
                        //Console.WriteLine(ex.Message);
                        _songList.Remove(_currentSong);
                    }
                  
                }
                else
                {
                    _skin.Render("Please choose a song.");
                    //Console.WriteLine("Please choose a song.");
                }
            }
            else
            {
                _skin.Render("Player locked... Unlock it to play song.");
                //Console.WriteLine("Player locked... Unlock it to play song.");
            }
            return _playing;

        }
        private void GetSongData(Song s)
        {
             var (title, genre, _, duration, _) = s;
            _skin.Render($"{title.CutString(30)} \t {genre} \t {duration}");
            //Console.WriteLine($"{title.CutString(30)} \t {genre} \t {duration}");
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
                _skin.Render("\nPlayer stopped.");
                //Console.WriteLine("\nPlayer stopped.");
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
            _skin.Render("\nSongs:");
            //Console.WriteLine("\nSongs:");
            
            SongListShow();

            UInt32 selected = 0;
            _skin.Render("\nSelect song number:");
            //Console.WriteLine("\nSelect song number:");
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
            _skin.Render("Input Genre you want..");
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

            
        }
        public void SongListShow()
        {
            int i = 1;
            _skin.Render(new string('|', 100));
            //Console.WriteLine(new string('|', 100));
            foreach (var item in _songList)
            {
                if (item.Like == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (item.Like == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                //Console.WriteLine("{0} : {1, 10}   {2,-25}", i, item.Genre, item.Title.CutString(20));
                
                _skin.Render(String.Format("{0} : {1, 10}  {2, -10}  {3,-25}", i, item.Genre, item.Like == true ? "Like!" : (item.Like == false ? "Dislike" : "neutral"), item.Title.CutString(20)));
                //Console.ForegroundColor = ConsoleColor.White;
                i++;
            }
            _skin.Render(new string('|', 50));
            //Console.WriteLine(new string('|', 50));
        }
        public void SetLike()
        {
            _skin.Render("Input songs number..");
            //Console.WriteLine("Input songs number..");
            int num;
            int.TryParse(Console.ReadLine().Trim(), out num);
            

            if(--num > 0 && num < _songList.Count)
            {
                _skin.Render("Input like, dislike or neutral..");
                //Console.WriteLine("Input like, dislike or neutral..");
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
        public void SetSkin()
        {
            
            
                _skin.Render("input skin name:");

                _skin = SkinFactory.CreateSkin(Console.ReadLine().ToLower().Trim());
                
            
        }

    }
}
