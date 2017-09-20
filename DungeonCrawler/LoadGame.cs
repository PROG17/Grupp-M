using System;

namespace DungeonCrawler
{

    // Egidio comment #3


    class LoadGame
    {
        public static void Load()
        {
            LoadItems();
            LoadRooms();
        }

        private static void LoadItems()
        {
            // Load all items

            Globals.items.Add("clue1", new Item("A piece of paper", "Description of clue 1"));
            Globals.items.Add("clue2", new Item("A crumbled paper", "Description of clue 2"));
            Globals.items.Add("clue3", new Item("A torn off hand with carved writings", "Description of clue 3"));
            // Egidio 
        }

        private static void LoadRooms()
        {
            // Load all rooms

            Globals.rooms.Add("entrance", new Room("Entrance","You're standing in a large open hallway. In front of you there is an old stairway leading up to the second floor of the mansion. To the left there is a sturdy door behind a bookshelf. There is a large [chandelier] covored in cobweb hanging from the ceiling."));
            Globals.rooms.Add("bathroom", new Room("Luxurious bathroom", "A white [throne] decorates this room.","When looking carefully, you find a [note] behind the toilet."));
        }
    }
}