﻿using System;
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
                    break;
                case "random color": return new RandomColorOutputSkin();
                    break;
                case "my eyes": return new MyEyes_____();
                    break;
                default: return new ClassicOutputSkin();
                    break;
            }
        }
    }
}