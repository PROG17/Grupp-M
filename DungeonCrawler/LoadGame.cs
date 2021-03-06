﻿// using Common;
using System;
using System.Collections.Generic;

namespace DungeonCrawler
{
    // Description
    // 
    // The class LoadGame initialize the game, create the map and position the objects in different rooms.
    // 


    static class LoadGame
    {
        public const int windowWidth = 120;
        public const int windowHeight = 30;     

      //   public static Dictionary<INames, Item> items = new Dictionary<INames, Item>();
        public static Dictionary<RNames, Room> rooms = new Dictionary<RNames, Room>();


        public static void Init()
        {
            Console.SetWindowSize(windowWidth, windowHeight);          
            LoadRooms();
            GameDescription();
        }
        

        public static void GameDescription()
        {
            GFXText.PrintTxt(-1, 3, 0, 20, "\"You are lost in a forest at night and there is a thunderstorm approaching.\"",false,true);
            System.Threading.Thread.Sleep(Globals.SleepTime);
            Console.Clear();
            GFXText.PrintTxt(-1, 3, 0, 20, "\"Finally you find a wooden sign that reads...\"", false, true); 
            System.Threading.Thread.Sleep(Globals.SleepTime);
            GFXText.PrintTxt(-1, 6, 0, 20, "Welcome to De Morgans Mansion", false, true);
            System.Threading.Thread.Sleep(Globals.SleepTime);
            Console.CursorVisible = true;
            Console.Clear();
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

            Room entrance = new Room("Entrance", "You have entered what seems to be an old abandoned mansion." +
                                     " There is a [note] next to you on the cold marble floor. To the left there is a wooden door behind a " +
                                     "bookshelf. Above you is a large [chandelier] covered in cobweb hanging from the ceiling. Behind you there's also a [panel].");

            var norDoor = new Door(DStatus.WALL, INames.EMPTY);
            var easDoor = new Door(DStatus.WALL, INames.EMPTY);
            var souDoor = new Door(DStatus.WALL, INames.EMPTY,RNames.Endroom);
            var WesDoor = new Door(DStatus.Closed, INames.KEY, RNames.DiningRoom);

            entrance.exitDoors[(int)Dir.NORTH] = norDoor;
            entrance.exitDoors[(int)Dir.EAST] = easDoor;
            entrance.exitDoors[(int)Dir.SOUTH] = souDoor;
            entrance.exitDoors[(int)Dir.WEST] = WesDoor;            

            var note = new Item("Note", "I once was lost but now I’m found, was blind but now I see." +
                                " Three there are and together they form the key.", INames.EMPTY, ItemPos.Room, true);
            var chandelier = new Item("Chandelier", "The chandelier looks unpolished and the candles " +
                                      "seems to have burned out a long time ago. There is a [chain]" +
                                      " hanging down from it which seems to be in reach.", INames.EMPTY, ItemPos.Room, false);
            var chain = new Item("Chain", "It seems like something shiny is attached to the [chain]. " +
                                 "Maybe you can use the chain to grab it?", INames.EMPTY, ItemPos.Room, false);
            var panel = new Item("Panel", "Some part seems to be missing.", INames.MEDALLION, ItemPos.Room, false);

            entrance.roomItems.Add(note);
            entrance.roomItems.Add(chandelier);
            entrance.roomItems.Add(chain);
            entrance.roomItems.Add(panel);

            rooms.Add(RNames.Entrance, entrance);



            // Dining room

            Room dining = new Room("Dining Room", "You have entered the dining room. Immediately to the" +
                                   " right there is a door unlocked. In the center of the room is a large" +
                                   " dining table with some plates and empty glasses. There are signs of" +
                                   " a large feast that ended abruptly. There must have been a large family" +
                                   " living here... Next to the end of the dining table is a burnt out [fireplace].");

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
            var fireplace = new Item("Fireplace", "Above the fireplace there is a dusty stone panel" +
                                     " and some unused [matches]. While looking closer inside the " +
                                     "fireplace you find that there is a pile of ashes with a [bronzepiece] in it.", INames.EMPTY, ItemPos.Room, false);
            var bronzepiece = new Item("Bronzepiece", "A bronze piece. Looks like this piece was once combined with another piece.", INames.EMPTY, ItemPos.Room, true);
            var matches = new Item("Matches", "Used to lit things on fire.", INames.EMPTY, ItemPos.Room, true);

            // Add the items to the Items List in the Room

            dining.roomItems.Add(fireplace);
            dining.roomItems.Add(bronzepiece);
            dining.roomItems.Add(matches);

            // Finally Add the Entry in the Collection of rooms

            rooms.Add(RNames.DiningRoom, dining);


            // LIVING ROOM
            Room living = new Room("Living room", "You have entered the living room. To the left is a" +
                                   " narrow hallway and to the right is a large double sided door. " +
                                   "The floor of the room is covered in glass and the windows of" +
                                   " the rooms seems to have been smashed in. This must have happened" +
                                   " a long time ago since [ivy] have started to grow inside the room.");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.Open, INames.EMPTY,RNames.Bedroom);
            souDoor = new Door(DStatus.Open, INames.EMPTY, RNames.DiningRoom);
            WesDoor = new Door(DStatus.Open, INames.EMPTY, RNames.Kitchen);

            living.exitDoors[(int)Dir.NORTH] = norDoor;
            living.exitDoors[(int)Dir.EAST] = easDoor;
            living.exitDoors[(int)Dir.SOUTH] = souDoor;
            living.exitDoors[(int)Dir.WEST] = WesDoor;

            var ivy = new Item("Ivy", "You stick your head out the window to examine where the ivy is coming from." +
                               " Entangled in the dark green ivy there is an old wooden [torch].", INames.EMPTY, ItemPos.Room, false);
            var torch = new Item("Torch", "An old wooden torch.", true);

            living.roomItems.Add(ivy);
            living.roomItems.Add(torch);

            rooms.Add(RNames.LivingRoom, living);



            // KITCHEN
            Room kitchen = new Room("Kitchen", "After witnessing the feast in the dining room, you expect to find a dirty kitchen" +
                                    " filled with pots and pans, as is needed to cook a large amount of food, but instead it's surprisingly empty." +
                                    "  Along one of the walls there's a [pantry] and to your left there's a dirty [window]. Not counting the" +
                                    " door that lead you here, there's only one exit from this room, and that's to your right.", 
                                    "How horrible! On closer inspection you see some [remains] of a corpse on top of the counter!");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.Open, INames.EMPTY, RNames.Cellar);
            souDoor = new Door(DStatus.Open, INames.EMPTY, RNames.LivingRoom);
            WesDoor = new Door(DStatus.WALL, INames.EMPTY);

            kitchen.exitDoors[(int)Dir.NORTH] = norDoor;
            kitchen.exitDoors[(int)Dir.EAST] = easDoor;
            kitchen.exitDoors[(int)Dir.SOUTH] = souDoor;
            kitchen.exitDoors[(int)Dir.WEST] = WesDoor;

            var remains = new Item("Remains", "In a pile of blood lies some remains of what you think once was a human being.", INames.EMPTY, ItemPos.Room, false);
            var window = new Item("Window", "The window is really dirty, and it's hard for you to see through it." +
                                  " It's definitely been a long time since anyone cleaned it.", INames.EMPTY, ItemPos.Room, false);
            var pantry = new Item("Pantry", "The door to the pantry is slightly open...", INames.EMPTY, ItemPos.Room, false);

            kitchen.roomItems.Add(remains);
            kitchen.roomItems.Add(window);
            kitchen.roomItems.Add(pantry);

            rooms.Add(RNames.Kitchen, kitchen);



            // CELLAR
            Room cellar = new Room("Cellar", "The cellar is completely dark. There is a distinct smell of mold, charcoal and burnt wood." +
                                   " While fumbling in the dark you feel something that resemble a [brazier].");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.WALL, INames.EMPTY);
            souDoor = new Door(DStatus.Open, INames.EMPTY, RNames.Kitchen);
            WesDoor = new Door(DStatus.WALL, INames.EMPTY);

            cellar.exitDoors[(int)Dir.NORTH] = norDoor;
            cellar.exitDoors[(int)Dir.EAST] = easDoor;
            cellar.exitDoors[(int)Dir.SOUTH] = souDoor;
            cellar.exitDoors[(int)Dir.WEST] = WesDoor;

            var brazier = new Item("Brazier", "Smells of charcoal. Can it be lit with a torch perhaps?", INames.EMPTY, ItemPos.Room, false);
            var painting = new Item("Painting", "A painting of August De Morgan", INames.EMPTY, ItemPos.Room, false);

            // Add the items to the Items List in the Room

            cellar.roomItems.Add(brazier);
            cellar.roomItems.Add(painting);

            rooms.Add(RNames.Cellar, cellar);


            // BEDROOM
            Room bedroom = new Room("Bedroom", "You enter the master bedroom. In the center of the room is a large [bed]" +
                                    " but something is definately not right in here...There is a distinct smell of rotten flesh." +
                                    " Could the smell come from the room to the left?");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.WALL, INames.EMPTY);
            souDoor = new Door(DStatus.Open, INames.EMPTY, RNames.LivingRoom);
            WesDoor = new Door(DStatus.Open, INames.EMPTY, RNames.Bathroom);

            bedroom.exitDoors[(int)Dir.NORTH] = norDoor;
            bedroom.exitDoors[(int)Dir.EAST] = easDoor;
            bedroom.exitDoors[(int)Dir.SOUTH] = souDoor;
            bedroom.exitDoors[(int)Dir.WEST] = WesDoor;

            var locker = new Item("Locker", "A sturdy locker with a ten digit number lock.", INames.EMPTY, ItemPos.Room, false);
            var bed = new Item("Bed", "The sheets of the bed are covered in blood. You look under the bed and find a hidden [locker].", INames.EMPTY, ItemPos.Room, false);

            bedroom.roomItems.Add(bed);
            bedroom.roomItems.Add(locker);
            
            rooms.Add(RNames.Bedroom, bedroom);



            // BATHROOM
            Room bathroom = new Room("Bathroom", "The room where the magic happens. But in this specific" +
                                     " restroom it seems to be black magic. Something terrible has happened" +
                                     " in here. In the middle of the room there's a white [throne]. " +
                                     "There are no other exits from here, the only way you can go is back.", 
                                     "The walls are covered in [blood], it reeks of evil.");

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.WALL, INames.EMPTY);
            souDoor = new Door(DStatus.Open, INames.EMPTY,RNames.Bedroom);
            WesDoor = new Door(DStatus.WALL, INames.EMPTY);

            bathroom.exitDoors[(int)Dir.NORTH] = norDoor;
            bathroom.exitDoors[(int)Dir.EAST] = easDoor;
            bathroom.exitDoors[(int)Dir.SOUTH] = souDoor;
            bathroom.exitDoors[(int)Dir.WEST] = WesDoor;

            var blood = new Item("Blood", "You find an area of the bloody wall where it seems someone has carved in a message: " +
                                 "First we were 13 and at the end we were 21", INames.EMPTY, ItemPos.Room, false);
            var throne = new Item("Throne", "It's just a toilet. A filthy toilet. Filled to the brim with real stinky stuff.", INames.EMPTY, ItemPos.Room, false);

            bathroom.roomItems.Add(blood);
            bathroom.roomItems.Add(throne);

            rooms.Add(RNames.Bathroom, bathroom);

            // ENDROOM
            Room endroom = new Room("Endroom", "Endroom");
            endroom.EndPoint = true;

            norDoor = new Door(DStatus.WALL, INames.EMPTY);
            easDoor = new Door(DStatus.WALL, INames.EMPTY);
            souDoor = new Door(DStatus.WALL, INames.EMPTY);
            WesDoor = new Door(DStatus.WALL, INames.EMPTY);

            endroom.exitDoors[(int)Dir.NORTH] = norDoor;
            endroom.exitDoors[(int)Dir.EAST] = easDoor;
            endroom.exitDoors[(int)Dir.SOUTH] = souDoor;
            endroom.exitDoors[(int)Dir.WEST] = WesDoor;

            rooms.Add(RNames.Endroom, endroom);
        }        
    }
}
