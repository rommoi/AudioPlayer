using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.OutputSkins
{
    public abstract class Skin
    {
        public abstract string Name { get; }
        public abstract void Clear();

        public abstract void Render(string txt);
    }
}
