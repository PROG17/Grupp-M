using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class DungeonCrawler
    {
        static void Main(string[] args)
        {
            const int windowWidth = 120;
            const int windowHeight = 30;

            Console.SetWindowSize(windowWidth, windowHeight);

            Menu.Run();
        }
    }
}
