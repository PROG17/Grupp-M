using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public static class Globals
    {
        public static List<Item> items = new List<Item>();
    }

    class DungeonCrawler
    {
        static void Main(string[] args)
        {
            const int windowWidth = 120;
            const int windowHeight = 30;

            LoadGame.Load();

            Console.SetWindowSize(windowWidth, windowHeight);

            Menu.Run();
        }
    }
}
