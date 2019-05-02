using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace AudioPlayer.OutputSkins
{
    public class ColorOutputSkin : Skin
    {
        ConsoleColor _color;


        public ColorOutputSkin(string color) // get rgb color #FFAABB
        {
            
            if(!Enum.TryParse(color, true, out _color))
            {
                _color = ConsoleColor.White;
            }
            
            Console.ForegroundColor = _color;
        }

        public override string Name { get { return "Colored"; } }

        public override void Clear()
        {
            Console.Clear();
        }

        public override void Render(string txt)
        {
            Console.WriteLine(txt);
        }
    }
}
