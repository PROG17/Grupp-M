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
        }

        private static void LoadItems()
        {
            // Load all items


            // Problem HERE: if I assign an item from items to a Room, I cannot assign the same object to another
            // room, because the object reference is the SAME!
            Globals.items.Add(INames.clue1, new Item("A piece of paper", "Description of clue 1"));
            Globals.items.Add(INames.clue2, new Item("A crumbled paper", "Description of clue 2"));
            Globals.items.Add(INames.clue3, new Item("A torn off hand with carved writings", "Description of clue 3"));

            Globals.items.Add(INames.Ax, new Item("An Ax", "A very sharp Ax that you can use to smash a door"));
            Globals.items.Add(INames.Bottle, new Item("A Bottle.", "A closed Bottle"));
            Globals.items.Add(INames.Box, new Item("A Box.", "A closed Box"));

            Globals.items.Add(INames.Chandelier, new Item("A Chandelier", "Lightning the room"));
            Globals.items.Add(INames.Cork, new Item("A Cork.", "Where is the open Bottle?"));
            Globals.items.Add(INames.Key, new Item("A Door Key.", "Use it to open a door"));

            Globals.items.Add(INames.Mailbox, new Item("A MailBox", "Perhaps some messages for you?"));
            Globals.items.Add(INames.Note, new Item("A Note", "You can Inspect to read the content"));
            Globals.items.Add(INames.Throne, new Item("A Throne", "raising on one side of the room"));

            Globals.items.Add(INames.Painting, new Item("A Painting", "with some strange landscape"));
            Globals.items.Add(INames.Torch, new Item("A Torch", "that you can use in dark places"));

        }

        private static void LoadRooms()
        {
            Globals.rooms.Add(RNames.Entrance, new Room("Entrance", "You're standing in a large open hallway. In front of you there " +
                "is an old stairway leading up to the second floor of the mansion. To the left there is a sturdy door behind a " +
                "bookshelf. There is a large [chandelier] covored in cobweb hanging from the ceiling."));

            Globals.rooms.Add(RNames.BathRoom, new Room("Luxurious bathroom", "A white [throne] decorates this room.",
                                                  "When looking carefully, you find a [note] behind the toilet."));



            // Initializing one or more rooms. this should be made by Game Handler

            Room dining = new Room("Dining Room", "A [torch] is on the table in front of you, a [bottle] on the floor and " +
                "a [key] hanging on the wall");

            // Create the doors for Dining Room
            var norDoor = new Door(DStatus.Closed, INames.Key);
            var easDoor = new Door(DStatus.WALL, INames.EMPTY);
            var souDoor = new Door(DStatus.Open, INames.Key);
            var WesDoor = new Door(DStatus.Closed, INames.Key);

            // Create the objects for the Dining Room. BETTER: I take one object from the dictionary 'items'

            

            // Fill the Array of Doors in the Room=dining object
            dining.ExitDoors[(int)Dir.North] = norDoor;
            dining.ExitDoors[(int)Dir.East] = easDoor;
            dining.ExitDoors[(int)Dir.South] = souDoor; // I came from here
            dining.ExitDoors[(int)Dir.West] = WesDoor;

            // Finally Add the Entry in the Collection of rooms

            Globals.rooms.Add(RNames.DiningRoom, dining);

        }
    }
}