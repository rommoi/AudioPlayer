using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.OutputSkins
{
    public class MyEyes_____ : Skin
    {
        public override string Name { get { return "Oh! My Eyes!!!"; } }

        public override void Clear()
        {
            Console.Clear();
            
        }

        public override void Render(string txt)
        {
            Random rnd = new Random();
            
            foreach (var item in txt)
            {
                
                Console.ForegroundColor = (ConsoleColor)rnd.Next(0, 15);
                Console.BackgroundColor = (ConsoleColor)rnd.Next(0, 15);
                Console.Write(item);
            }
            Console.WriteLine();
        }
    }
}
