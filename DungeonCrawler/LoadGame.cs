using System;

namespace DungeonCrawler
{
    class LoadGame
    {
        public static void Load()
        {
            LoadItems();
        }

        private static void LoadItems()
        {
            // Load all items

            Globals.items.Add(new Item(10, "Rusty key", "A rusted key, probably without any use."));
            Globals.items.Add(new Item(100, "Silver key", "Ohh, so shiny."));
            Globals.items.Add(new Item(1000, "Gold coin", "An old gold coin. Worn out but probably still worth a lot!"));
        }
    }
}