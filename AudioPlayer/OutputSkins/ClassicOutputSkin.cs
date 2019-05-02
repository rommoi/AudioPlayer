using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.OutputSkins
{
    public class ClassicOutputSkin : Skin
    {
        public override string Name { get { return "Classic"; } }

        public ClassicOutputSkin()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public override void Clear()
        {
            Console.Clear();
        }

        public override void Render(string txt)
        {
            Console.WriteLine(txt);
            //Console.WriteLine(Console.BackgroundColor) ;
        }
    }
}
