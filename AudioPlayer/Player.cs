using AudioPlayer.OutputSkins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMPLib;
using System.Runtime.Serialization.Formatters.Binary;

namespace AudioPlayer
{
    class Player : PlayerBase<AudioItem>
    {

        
        WMPLib.WindowsMediaPlayer _wmp;
        enum SerializerType
        {
            BIN,
            SOAP,
            XML,
            JSON
        }
        
        public Player(Skin skin) : base(skin)
        {
            _wmp = new WMPLib.WindowsMediaPlayer();
            _wmp.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(_wmp_PlayStateChange);


            Load(Environment.CurrentDirectory);
           

        }

        public override void Load(string folderPath)
        {
            try
            {
                var files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly).Where(s => new string[] { ".mp3", ".wav" }.Contains(Path.GetExtension(s))).ToList();
                Random rnd = new Random();
                var arr = new bool?[3] { false, true, null };
                
                foreach (var item in files)
                {
                    if (File.Exists(item))
                    {
                        AudioItem ai = new AudioItem(0, Path.GetFileNameWithoutExtension(item), Path.GetFileName(item), 
                            arr[rnd.Next(3)], new Artist(), new Album(), (AudioItem.AudioGenre)rnd.Next(5));
                        _plaingItems.Add(ai);
                        
                    }
                }
                
                _wmp.newPlaylist("playlist1", "");
                foreach (var item in _plaingItems)
                {
                    IWMPMedia w = _wmp.newMedia(item.Path);
                    _wmp.currentPlaylist.appendItem(w);
                    //_wmp.settings.setMode("loop", true);
                    _wmp.settings.autoStart = true;
                    
                    //Console.WriteLine($"{w.durationString} {w.name}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Couldn't open folder:  {0}", folderPath);
            }
        }

        public override void Clear()
        {
            base.Clear();
            _wmp.currentPlaylist.clear();
        }

        
        public override void SavePlaylist(string name)
        {
            PlaylistSerializer.BinSerialize(_plaingItems, name);
        }
        public override void LoadPlaylist(string path)
        {
            _plaingItems = DeserializerFactory.DeserializeObjects(path).ToList();
        
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
                    //if (_playing)
                    //{
                    //    Stop();
                    //}

                    _playing = true;
                    _skin.Render("\nPlayer started.");

                    _wmp.currentMedia = _wmp.newMedia(_currentItem.Path);
                    //_wmp.URL = _currentItem.Path;
                    
                    //GetPlayingItemData(_currentItem);
                    
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
            
            _skin = _tempSkin;
            _skin.Render(new string('|', 100));
            
            
           
        }

        protected override void GetPlayingItemData(AudioItem item)
        {
            var (duration, durationMS, title, path, like) = item;
            _skin.Render($"{_wmp.currentMedia.name.CutString(30)} \t {_wmp.currentMedia.duration} \t {_wmp.currentMedia.durationString}");
            //_skin.Render($"{title.CutString(30)} \t {duration} \t {durationMS}");

        }

        private void _wmp_PlayStateChange(int NewState)
        {
            switch ((WMPPlayState)NewState)
            {
                case WMPPlayState.wmppsPlaying:
                    GetPlayingItemData(_currentItem);
                    //_currentSong.DurationMinSec = _wmp.currentMedia.durationString;
                    //_currentSong.Duration = _wmp.currentMedia.duration;
                    break;
                case WMPPlayState.wmppsStopped:
                    //_currentItem = _plaingItems[(_plaingItems.IndexOf(_currentItem)) % (_plaingItems.Count-1)];
                    //Play();
                    break;
                case WMPPlayState.wmppsMediaEnded:
                    
                    break;
                

            }
        }

        
        public override void ClosePlayer()
        {
            Stop();
            _wmp.PlayStateChange -= _wmp_PlayStateChange;
            
            _wmp.close();
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

        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                ClosePlayer();
                
                Console.WriteLine("Player dispose");

                disposedValue = true;
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
