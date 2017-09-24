using System;
using System.Collections.Generic;

namespace DungeonCrawler
{

    class LoadGame
    {
        public static void Load()
        {
            LoadItems();
            LoadRooms();
           // InitCommands();
        }

        private static void LoadItems()
        {
            // Load all items

            
            // Problem HERE: if I assign an object (from items dictionary) to a Room, I cannot assign the same object to another
            // room, because the object reference is the SAME!
            // can the following Dictionary be used for other purposes??
            // Probably obsolete Dictionary.

            Globals.items.Add(INames.CLUE1, new Item("A piece of paper", "Description of clue 1"));
            Globals.items.Add(INames.CLUE2, new Item("A crumbled paper", "Description of clue 2"));
            Globals.items.Add(INames.CLUE3, new Item("A torn off hand with carved writings", "Description of clue 3"));

            Globals.items.Add(INames.AX, new Item("An Ax", "A very sharp Ax that you can use to smash a door"));
            Globals.items.Add(INames.BOTTLE, new Item("A Bottle.", "A closed Bottle"));
            Globals.items.Add(INames.BOX, new Item("A Box.", "A closed Box"));

            Globals.items.Add(INames.CHANDELIER, new Item("A Chandelier", "Lightning the room"));
            Globals.items.Add(INames.CORK, new Item("A Cork.", "Where is the open Bottle?"));
            Globals.items.Add(INames.KEY, new Item("A Door Key.", "Use it to open a door"));

            Globals.items.Add(INames.MAILBOX, new Item("A MailBox", "Perhaps some messages for you?"));
            Globals.items.Add(INames.NOTE, new Item("A Note", "You can Inspect to read the content"));
            Globals.items.Add(INames.THRONE, new Item("A Throne", "raising on one side of the room"));

            Globals.items.Add(INames.PAINTING, new Item("A Painting", "with some strange landscape"));
            Globals.items.Add(INames.TORCH, new Item("A Torch", "that you can use in dark places"));

        }

        private static void LoadRooms()
        {
            
            Globals.rooms.Add(RNames.Entrance, new Room("Entrance", "You're standing in a large open hallway. In front of you there " +
                "is an old stairway leading up to the second floor of the mansion. To the left there is a sturdy door behind a " +
                "bookshelf. There is a large [chandelier] covored in cobweb hanging from the ceiling."));

            Globals.rooms.Add(RNames.BathRoom, new Room("Luxurious bathroom", "A white [throne] decorates this room.",
                                                  "When looking carefully, you find a [note] behind the toilet."));



            // ************************************************************
            // Please USE the format below to define each Room, location of objects
            // Status of Doors, etc.
            // ********************************************************************

            // Initializing one or more rooms. this should be made by Game Handler
            // Better inporting data from File!

            Room dining = new Room("Dining Room", "A [torch] is on the table in front of you, a [bottle] on the floor and " +
                "a [key] hanging on the wall.");

            // Create the doors for Dining Room
            var norDoor = new Door(DStatus.Closed, INames.KEY);
            var easDoor = new Door(DStatus.WALL, INames.EMPTY);
            var souDoor = new Door(DStatus.Open, INames.KEY);
            var WesDoor = new Door(DStatus.Closed, INames.KEY);

            // Fill the Array of Doors in the Room=dining object
            dining.ExitDoors[(int)Dir.NORTH] = norDoor;
            dining.ExitDoors[(int)Dir.EAST] = easDoor;
            dining.ExitDoors[(int)Dir.SOUTH] = souDoor; // I came from here
            dining.ExitDoors[(int)Dir.WEST] = WesDoor;

            // Create the objects for the Dining Room. BETTER: I take one object from the dictionary 'items'
            var torch = new Item("A Torch", "that you can use in dark places", INames.EMPTY, ItemPos.Room);
            var bottle = new Item("A Bottle.", "A closed Bottle", INames.CORK, ItemPos.Room);
            var key = new Item("A Door Key.", "Use it to open a door", INames.EMPTY, ItemPos.Room);

            // Add the items to the Items List in the Room

            dining.RoomItems.Add(torch);
            dining.RoomItems.Add(bottle);
            dining.RoomItems.Add(key);



            // Finally Add the Entry in the Collection of rooms

            Globals.rooms.Add(RNames.DiningRoom, dining);

        }

        //private static void InitCommands()
        //{
        //    List<string> commands = new List<string>()
        //    { "GO", "GET", "DROP", "USE", "ON", "LOOK", "INSPECT", "SHOW" };


        //}
    }
}
