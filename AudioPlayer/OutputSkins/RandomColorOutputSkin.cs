using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.OutputSkins
{
    public class RandomColorOutputSkin : Skin
    {
        ConsoleColor _color;
        Random rnd;
        
        public RandomColorOutputSkin()
        {
            rnd = new Random();
        }

        public override string Name { get { return "RandomColor"; } }

        public override void Clear()
        {
            Console.Clear();
            
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine(new StringBuilder().Append('\u058D', 30).ToString());
            Console.OutputEncoding = Encoding.ASCII;
        }

        public override void Render(string txt)
        {
            _color = (ConsoleColor)rnd.Next(1, 15); //exclude black, because background is black.
            
            Console.ForegroundColor = _color;
            Console.WriteLine(txt);
        }
    }
}
