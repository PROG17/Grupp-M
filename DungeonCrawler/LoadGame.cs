﻿using System;
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
            GFXText.PrintTxt(-1, 3, 0, Globals.TextDelay, "\"I once was lost but now I’m found, was blind but now I see. Five there are and together they form the key\"",true,true);
            System.Threading.Thread.Sleep(Globals.SleepTime);
            GFXText.PrintTxt(-1, 6, 0, Globals.TextDelay, "Welcome to De Morgans Mansion", true, true);
            System.Threading.Thread.Sleep(Globals.SleepTime);
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

            // COPY PASTE TO MAKE A NEW ROOM
            /*
            Room entrance = new Room("room name", "description1","description2 (optional)");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.WALL, INames.EMPTY);
            souDoor = new Door(DStatus.WALL, INames.EMPTY);
            WesDoor = new Door(DStatus.WALL, INames.EMPTY, RNames.DiningRoom);

            entrance.exitDoors[(int)Dir.NORTH] = norDoor;
            entrance.exitDoors[(int)Dir.EAST] = easDoor;
            entrance.exitDoors[(int)Dir.SOUTH] = souDoor;
            entrance.exitDoors[(int)Dir.WEST] = WesDoor;

            var item1 = new Item("item name", "item description", INames.EMPTY, ItemPos.Room, false);

            entrance.roomItems.Add(item1);

            rooms.Add(RNames.Entrance, entrance);
            */



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
            var chain = new Item("Chain", "It seems the [chain] is within reach...", INames.EMPTY, ItemPos.Room, false);

            entrance.roomItems.Add(chandelier);
            entrance.roomItems.Add(chain);

            rooms.Add(RNames.Entrance, entrance);



            // Dining room

            Room dining = new Room("Dining Room", "You have entered the dining room. Immediately to the right there is a door.","In the center of the room is a large dining table with some plates and empty glasses. There are signs of a large feast that ended abruptly. There must have been a large family living here... Next to the end of the dining table is a burnt out [fireplace].");

            // Create the doors for Dining Room -- and where they lead to
            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.Open, INames.EMPTY, RNames.LivingRoom);
            souDoor = new Door(DStatus.Open, INames.EMPTY, RNames.Entrance);
            WesDoor = new Door(DStatus.WALL, INames.EMPTY);

            // Fill the Array of Doors in the Room=dining object
            dining.exitDoors[(int)Dir.NORTH] = norDoor;
            dining.exitDoors[(int)Dir.EAST] = easDoor;
            dining.exitDoors[(int)Dir.SOUTH] = souDoor; // I came from here
            dining.exitDoors[(int)Dir.WEST] = WesDoor;

            // Create the objects for the Dining Room. BETTER: I take one object from the dictionary 'items'
            var fireplace = new Item("Fireplace", "Above the fireplace there is a dusty stone panel and some unused [matches]. While looking closer inside the fireplace you find that there is a pile of ashes with a bronze [piece] in it.", INames.EMPTY, ItemPos.Room, false);
            var piece1 = new Item("Piece", "A bronze piece.", INames.EMPTY, ItemPos.Room, true);
            var matches = new Item("Matches", "Used to lit things on fire.", INames.EMPTY, ItemPos.Room, true);

            // Add the items to the Items List in the Room

            dining.roomItems.Add(fireplace);
            dining.roomItems.Add(piece1);
            dining.roomItems.Add(matches);

            // Finally Add the Entry in the Collection of rooms

            rooms.Add(RNames.DiningRoom, dining);


            // LIVING ROOM
            Room living = new Room("Living room", "You have entered the living room. The floor of the room is covered in glass and the windows of the rooms seems to have been smashed in. This must have happened a long time ago since [ivy] have started to grow inside the room.", "description2 (optional)");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.Open, INames.EMPTY,RNames.Bedroom);
            souDoor = new Door(DStatus.Open, INames.EMPTY, RNames.DiningRoom);
            WesDoor = new Door(DStatus.Open, INames.EMPTY, RNames.Kitchen);

            living.exitDoors[(int)Dir.NORTH] = norDoor;
            living.exitDoors[(int)Dir.EAST] = easDoor;
            living.exitDoors[(int)Dir.SOUTH] = souDoor;
            living.exitDoors[(int)Dir.WEST] = WesDoor;

            var ivy = new Item("Ivy", "You stick your head out the window to examine where the ivy is coming from. Entangled in the dark green ivy there is an old wooden [torch].", INames.EMPTY, ItemPos.Room, false);
            var torch = new Item("Torch", "An old wooden torch.", true);

            living.roomItems.Add(ivy);
            living.roomItems.Add(torch);

            rooms.Add(RNames.LivingRoom, living);



            // KITCHEN
            Room kitchen = new Room("Kitchen", "After witnessing the feast in the living room, you expect to find a dirty kitchen filled with pots and pans, as is needed to cook a large amount of food, but instead it's surprisingly empty.  Along one of the walls there's a [pantry] and to your left there's a dirty [window]. Not counting the door that lead you here, there's only one exit from this room, and that's to your right.", "How horrible! On closer inspection you see some [remains] of a corpse on top of the counter!");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.Open, INames.EMPTY, RNames.Cellar);
            souDoor = new Door(DStatus.Open, INames.EMPTY, RNames.LivingRoom);
            WesDoor = new Door(DStatus.WALL, INames.EMPTY);

            kitchen.exitDoors[(int)Dir.NORTH] = norDoor;
            kitchen.exitDoors[(int)Dir.EAST] = easDoor;
            kitchen.exitDoors[(int)Dir.SOUTH] = souDoor;
            kitchen.exitDoors[(int)Dir.WEST] = WesDoor;

            var remains = new Item("Human remains", "In a pile of blood lies some remains of what you think once was a human being.", INames.EMPTY, ItemPos.Room, false);
            var window = new Item("Dirty window", "The window is really dirty, and it's hard for you to see through it. It's definitely been a long time since anyone cleaned it.", INames.EMPTY, ItemPos.Room, false);
            var pantry = new Item("Pantry", "The door to the pantry is slightly open...", INames.EMPTY, ItemPos.Room, false);

            kitchen.roomItems.Add(remains);
            kitchen.roomItems.Add(window);
            kitchen.roomItems.Add(pantry);

            rooms.Add(RNames.Kitchen, kitchen);



            // CELLAR
            Room cellar = new Room("Cellar", "The cellar is completely dark. There is a distinct smell of mold, charcoal and burnt wood. While fumbling in the dark you feel something that resemble a [brazier]", "The cellar is now lit and you find that its covered with large kegs that are covered in moss. In the corner of the cellar there seems to be a [painting]");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.WALL, INames.EMPTY);
            souDoor = new Door(DStatus.Open, INames.EMPTY, RNames.Kitchen);
            WesDoor = new Door(DStatus.WALL, INames.EMPTY);

            cellar.exitDoors[(int)Dir.NORTH] = norDoor;
            cellar.exitDoors[(int)Dir.EAST] = easDoor;
            cellar.exitDoors[(int)Dir.SOUTH] = souDoor;
            cellar.exitDoors[(int)Dir.WEST] = WesDoor;

            var brazier = new Item("Brazier", "Smells of charcoal. Can be lit.", INames.EMPTY, ItemPos.Room, false);
            var painting = new Item("Painting", "A painting of August De Morgan", INames.EMPTY, ItemPos.Room, false);

            // Add the items to the Items List in the Room

            cellar.roomItems.Add(brazier);
            cellar.roomItems.Add(painting);

            rooms.Add(RNames.Cellar, cellar);


            // BEDROOM
            Room bedroom = new Room("Bedroom", "description1", "description2 (optional)");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.WALL, INames.EMPTY);
            souDoor = new Door(DStatus.Open, INames.EMPTY, RNames.LivingRoom);
            WesDoor = new Door(DStatus.Open, INames.EMPTY, RNames.Bathroom);

            bedroom.exitDoors[(int)Dir.NORTH] = norDoor;
            bedroom.exitDoors[(int)Dir.EAST] = easDoor;
            bedroom.exitDoors[(int)Dir.SOUTH] = souDoor;
            bedroom.exitDoors[(int)Dir.WEST] = WesDoor;

            rooms.Add(RNames.Bedroom, bedroom);



            // BATHROOM
            Room bathroom = new Room("Bathroom", "description1", "description2 (optional)");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.WALL, INames.EMPTY);
            souDoor = new Door(DStatus.Open, INames.EMPTY,RNames.Bedroom);
            WesDoor = new Door(DStatus.WALL, INames.EMPTY);

            bathroom.exitDoors[(int)Dir.NORTH] = norDoor;
            bathroom.exitDoors[(int)Dir.EAST] = easDoor;
            bathroom.exitDoors[(int)Dir.SOUTH] = souDoor;
            bathroom.exitDoors[(int)Dir.WEST] = WesDoor;

            rooms.Add(RNames.Bathroom, bathroom);
        }        
    }
}
