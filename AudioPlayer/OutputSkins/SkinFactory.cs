using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.OutputSkins
{
    public static class SkinFactory
    {
        public static Skin CreateSkin(string txt, string color)
        {
            
            switch (txt)
            {
                //case "classic": return new ClassicOutputSkin();
                //    break;
                case "colored": return new ColorOutputSkin(color);
                    
                case "random color": return new RandomColorOutputSkin();
                    
                case "my eyes": return new MyEyes_____();
                    
                default: return new ClassicOutputSkin();
                    
            }
        }
    }
}
