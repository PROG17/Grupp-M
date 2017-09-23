using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DungeonCrawler
{
    public static class Globals
    {
        public const int windowWidth = 120;
        public const int windowHeight = 30;

        //public static Dictionary<string, Item> items = new Dictionary<string, Item>();
        //public static Dictionary<string, Room> rooms = new Dictionary<string, Room>();

        public static Dictionary<INames, Item> items = new Dictionary<INames, Item>();
        public static Dictionary<RNames, Room> rooms = new Dictionary<RNames, Room>();
    }

    class DungeonCrawler
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Globals.windowWidth, Globals.windowHeight);

            LoadGame.Load();

            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.Entrance].Name, 5, 5, false);
            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.Entrance].Description, 10, 6, true);

            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.BathRoom].Name, 5, 10, false);
            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.BathRoom].Description, 10, 11, true);
            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.BathRoom].Description2, 10, 12, true);

            //GFXText.PrintTextWithHighlights(Globals.rooms["entrance"].Name, 5, 5, false);
            //GFXText.PrintTextWithHighlights(Globals.rooms["entrance"].Description, 10, 6, true);

            //GFXText.PrintTextWithHighlights(Globals.rooms["bathroom"].Name, 5, 10, false);
            //GFXText.PrintTextWithHighlights(Globals.rooms["bathroom"].Description, 10, 11, true);
            //GFXText.PrintTextWithHighlights(Globals.rooms["bathroom"].Description2, 10, 12, true);

        }
    }
}
