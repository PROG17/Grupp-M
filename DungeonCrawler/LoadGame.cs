using System;
using System.Collections.Generic;

namespace DungeonCrawler
{

    class LoadGame
    {
        public const int windowWidth = 120;
        public const int windowHeight = 30;     

        public static Dictionary<INames, Item> items = new Dictionary<INames, Item>();
        public static Dictionary<RNames, Room> rooms = new Dictionary<RNames, Room>();

        public static void Init()
        {
            Console.SetWindowSize(windowWidth, windowHeight);
            LoadItems();
            LoadRooms();
            GameDescription();            
        }



        public static void GameDescription()
        {

            // Formatting Text
            /*GFXText.PrintTextWithHighlights(rooms[RNames.Entrance].Name, 5, 5, false);
            GFXText.PrintTextWithHighlights(rooms[RNames.Entrance].Description, 10, 6, true);

            GFXText.PrintTextWithHighlights(rooms[RNames.BathRoom].Name, 5, 10, false);
            GFXText.PrintTextWithHighlights(rooms[RNames.BathRoom].Description, 10, 11, true);
            GFXText.PrintTextWithHighlights(rooms[RNames.BathRoom].Description2, 10, 12, true);


            GFXText.PrintTextWithHighlights(rooms[RNames.DiningRoom].Name, 5, 14, false);
            GFXText.PrintTextWithHighlights(rooms[RNames.DiningRoom].Description, 5, 16, true);*/
            
        }

        //public static void Load()
        //{
        //    LoadItems();
        //    LoadRooms();
        //    // InitCommands();
        //}

        private static void LoadItems()
        {
            // Load all items


            // Problem HERE: if I assign an object (from items dictionary) to a Room, I cannot assign the same object to another
            // room, because the object reference is the SAME!
            // can the following Dictionary be used for other purposes??
            // Probably obsolete Dictionary.

            // Perhaps THIS dictionary should be Refactored to <KEY = item name><VALUE = string[]>
            // The String array contains 0, 1 or more Descriptions for the item
            // Then during the instantiation of the objects (during room creation) I can just
            // use the item name (enum) to retrieve the description.
            // This offers flexibility in extending the dictionary/items. We change the description
            // only in one place.
            // Ex.
            // items.Add(INames.CLUE1, {"A piece of paper", "Description of clue 1"});

            // This block can be commented out

            //items.Add(INames.CLUE1, new Item("A piece of paper", "Description of clue 1"));
            //items.Add(INames.CLUE2, new Item("A crumbled paper", "Description of clue 2"));
            //items.Add(INames.CLUE3, new Item("A torn off hand with carved writings", "Description of clue 3"));

            //items.Add(INames.AX, new Item("An Ax", "A very sharp Ax that you can use to smash a door"));
            //items.Add(INames.BOTTLE, new Item("A Bottle.", "A closed Bottle"));
            //items.Add(INames.BOX, new Item("A Box.", "A closed Box"));

            //items.Add(INames.CHANDELIER, new Item("A Chandelier", "Lightning the room"));
            //items.Add(INames.CORK, new Item("A Cork.", "Where is the open Bottle?"));
            //items.Add(INames.KEY, new Item("A Door Key.", "Use it to open a door"));

            //items.Add(INames.MAILBOX, new Item("A MailBox", "Perhaps some messages for you?"));
            //items.Add(INames.NOTE, new Item("A Note", "You can Inspect to read the content"));
            //items.Add(INames.THRONE, new Item("A Throne", "raising on one side of the room"));

            //items.Add(INames.PAINTING, new Item("A Painting", "with some strange landscape"));
            //items.Add(INames.TORCH, new Item("A Torch", "that you can use in dark places"));
            // ---------------------------------------------------------------------------------
        }

        private static void LoadRooms()
        {
            // Check BELOW how to define a ROOM
            // 
            
            // When defining ROOMS, the name must be ONE word ONLY and must match the Enum RNames in Enum.cs file
            // 


            rooms.Add(RNames.BathRoom, new Room("Luxurious bathroom", "A white [throne] decorates this room.",
                                                  "When looking carefully, you find a [note] behind the toilet."));



            // ************************************************************
            // Please USE the format below to define each Room, location of objects
            // Status of Doors, etc.
            // ********************************************************************

            // Initializing one or more rooms. this should be made by Game Handler
            // Better inporting data from File!


            // Entrance room

            Room entrance = new Room("Entrance", "You're standing in a large open hallway. In front of you there " +
            "is an old stairway leading up to the second floor of the mansion. To the left there is a sturdy door behind a " +
            "bookshelf. There is a large [chandelier] covered in cobweb hanging from the ceiling.");

            var norDoor = new Door(DStatus.WALL, INames.EMPTY);
            var easDoor = new Door(DStatus.WALL, INames.EMPTY);
            var souDoor = new Door(DStatus.WALL, INames.EMPTY);
            var WesDoor = new Door(DStatus.Closed, INames.KEY, RNames.DiningRoom);

            entrance.exitDoors[(int)Dir.NORTH] = norDoor;
            entrance.exitDoors[(int)Dir.EAST] = easDoor;
            entrance.exitDoors[(int)Dir.SOUTH] = souDoor;
            entrance.exitDoors[(int)Dir.WEST] = WesDoor;

            var chandelier = new Item("Chandelier", "The chandelier looks unpolished and the candles seems to have burned out a long time ago. There is a [chain] hanging down from it which seems to be in reach.", INames.EMPTY, ItemPos.Room, false);
            var chain = new Item("Chain", ".", INames.EMPTY, ItemPos.Room, false);

            entrance.roomItems.Add(chandelier);
            entrance.roomItems.Add(chain);
            //entrance.roomItems.Add(key);  should only exist after USE chain

            rooms.Add(RNames.Entrance, entrance);



            // Dining room

            Room dining = new Room("Dining Room", "A [torch] is on the table in front of you, a [bottle] on the floor and " +
                "a [key] hanging on the wall.");

            // Create the doors for Dining Room -- and where they lead to
            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.Open, INames.EMPTY, RNames.LivingRoom);
            souDoor = new Door(DStatus.Open, INames.KEY, RNames.Entrance);
            WesDoor = new Door(DStatus.WALL, INames.KEY);

            // Fill the Array of Doors in the Room=dining object
            dining.exitDoors[(int)Dir.NORTH] = norDoor;
            dining.exitDoors[(int)Dir.EAST] = easDoor;
            dining.exitDoors[(int)Dir.SOUTH] = souDoor; // I came from here
            dining.exitDoors[(int)Dir.WEST] = WesDoor;

            // Create the objects for the Dining Room. BETTER: I take one object from the dictionary 'items'
            var torch = new Item("Torch", "that you can use in dark places", INames.EMPTY, ItemPos.Room, true);
            var bottle = new Item("Bottle", "A closed Bottle", INames.CORK, ItemPos.Room, true);
            var key = new Item("Key", "Use it to open a door", INames.EMPTY, ItemPos.Room, true);
            var note = new Item("Note", "You can Inspect to read the content",INames.EMPTY,ItemPos.Room, true);

            // Add the items to the Items List in the Room

            dining.roomItems.Add(torch);
            dining.roomItems.Add(bottle);
            dining.roomItems.Add(key);
            dining.roomItems.Add(note);
            
            // Finally Add the Entry in the Collection of rooms

            rooms.Add(RNames.DiningRoom, dining);

        }
        
    }
}
