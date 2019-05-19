using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class EventArguments
    {
    }
    //public event EventHandler<PlayingStateEventArgs> PlayerPlayingStateChanged;
    //public event EventHandler<PlaingItemEventArgs> SongStarted;
    //public event EventHandler<SongListEventArgs> CollectionChanged;
    //public event EventHandler<VolumeEventArgs> VolumeChanged;
    //public event EventHandler<LockEventArgs> LockStateChanged;
    public class PlayingStateEventArgs : EventArgs
    {
        public bool IsPlaying;
    }
    public class PlaingItemEventArgs : EventArgs
    {
        public string ItemTitle;
        public string ItemDuration;
        public string ItemDurationMinSec;
    }
    
    public class VolumeEventArgs : EventArgs
    {
        public int Volume;
    }
    public class LockEventArgs : EventArgs
    {
        public bool IsLocked;
    }
}
