using AudioPlayer.OutputSkins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMPLib;

namespace AudioPlayer
{
    class Player : PlayerBase<AudioItem>
    {

        
        WMPLib.WindowsMediaPlayer _wmp;
        
        
        public Player(Skin skin) : base(skin)
        {
            _wmp = new WMPLib.WindowsMediaPlayer();
            _wmp.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(_wmp_PlayStateChange);
           
            

            
            List<string> files = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.TopDirectoryOnly).Where(s => new string[] { ".mp3", ".wav" }.Contains(Path.GetExtension(s))).ToList();

            Random rnd = new Random();
            var arr = new bool?[3] { false, true, null };

            List<AudioItem> songsList = new List<AudioItem>();
            foreach (var item in files)
            {
                string[] parts = item.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                AudioItem ai = new AudioItem(0, parts[parts.Length - 1], parts[parts.Length - 1], arr[rnd.Next(3)], new Artist(), new Album(), (AudioItem.AudioGenre)rnd.Next(5));
                //Song s = new Song(arr[rnd.Next(3)], new Artist(), new Album(), title: parts[parts.Length - 1], path: parts[parts.Length - 1], genre: (GenreType)rnd.Next(5));
                songsList.Add(ai);
            }
            AddItem(songsList);  //using overloaded method

            //_wmp.newPlaylist("playlist1", "");
            //var l = Directory.GetFiles(@"d:\DefaultRepository\", "*.mp3", SearchOption.AllDirectories);
            //foreach (var item in l)
            //{
            //    IWMPMedia w = _wmp.newMedia(item);
            //    _wmp.currentPlaylist.appendItem(w);
            //    Console.WriteLine($"{w.durationString} {w.name}");
            //}
            
            //_wmp.controls.play();

        }

        
        

        bool _isLocked;
        public override bool LockUnLock {
            get
            {
                return _isLocked;
            }
            set
            {
                _isLocked = value;
                if (_isLocked) _skin.Render("\nPlayer locked...");
                else _skin.Render("\nPlayer unlocked...");
            }
        }

        public override int Volume {
            get
            {
                return _wmp.settings.volume;
            }
            set
            {
                _skin.Render("\ninput value :");
                int volumeValue = 0;
                if (int.TryParse(Console.ReadLine(), out volumeValue))
                {
                    _wmp.settings.volume = volumeValue;
                    
                    _skin.Render($"\nVolume changed : {_wmp.settings.volume}");
                }
            }
        }


        public override void Play()
        {
            if (!_isLocked)
            {
                if (_currentItem != null)
                {
                    if (_playing)
                    {
                        Stop();
                    }

                    _playing = true;
                    _skin.Render("\nPlayer started.");
                    
                    _wmp.URL = _currentItem.Path;
                    
                    GetPlayingItemData(_currentItem);
                    
                    try
                    {
                        _wmp.controls.play();
                    }
                    catch (Exception ex)
                    {
                        _skin.Render(ex.Message);
                        _plaingItems.Remove(_currentItem);
                    }

                }
                else
                {
                    _skin.Render("Please choose a song.");
                }
            }
            else
            {
                _skin.Render("Player locked... Unlock it to play song.");
            }
            
        }
        public override void Stop()
        {
            _playing = false;
            _wmp.controls.stop();
            _skin.Render("\nPlayer stopped.");
            
        }

        bool _paused = false;
        double _songTimeMark = 0.0;
        public override void Pause()
        {
            if (_paused)
            {
                _paused = false;
                _wmp.controls.currentPosition = _songTimeMark;
                _wmp.controls.play();
            }
            else
            {
                _paused = true;
                _songTimeMark = _wmp.controls.currentPosition;
                _wmp.controls.pause();
            }
        }

        public override void ShowPlaylist()
        {
            int i = 1;
            _skin.Clear();

            Skin _tempSkin = _skin;
            _skin = SkinFactory.CreateSkin("classic", "");
            _skin.Render(new string('|', 100));
            foreach (var item in _plaingItems)
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
                _skin.Render(String.Format("{0,2} : {1, 10}  {2, -10}  {3,-25}", i, item.Genre, item.Like == true ? "Like!" : (item.Like == false ? "Dislike" : "neutral"), item.Title.CutString(20)));

                i++;
            }

            _skin.Render(new string('|', 100));
            _skin = _tempSkin;
           
        }

        protected override void GetPlayingItemData(AudioItem item)
        {
            var (duration, durationMS, title, path, like) = item;
            _skin.Render($"{title.CutString(30)} \t {duration} \t {durationMS}");
            
        }

        private void _wmp_PlayStateChange(int NewState)
        {
            switch ((WMPPlayState)NewState)
            {
                case WMPPlayState.wmppsPlaying:

                    //_currentSong.DurationMinSec = _wmp.currentMedia.durationString;
                    //_currentSong.Duration = _wmp.currentMedia.duration;
                    break;
                case WMPPlayState.wmppsStopped:
                    
                    break;

            }
        }

        
        public override void ClosePlayer()
        {
            Stop();
            _wmp.PlayStateChange -= _wmp_PlayStateChange;
            
            _wmp = null;
            _skin.Render("Qiut...");
            
        }
        
        

        public void Filter_ByGenre()
        {
            _skin.Render("Input Genre you want..");
            AudioItem.AudioGenre genre;

            if(Enum.TryParse(Console.ReadLine().Trim().ToUpper(), out genre))
            {
                _plaingItems = _plaingItems.Where(x => x.Genre == genre).Select(x => x).ToList();
            }
        }















        //public void SongListGenreSort(string genre)
        //{
        //    _skin.Render("Input Genre you want..");
        //    string g = genre;

        //    GenreType gt = (GenreType)Enum.Parse(typeof(GenreType), genre);
        //    List<Song> filteredSongList = new List<Song>();
        //    foreach (var item in _songList)
        //    {
        //        if(item.Genre == gt)
        //        {
        //            filteredSongList.Add(item);
        //        }
        //    }
        //    _songList = filteredSongList;


        //}
        //public void SongListShow()
        //{
        //    int i = 1;
        //    _skin.Clear();

        //    Skin _tempSkin = _skin;
        //    _skin = SkinFactory.CreateSkin("classic", "");
        //    _skin.Render(new string('|', 100));
        //    //Console.WriteLine(new string('|', 100));
        //    foreach (var item in _songList)
        //    {
        //        if (item.Like == true)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Green;
        //        }
        //        else if (item.Like == false)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //        }
        //        else
        //        {
        //            Console.ForegroundColor = ConsoleColor.White;
        //        }
        //        //Console.WriteLine("{0} : {1, 10}   {2,-25}", i, item.Genre, item.Title.CutString(20));

        //        _skin.Render(String.Format("{0,2} : {1, 10}  {2, -10}  {3,-25}", i, item.Genre, item.Like == true ? "Like!" : (item.Like == false ? "Dislike" : "neutral"), item.Title.CutString(20)));

        //        //Console.ForegroundColor = ConsoleColor.White;
        //        i++;
        //    }

        //    _skin.Render(new string('|', 50));
        //    _skin = _tempSkin;
        //    //Console.WriteLine(new string('|', 50));
        //}



    }


}
