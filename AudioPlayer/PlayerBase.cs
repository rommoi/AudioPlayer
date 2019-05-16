using AudioPlayer.OutputSkins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public abstract class PlayerBase<T>
        : IDisposable
        where T : PlaingItem
    {
        
        protected List<T> _plaingItems;
        protected T _currentItem;

        protected Skin _skin;
        
        public virtual bool LockUnLock { get; set; }

        public abstract int Volume { get; set; }

        protected bool _playing;                    //boolean flag. true player plays, false player stopped
        protected double _itemPlayingPosition;     //plaing item position

        public abstract void Play();
        public abstract void Stop();
        public abstract void Pause();
        public abstract void ClosePlayer();

        public bool IsPlaying {
            get
            {
                return _playing;
            }
        }

        protected virtual void GetPlayingItemData(T item)
        {
            var (duration, durationMS, title, path, like) = item;
            _skin.Render($"{duration}, {durationMS}, {title.CutString(30)}, {path}, {like}");
        }

        public virtual void ShowPlaylist()
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
                _skin.Render(String.Format("{0,2} : {1, 10}  {2, -10}", i, item.Like == true ? "Like!" : (item.Like == false ? "Dislike" : "neutral"), item.Title.CutString(20)));
                
                i++;
            }

            _skin.Render(new string('|', 50));
            _skin = _tempSkin;
            
        }

        public virtual void SetLike()
        {
            _skin.Render("Input songs number..");
            
            int num;
            int.TryParse(Console.ReadLine().Trim(), out num);


            if (--num >= 0 && num < _plaingItems.Count)
            {
                _skin.Render("Input like, dislike or neutral..");
                
                var state = Console.ReadLine().Trim();

                if (String.Compare(state, "like") == 0)
                {
                    _plaingItems[num].Like = true;
                }
                else if (String.Compare(state, "dislike") == 0)
                {
                    _plaingItems[num].Like = false;
                }
                else if (String.Compare(state, String.Empty) == 0)
                {
                    _plaingItems[num].Like = null;
                }
            }
        }

        public virtual void SetSkin()
        {
            _skin.Render("input skin name:");

            _skin = SkinFactory.CreateSkin(Console.ReadLine().ToLower().Trim(), "");
            _skin.Render($"You set : {_skin.Name}");
        }

        public abstract void Load(string folderPath);
        public virtual void Clear()
        {
            _plaingItems.Clear();
        }

        public abstract void SavePlaylist(string name);
        public abstract void LoadPlaylist(string path);

        public virtual void SetPlayingItem()
        {
            _skin.Render("\nSongs:");
            ShowPlaylist();
            UInt32 selected = 0;
            _skin.Render("\nSelect song number:");
            UInt32.TryParse(Console.ReadLine(), out selected);
            if (selected != 0 && (selected - 1) < _plaingItems.Count)
            {
                selected--;
                _currentItem = _plaingItems[(int)(selected)];

            }
        }

        public virtual void SortPlayList_ByTitle()
        {
            _plaingItems = _plaingItems.SortByTitle().ToList();
        }

        public virtual void ShufflePlayList()
        {
            _plaingItems = _plaingItems.Shuffle().ToList();
        }

        public virtual void LabelOutput(string label)
        {
            _skin.Render(label);
        }

        public PlayerBase(Skin skin)
        {
            _plaingItems = new List<T>();
            _skin = skin ?? new ClassicOutputSkin();
        }

        #region IDisposable Support
        protected bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~PlayerBase()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Console.WriteLine("BASE FINALIZER!!");
            
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Console.WriteLine("BASE DISPOSE!!");
            Dispose(true);
            _skin = null;
            _currentItem = null;
            _plaingItems = null;
            // TODO: uncomment the following line if the finalizer is overridden above.
             GC.SuppressFinalize(this);
        }
        
        #endregion

    }
    
}
