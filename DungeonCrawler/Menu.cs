using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Menu
    {
        public static void Run()
        {
            // Show menu and ask for user input
            Console.Clear();
            GFXText.PrintTxt(-1,1,-4,20,"Dungeon Crawler",false,false);
            Console.ReadKey();
        }
    }
}