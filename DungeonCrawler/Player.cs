﻿// using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler

    // Description
    // 
    // Player class is the "core' class of the game. it keeps track of the position in the game of the player, updates the status of the 
    // doors, the number of items in the room and inventory according to the player actions. 
    // Main purpose is to add logic when player interacts with some specific objects (i.e. using some items player can die or can win the game or
    // can open a safe, etc.
    //
    // Properties and field:
    // 
    // - bagSize  : the max number of items a player can carry with him.
    // - Name     : Player name
    // - CurRoom  : the room where Player is located at a specific moment.
    // - inventory: A list of items (objects) the player carries with him (the rucksack)
    // - msg      : the message returned to GameHandler when an action has been executed.
    // 
    // - Constructors: One constructor is used to create the player and initialize the Inventory.
    //                
    // - Methods     : Implementation of all the actions: go, get, drop, use item1, use item1 on item2, look, look item1, show
    //



{
    class Player
    {
        private int bagSize = 5;               // Max objects that can be carried
        public string Name { get; set; }
        public RNames CurRoom { get; set; } = RNames.Entrance;        // Default Room. --- Maybe unnecessary

        // What is 'currentPos' used for?
        // public Dir CurrentPos { get; set; } = Dir.NORTH;          // Default Position. Refers to Enum Dir(ections) in the Enum file
        
        private List<Item> inventory;  // Only Player can access its Inventory
        private string msg;


        // Constructor
        //public Player(string name)
        //{
        //    var inventory = new List<Item>(bagSize);
        //    Name = name;
        //}


        public Player(string name, RNames curRoom, List<Item> inventory)
        {
            this.inventory = inventory;            
            Name = name;
            // CurrentPos = curPos;
            CurRoom = curRoom;
        }

        // Methods

        // Take in the Direction and returns a Message to GameHandler
        // to help the player making next move
        //
        public string Go(Dir dir)
        {

            // Note (int) because dir is enum.
            DStatus doorStatus = LoadGame.rooms[CurRoom].exitDoors[(int)dir].Status;

            // Check first if I can move any direction (if there is a door or a wall)
            // I need the following regardless the position N,E, W,E.

            //
            if (doorStatus == DStatus.WALL)
            {
                msg = "I am sorry, but you hit a Wall!";
            }
            else if (doorStatus == DStatus.Closed)
            {
                msg = "You find a door which is Locked. What would you like to do?";
            }
            // If the players tries to move through an open door, the following will take place
            else
            {
                Console.Clear();
                // NOTE - A bit confusing CurRoom as index and then assigned ;-))

                CurRoom = LoadGame.rooms[CurRoom].exitDoors[(int)dir].LeadsToRoom;                      // Reads from list of rooms to find out where to go

                //ENDROOM
                if (LoadGame.rooms[CurRoom].EndPoint)
                {
                    // Player has completed the game
                    Console.Clear();
                    //string msg1 = "The medallion starts to glow and suddenly you start to levitate.";
                    //string msg2 = "Congratulations you have solved the mystery and will now be set free.";
                    string[] msg = {
                        "You decide to walk through the newly opened doorway, entering the blinding light.",
                        "You're finally able to leave this cursed mansion.",
                        "Congratulations " + Name + ", you solved the mystery!"
                    };
                    for (int i = 0; i < msg.Count(); i++)
                    {
                        GFXText.PrintTxt(-1, 5 + i * 2, Globals.TextTrail, Globals.TextDelay, msg[i], false, false);
                        System.Threading.Thread.Sleep(Globals.SleepTime);
                    }
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                //END OF ENDROOM


                if (LoadGame.rooms[CurRoom].Visited)                                                    // If the new room already visited (player backtracking), text will be printed instantly
                {
                    GFXText.PrintTxt(Globals.RoomNameXPos, Globals.RoomNameYPos, 0, 0, LoadGame.rooms[CurRoom].Name, true, false);
                    //GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].name, Globals.RoomNameXPos, Globals.RoomNameYPos, false);
                    GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].Description, Globals.RoomDescriptionXPos, Globals.RoomDescriptionYPos, false);
                }
                else
                {
                    GFXText.PrintTxt(Globals.RoomNameXPos, Globals.RoomNameYPos, Globals.TextTrail, Globals.TextDelay*5, LoadGame.rooms[CurRoom].Name, false, false);
                    //GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].name, Globals.RoomNameXPos, Globals.RoomNameYPos, true);          // If the new room is not visited, text will be printed slowly
                    GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].Description, Globals.RoomDescriptionXPos, Globals.RoomDescriptionYPos, true);
                    LoadGame.rooms[CurRoom].Visited = true;
                }
                return null;
            }
            return msg;
        }



        public string Get(INames item)
        {
            var roomItems = LoadGame.rooms[CurRoom].roomItems;

            for (int i = 0; i < roomItems.Count(); i++)
            {
                // I search for the item in the list within the current room
                if (roomItems[i].Name.ToUpper() == item.ToString() && roomItems[i].Pickup)  // item is enum! need conversion to string
                {
                    // Do I have space in the bag?
                    if (inventory.Count() < bagSize && inventory.Count() >= 0)
                    {
                        // Update the item location in Room object. I do not touch the object in the room!

                        roomItems[i].BelongsTo = ItemPos.Inventory;

                        // I need to test which of the following works !
                        // Both following ways are OK. tested.
                        // Commented out tmp

                        // If both Torch and Ivy are in the room...
                        if (roomItems[i].Name.ToUpper() == "TORCH")
                        {
                            for (int j = 0; j < roomItems.Count; j++)
                            {
                                if (roomItems[j].Name.ToUpper() == "IVY")
                                {
                                    roomItems[j].Description = "You stick your head out the window and find nothing more hidden in the ivy.";

                                }
                            }

                        }

                        // var tmp = new Item(roomItems[i].Name, roomItems[i].Description, roomItems[i].CombWith, roomItems[i].BelongsTo);
                        var tmp1 = new Item(roomItems[i]);   // Using the copy constructor                        
                        inventory.Add(tmp1);              // Can ADD the item from room to Player Inventory
                        roomItems.Remove(roomItems[i]);   // Now I can finally remove the Object from Room List

                        return $"You have just collected [{tmp1.Name}] and now you have {inventory.Count()} objects of {bagSize} in your bag.";

                    }
                    else
                    {
                        return $"Sorry! Your bag is Full and you have {bagSize} objects.";
                    }
                }
                else if (roomItems[i].Name.ToUpper() == item.ToString() && !roomItems[i].Pickup)        // item exists but cannot be picked up
                    return $"Cannot pickup {item}.";
            }
            // The User tries to GET an object which is NOT in the room
            if (item.ToString() == "DOOR") return "The door is too heavy to pickup!";
            return $"The {item} is not in this room.";

        }

        public string Drop(INames item)
        {
            // need to check if the item is in my bag first
            // then I can do all the actions and messages
            var roomItems = LoadGame.rooms[CurRoom].roomItems;  // just to make the names shorter in the code..

            Item itemInBag = new Item();
            bool found = false;
            for (int i = 0; i < inventory.Count(); i++)
            {
                if (inventory[i].Name.ToUpper() == item.ToString())
                {
                    found = true;
                    itemInBag = inventory[i];
                    break;
                }
            }
            if (found)
            {
                // need to move the item from inventory in the room
                itemInBag.BelongsTo = ItemPos.Room;

                var tmp1 = new Item(itemInBag);   // Using the copy constructor 
                roomItems.Add(tmp1);
                inventory.Remove(itemInBag);

                return $"You have just dropped [{tmp1.Name}] and now you have {inventory.Count()} objects in your backpack.";
            }
            else
            {
                return $"I am sorry but you don't have {item.ToString()} in your backpack.";
            }
        }

        // Use() is overloaded
        // Use(arg) is currently used to let the user interact with objects (items) in the game
        // For example the first block lets the user "use" the chain and a key drops (spawns) in the room
        public void Use(INames item)
        {
            // Logic for using items in rooms
            var roomItems = LoadGame.rooms[CurRoom].roomItems;
            for (int i = 0; i < roomItems.Count(); i++)
            {
                if (roomItems[i].Name.ToUpper() == item.ToString() && !roomItems[i].IsUsed)
                {
                    // BLOCK CHAIN
                    if (roomItems[i].Name.ToUpper() == "CHAIN")
                    {
                        roomItems[i].IsUsed = true;
                        var key = new Item("Key", "A rusty bronze key.", INames.EMPTY, ItemPos.Room, true);
                        roomItems.Add(key);
                        Console.Clear();
                        GFXText.PrintTextWithHighlights("A [key] falls down.", 2, 2, true);
                        Console.Write("\n\n");
                        return;
                    }
                    // END OF BLOCK CHAIN

                    // BLOCK REMAINS
                    if (roomItems[i].Name.ToUpper() == "REMAINS")
                    {
                        roomItems[i].IsUsed = true;
                        //var hand = new Item("Hand", "It smells really foul. Carved into the hand is a number: 42.", INames.EMPTY, ItemPos.Inventory, true);
                        // If there's space in players inventory, add it there. Else add it to room inventory
                        if (inventory.Count() < bagSize) inventory.Add(new Item("Hand", "It smells really foul. Carved into the hand is a message: 42 is next to 21", INames.EMPTY, ItemPos.Inventory, true));
                        else roomItems.Add(new Item("Hand", "It smells really foul. Carved into the hand is a message: 42 is next to 21.", INames.EMPTY, ItemPos.Room, true));
                        Console.Clear();
                        GFXText.PrintTextWithHighlights("Among the remains you find and pick up an [hand].", 2, 2, true);
                        Console.Write("\n\n");
                        return;
                    }
                    // END OF BLOCK REMAINS

                    // BLOCK PANTRY
                    if (roomItems[i].Name.ToUpper() == "PANTRY")
                    {
                        roomItems[i].IsUsed = true;
                        //var bread = new Item("Bread", "A fresh loaf of bread. Looks and smells really good.", INames.EMPTY, ItemPos.Inventory, true);

                        // I collect the bread if I have space in the inventory, otherwise I leave it in the room.

                        if (inventory.Count() < bagSize)
                        {
                            // roomItems[i].IsUsed = true;
                            inventory.Add(new Item("Bread", "A fresh loaf of bread. Looks and smells really good.", INames.EMPTY, ItemPos.Inventory, true));
                            Console.Clear();
                            GFXText.PrintTextWithHighlights("You open the pantry and find a loaf of [bread] which you pick up.", 2, 2, true);
                        }
                        else
                        {
                            if (roomItems.FirstOrDefault(c => c.Name.ToUpper() == "BREAD") == null)  // the bread has not yet been placed in the room
                            {
                                roomItems.Add(new Item("Bread", "A fresh loaf of bread. Looks and smells really good.", INames.EMPTY, ItemPos.Room, true));
                            }
                            Console.Clear();
                            GFXText.PrintTextWithHighlights("You open the pantry and find a loaf of [bread] which you might pick up.", 2, 2, true);

                        }
                        Console.Write("\n\n");
                        return;
                    }
                    // END OF BLOCK PANTRY

                    // BLOCK THRONE
                    if (roomItems[i].Name.ToUpper() == "THRONE")
                    {
                        Console.Clear();
                        GFXText.PrintTxt(-1, 5, Globals.TextTrail, Globals.TextDelay, "For some reason you decide to drink the vile contents of the toilet...", true, false);
                        System.Threading.Thread.Sleep(Globals.SleepTime);
                        GFXText.PrintTxt(-1, 10, Globals.TextTrail, Globals.TextDelay, "You die...", false, false);
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    // END OF BLOCK THRONE

                    // BLOCK IVY
                    if (roomItems[i].Name.ToUpper() == "IVY")
                    {
                        Console.Clear();
                        GFXText.PrintTxt(-1, 5, Globals.TextTrail, Globals.TextDelay, "You touch the poison ivy and suddenly become paralyzed...", true, false);
                        System.Threading.Thread.Sleep(Globals.SleepTime);
                        GFXText.PrintTxt(-1, 10, Globals.TextTrail, Globals.TextDelay, "You die...", false, false);
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    // END OF BLOCK IVY


                    // Add more blocks of code here for more item uses
                }
                else if (roomItems[i].Name.ToUpper() == item.ToString() && roomItems[i].IsUsed)
                {
                    Console.WriteLine("\n{0} already used...", item);
                    return;
                }
            }

            // Logic for using items in inventory

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Name.ToUpper() == item.ToString().ToUpper())
                {
                    // BLOCK HAND
                    if (item.ToString().ToUpper() == "HAND")
                    {
                        Console.Clear();
                        GFXText.PrintTxt(-1, 5, Globals.TextTrail, Globals.TextDelay, "For some reason you decide to consume the half-rotten hand...", true, false);
                        System.Threading.Thread.Sleep(Globals.SleepTime);
                        GFXText.PrintTxt(-1, 10, Globals.TextTrail, Globals.TextDelay, "You die...", false, false);
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    // END OF BLOCK HAND

                    // BLOCK BREAD
                    if (item.ToString().ToUpper() == "BREAD")
                    {
                        Console.Clear();
                        GFXText.PrintTextWithHighlights("The bread tastes delicious. Argh, but what is this? Inside you find a [goldpiece]!", Globals.RoomDescriptionXPos, Globals.RoomDescriptionYPos, true);
                        var goldpiece = new Item("Goldpiece", "A golden piece of something bigger. What can it be?", INames.EMPTY, ItemPos.Inventory, true);
                        inventory.Remove(inventory[i]);     // REMOVE OLD ITEM
                        inventory.Add(goldpiece);              // ADD NEW ITEM
                        return;
                    }
                    // END OF BLOCK BREAD

                    // Player tries to combine the pieces. If the player got all pieces a medallion is created and the game is completed.

                    if (item == INames.BRONZEPIECE || item == INames.SILVERPIECE || item == INames.GOLDPIECE)
                    {
                        // if (inventory.Contains(new Item("Bronzepiece", "", false)) && inventory.Contains(new Item("Silverpiece", "", false)) && inventory.Contains(new Item("Goldpiece", "", false)))
                        if ((inventory.FirstOrDefault(c => c.Name.ToUpper() == "BRONZEPIECE")) != null &&
                            (inventory.FirstOrDefault(c => c.Name.ToUpper() == "SILVERPIECE")) != null &&
                            (inventory.FirstOrDefault(c => c.Name.ToUpper() == "GOLDPIECE")) != null)
                        {
                            Item tmpItem = inventory.FirstOrDefault(x => x.Name == "Bronzepiece");
                            if (tmpItem != null) inventory.Remove(tmpItem);

                            tmpItem = inventory.FirstOrDefault(x => x.Name == "Silverpiece");
                            if (tmpItem != null) inventory.Remove(tmpItem);

                            tmpItem = inventory.FirstOrDefault(x => x.Name == "Goldpiece");
                            if (tmpItem != null) inventory.Remove(tmpItem);

                            tmpItem = null;

                            var medallion = new Item("Medallion", "A magnificent medallion. Looks ancient and infused with magic.", INames.EMPTY, ItemPos.Room, true);
                            inventory.Add(medallion);
                            Console.Clear();
                            GFXText.PrintTextWithHighlights("You combine the pieces into a magnificent medallion.", 2, 2, true);
                            Console.Write("\n\n");
                            return;
                        }
                        else
                        {
                            Console.Clear();
                            GFXText.PrintTextWithHighlights("You are missing a piece.", 2, 2, true);
                            return;
                        }
                    }
                }
            }
            var breadInRoom = LoadGame.rooms[CurRoom].roomItems.FirstOrDefault(c => c.Name.ToUpper() == "BREAD");
            if (breadInRoom != null) // I have exposed the bread, but I could not USE it because I had no space in the inventory
            {
                Console.Clear();
                GFXText.PrintTextWithHighlights("Try to pickup [Bread] before using it.\n\n", 2, 2, true);
            }
            else
            {
                Console.WriteLine("Cannot use {0}", item);
            }
            // End of logic for using items in inventory

        }

        public string Use(INames item1, INames item2)
        {
            // Check if item1/2 is door also

            // DOOR OPENER
            // This code should be able to use any key named 'key' to unlock any door
            // Either make sure the player only can carry 1 key at a time, or change this/add more code to allow multiple simultaneously key usage
            if (item1 == INames.KEY && item2 == INames.DOOR)
            {
                Console.Clear();
                for (int i = 0; i < LoadGame.rooms[CurRoom].exitDoors.Count(); i++)
                {
                    if (LoadGame.rooms[CurRoom].exitDoors[i].CanBeOpenWith == INames.KEY && LoadGame.rooms[CurRoom].exitDoors[i].Status != DStatus.Open)
                    {
                        for (int inv = 0; inv < inventory.Count(); inv++)
                        {
                            if (inventory[inv].Name.ToUpper() == INames.KEY.ToString())
                            {
                                LoadGame.rooms[CurRoom].exitDoors[i].Status = DStatus.Open;
                                inventory.Remove(inventory[inv]);
                                return "You opened the door.";
                            }
                        }
                        return "You do not have the key.";
                    }
                }
                return "There is no closed door.";
            }

            // END OF DOOR OPENER


            // USE matches on torch and gain a flaming torch

            if (item1 == INames.MATCHES && item2 == INames.TORCH)
            {

                foreach (var i in inventory)
                {
                    if (i.Name.ToUpper() == INames.MATCHES.ToString())
                    {
                        foreach (var j in inventory)
                        {
                            if (j.Name.ToUpper() == INames.TORCH.ToString())
                            {
                                inventory.Remove(i);
                                inventory.Remove(j);

                                var flamingtorch = new Item("Flamingtorch", "A flaming torch. This should light up even the darkest of places.", INames.EMPTY, ItemPos.Room, true);
                                inventory.Add(flamingtorch);
                                Console.Clear();
                                GFXText.PrintTextWithHighlights("You use the matches on the torch and now have a [flamingtorch].", 2, 2, true);
                                Console.Write("\n\n");
                                return "";
                            }
                        }
                    }
                }

                return "You don't have the required items.";
            }
            // END of Use matches on torch

            // Use flamingtorch on brazier to make cellar visible.
            if (item1 == INames.FLAMINGTORCH && item2 == INames.BRAZIER && CurRoom == RNames.Cellar)
            {
                // Check if Player have a flaming torch

                foreach (var k in inventory)
                {
                    if (k.Name.ToUpper() == INames.FLAMINGTORCH.ToString())
                    {
                        //inventory.Remove(k);
                        //Change cellar room description unveil painting
                        string cellardescription = "The cellar is now lit and you find that its covered with large kegs that are covered in moss. On top of some kegs a large [painting] appears.";
                        LoadGame.rooms[CurRoom].Description = cellardescription;
                        LoadGame.rooms[CurRoom].Visited = false;
                        Console.Clear();
                        GFXText.PrintTextWithHighlights("You use the flaming torch and flames start to spark across the room.", 2, 2, true);
                        return "";

                    }
                }

                return "You don't have the required item.";

            }

            // MEDALLION ON PANEL
            if (CurRoom == RNames.Entrance)
            {
                if (item1 == INames.MEDALLION && item2 == INames.PANEL)
                {
                    foreach (var i in inventory)
                    {
                        if (i.Name.ToUpper() == INames.MEDALLION.ToString())
                        {
                            LoadGame.rooms[RNames.Entrance].exitDoors[(int)Dir.SOUTH].Status = DStatus.Open;
                            LoadGame.rooms[RNames.Entrance].Description = "You have entered what seems to be an old abandoned mansion. There is a [note] next to you on the cold marble floor. To the left there is a wooden door behind a bookshelf. Above you is a large [chandelier] covered in cobweb hanging from the ceiling. Behind you there's also a [panel], and now also an opening next to it.";
                            inventory.Remove(i);
                            Console.Clear();
                            GFXText.PrintTextWithHighlights("You insert the medallion into the panel, it seems like a perfect match. After a brief moment the wall next to it creaks open and a bright lights shines through...", 2, 2, true);
                            Console.Write("\n\n");
                            return "";
                        }
                    }
                }
                return "You don't have the required items.";
            }
            // END BLOCK MEDALLION ON PANEL

            return $"You don't have the required items.";
        }

        // Look in the current Room
        // Look with no arguments returns both descriptions of the room instantly, and a list of current 'room inventory'
        // Does not list usable items in the room, only items the player is able to pickup
        public string Look()
        {
            Console.Clear();
            GFXText.PrintTxt(Globals.RoomNameXPos, Globals.RoomNameYPos, 0, 0, LoadGame.rooms[CurRoom].Name, true, false);
            //GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].name, Globals.RoomNameXPos, Globals.RoomNameYPos, false);
            GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].Description, Globals.RoomDescriptionXPos, Globals.RoomDescriptionYPos, false);
            GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].Description2, Globals.RoomDescription2XPos, Globals.RoomDescription2YPos, false);
            Console.Write("\n\n");
            int lineCheck = 0;    // Linecheck is needed to break lines between found items, since PrintTextWithHighlights() is used
            for (int i = 0; i < LoadGame.rooms[CurRoom].roomItems.Count; i++)
            {
                if (LoadGame.rooms[CurRoom].roomItems[i].Pickup)
                {
                    // Message to print when an item is found ===== REVISE TEXT?
                    GFXText.PrintTextWithHighlights($"In this room there's also a [{LoadGame.rooms[CurRoom].roomItems[i].Name}].", 2, 12 + lineCheck, false);
                    lineCheck++;
                }
            }
            return null;
            //return $" return a message to the Game Handler";
        }


        // Look() with arguments returns description of specific items
        public string Look(string arg1)
        {
            string painting = @" You examine the painting and it appears to be a portait of August de Morgan
           ______________________________________________________________________________________________                                  
           |                                                     .,`                                    |  
           |      2358 is in between.            .,:,...`      ``..,```.```                             |
           |      You have to get out..         ,:.,:::,,,``  ``,:; ; ; ;::;,..`                        | 
           |                                  `.; ';';::''';.``,.:,:;;;:;;,':,`                         |
           |                                 `.; ++'+++'++'+;;;;;;+' + '''''' + ':,`                    |   
           |                               ``''++#++##++++++++'+++''++'++''++;:.                        |
           |                               `; '#+##+++####+#+++#++++++#++++++++'':.                     |
           |                              `,'+++++###########+#+++++++++#++++'';;;:.                    |
           |                              `:''++################++'++;''+'++++++'';;;`                  |
           |                             .; +#+#############@#+#+++';,,,,:::'+++++++';:                 |
           |                             ; +####################+++',..,.,,..,;++##+'';.                |
           |                            :####+###########@##++#+';,...```...`..'+#+#':`                 |
           |                          ..'+##################@++;,.````````......:#+++',                 |
           |                         `; '+####@#####@####@###++':.````````````.....++#+;.               |
           |                       ``; ++#####################+:.``````````````...`,+##'''.             |
           |                        ; +#############@##@@##@#+'.``.`.``````````.....;###+':             |
           |                       `##+#######@@###@#@@######:.```....`...`````....`++#++;              |
           |                      `.+++#####@##@@####@@@#####:```````````````.`......##++'`             |
           |                      .,++#######@##@#@##@##@##+;`.````````.`.``````````.:#++':`            |
           |                      ; +#####@@@@@####@###@###+',``...```````````.`.``````+##+'            |
           |                     .:##+###@####@#@@########';.`````.`....`````````````.:+++'             |
           |                    .'++#####@##@###@@@@#####+;,...```.````.`.`````..`````.'#+'             |
           |                   ` '######@#@@@@#@@@#######+,....`...`.``...`.`.``.``.```:++'             |
           |                   ``:+#@@####@@@#@#@@+######'.......`.```....`.`.`.`.`````.+#'             |
           |                   `; ++###@@@#@@@#@##@######+:.....`.....``...`..```.`.````.;;.            |
           |                 ``; +########@##@#@#@@@#####+;...................``...``````,;             |
           |                 `,+####@####@@@@@##@@##@###'...........................`..``+:             |
           |                 :######@@@##@#@@@@#@#@#@###;,......................``..`.``.'':            |
           |                `;##@#@######@@#@@@#@@@#@##++:..........................`.`..'+.            |
           |                `;#####+@@@@###@@@@@@@@##@#+;:..............................,'+;            |
           |                ;####@##@@@@@@@@@@@@@@@@###+;,..............................,';`            |
           |               `+##@####@@@@@@@@@@@@@@@@#@##;.,.........................```.,:`             |
           |               `+#@###@@@@@@@@@@@@@@@@@@@@##;,,.,,.,.,.......................:`             |
           |               `'#####@@@@@@@@@@@#@@@@@@@@##',.....,..,........,.............:`             |
           |               `+#####@@#@#@@@@@#+@@@@@@#@@#',.,...,.,.,.,,;';;,,............:`             |
           |              `,###@@@@@@@#@@@@@@@#@@#@#@@@@#',....,.,.,:'+#####++;,,.......,:``            |
           |              `+######@@@@#@@##@@@##@@@#@@##+#+;,,,:::;;'''++#######+:,:,,:,';              |
           |              .#######@@@#+##+#@@@##@@@@@#+:,:'''+;:;''#######+##@##+';:,,;+''              |
           |              `+#@##@@@@@@+#+##@@##@@@@@##',,,:;;:'++++####':::#@@#@##';,:'##+.             |
           |              `+###@@@@@@@;+'@@@@@@@@@@@#+:.,,,,:;;;+##+##+:::'+@@#@#++'+#####'             |
           |               ;#####@@@@#;+;#@#@#@@@@###+:,,,,,,:'++####'@@###@@@###;'+#@+###'`            |
           |               .+####@@@#@;+:+'#@@@##@@##;:,.,,,,,,,:;:,,''##+#@@+##+####+##@''`            |
           |               `;#####@@#@'';''##@###@@##;:,,,,,,,,,,:,.,;:;''+++;+++:'######,:.            |
           |                ,#@##@@@@@#;',:'#@###@##+';,,,,,.,....,.,:'+++##+,';:,,'###'.'#;`           |
           |                 ,+##@@@@##:#;:;+#####@##'';,:,:,,,,...,,,;+@###;;,,...,+##::,+++`          |
           |                `.###@@@@@@+:#+;+@@@@@###'+':,,,,,,.,.,,::''#++',,.....,'#+,:::.,`          |
           |                `.#####@@@@@;;+;:'+@@#@###+'':,:,,,,,,,,,,'+++';:,......:#+:::`` `          |
           |                 `;####@@@@@#,:;::'#@##@####';:,,,,,.,.,,:;;;,.,,.,.....,++:,,``            |
           |                   ,+##@@@@@#;,,,,;+#######+':;::,,,.,.,,,::,,...,,,.....++'.```            |
           |                    `+####@###:..,:+##@#####''';:,,,.,.,.,......:;;::;...;+.````            |
           |                    ``,######@@;,:'#####@@@@##+;,,,,,,,,,,,,.,,::,..:';..,+,.```            |
           |                    ```,##@##@@@##@#####@@@#+''';:,,,,,,,,,,,:;':.........',.```            |
           |                      ``.'#+###@#@#######@@#++''',:,,,,,,,,,; '' +#;,.,.....',.``           |
           |                    ``   ```:+##@######@#@#@#+';::,,,,,,,,,::';:+#####';,,'..``             |
           |                       `````.,:+#########@##+':;;:,,,,,,,,,;;:..,+#####+'+'```              |
           |                      `````.....:'###########+'; ;,,,,,,,,,::,.....,'######.```             |
           |                      ````........:+#####@##+''';:::,,:,,:;,.....,,'####+'.``` `            |
           |                    ``````..........,'++##@#++':::::,:,,::,..,,....; +##+++.```             |
           |                    `` .; +:............,'####+;;:;:::,,,,,.,:;:....;' +##++````            |
           |                      .++;:.............,,; ++'+';::::,,.,,,:'':,...,,+##+;````             |
           |                     `'##:.................,:'';';::,:,,,,'+##+++##','##+,```               |
           |                     .###+,...................:'';::,,:::;+;:::;++#####++.````              |
           |                   ``,####+:.`..................,:::;;::;';,,,....,,'#+'+..```              |
           |                   ``+######,..................,..,:;;;:';,,.,::,,::;+:;;,.````             |
           |                 ` `.########:................,..,,..,:;';,:..,;++###+;':.,``               |
           |                   `'@#####@##'.`.................,,,...,:; ;:; ''#####+;;`...``            |
           |                  `.####@@@###@+.`....................,,,,..,;++#+###+':.`..`               |
           |                   '#@##@@@@@@###....,..............,,,,,,,,,,,..,:+++'```,``               |
           |                  `+#@##@@@@@@#@@#,........,,.,...,.,,,,,,,,,,,..,,;+';...,.`               |
           |                  :##@##@@@@@@#@#@#:............,...,,:::,,,:,...,,;+',`.:.``               |
           |                `.#######@@@@@####@#'...........,,....,,,,,,;,,,,::;++..:,```               |
           |                .##@#@###@#@@@#@##@@##;.................,,,,'';;;''++,.,.````               |
           |               `##@@#@#####@@@#@###@@##+,..,..............,:+++'++++'...`````               |
           |              `'######@@####@@@@#####@###'...,............,;#++++++'...`````                |
           |             `+@########@@@@@#@@@@@##@@@@@#;,...............,;'++':..```````                |
           |            .########@@#@@@@###@######@@@@#@+:.......................` `````                |
           |          `,###@####@@@@@@@####@#@#@@@##@@@###;....................,'`` ````                |
           |          :############@@@@@#############@@###@#:..................;#; ``  `                |
           |         ;#####@#@######@@@@#@###########@@@@@####:................+##.    `                |
           |       `+##################@#############@#@@@@@@##,...,,.........,###+                     |
           |      .############@#######@@@######@@#####@@@@@@###..............,#@#+:                    |
           |     :#+############@#+#####@############@###@@@####'.`...........:###++`                   |
           |    :+#+#############@#+####@############@###@@@@@@##,............'####+;                   |
           | ` '###+####+########@@####@@@########@#@@###@###@@@##............+####++:                  |
           | ,##########+#+++######@+#############@@######@##@@@##'..........;########+,                |
           |.++#++#################@#########################@@#@##:........,##########+',`             |
           |########################@#++###################@####@#@#,.......;#@##+#######+;.            |
           |###+#+++########+#+######@#++########################@@###+',```.'###+#####+#+++:           |
           |#+###+##+##++###+######+##@#++#+#################@###@#@@@###'..`.+#########++'+''`         |
           |###+############+############++##+######################@@@@##,...,##+#+#+++++++'''`        |
           |###+########+###+###########@#++#########@################@@###``..'#++#++##+++++++'        |
           |###+#+###++######++###########++++#########@#####+######+##@###'``.,##++#++++++++++''.      |
           |###+++######+####+#####+#######++++#+####################+##@###:```'#++++++++++++++'+,     |
           |################################'+++++#########+#########+++#####.``,#+++++++++++++'+'+.    |
           |+####+###########+###+###+######+'++######+##############++++####'```'+++++++++''++'+++',   |
           |+##########+#####+#+############@+''+++#+#+#+####++++++##++++++###:``.#++++++++++++'+++'';  |
           |+####+###########+++########+#####+'+++#+++#+###++++++++#++++'+++#+.``:++++++++'+++'+++'+'  |
           |+#####++######+###+###++###########++++#+++#####++++++++#++++'''+++'``.'++++++++++++'++'''  |
           |##############+####+##########++###@+++#+++###+##+++++++++++++++'+##,``:''+'+++'+++''+++''  |
           |##+##++##########+###+##+#######+#+##++++++++++#++++++++++++'+++''+++``.''++''+'++++''+'''  |
           |###+####+################+############+++++++++#+++++++++++++'++++''+'``++'++++++++++'++''  |
           |##########+##########+###+#####++##+###+'+++++++++++++++++++++'+++'+'+:`+++++++++++++'++''  |
           |++#########+++##+######+#+#########+#++#+'++++'++++++++++''+++''++''''+,+++++++'+'+++''+''  |
           |++#+#+##########+##+###########+#####++##''''+'''+++++''''++++'+''++'++'+'+++++'+''+''''''""|";

            int password = 1323584221;

            if (arg1.ToUpper() == INames.PAINTING.ToString() && CurRoom == RNames.Cellar)
            {
                Console.WriteLine(painting);
                return "";
            }

            if (arg1.ToUpper() == INames.LOCKER.ToString() && CurRoom == RNames.Bedroom)
            {
                Item checkSilver = inventory.FirstOrDefault(c => c.Name == "Silverpiece");
                if (checkSilver != null)
                {
                    Console.Clear();
                    GFXText.PrintTextWithHighlights("The locker is already open and you have a Silverpiece in you rucksac", 2, 2, false);
                    return "";
                }
                else
                {
                    GFXText.PrintTextWithHighlights("The locker looks very sturdy. Its locked with a ten digit number lock.", 2, 2, false);
                    Console.Write("\n Enter the number:");

                    bool result = int.TryParse(Console.ReadLine(), out int number);
                    Console.Clear();

                    if (result)
                    {
                        if (number == password)
                        {
                            var silverpiece = new Item("Silverpiece", "A beatutiful shiny silverpiece. This looks combinable.", INames.EMPTY, ItemPos.Room, false);
                            //Console.Clear();

                            if (inventory.Count() <= bagSize && inventory.Count() >= 0)
                            {
                                silverpiece.BelongsTo = ItemPos.Inventory;
                                inventory.Add(silverpiece);

                                GFXText.PrintTextWithHighlights("The locker opens and you retrieve a [silverpiece].", 2, 2, true);
                                return "";
                            }
                            else
                            {
                                GFXText.PrintTextWithHighlights("Unfortunately, you cannot collect the [silverpiece] as your rucksak is FULL!! Please drop some items.", 2, 2, true);
                                return "";
                            }

                        }
                        else
                        {
                            GFXText.PrintTextWithHighlights("Nothing happends. The password must be incorrect.", 2, 2, true);
                            return "";
                        }
                    }
                    else
                    {
                        return "That is not a number...";
                    }
                }

            }
            // Is the item the player is looking for in the room inventory? Return its description
            // This includes items that player is not able to pickup
            var roomItems = LoadGame.rooms[CurRoom].roomItems;
            for (int i = 0; i < roomItems.Count(); i++)
            {
                if (roomItems[i].Name.ToUpper() == arg1.ToUpper())
                {
                    return roomItems[i].Description;
                }

            }

            // Is the item the player is looking for in the player inventory? Return its description
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Name.ToUpper() == arg1.ToUpper())
                    return inventory[i].Description;
            }
            return "What's this?"; // Error check
        }


        // Removed Inspect()
        /*
        public string Inspect(INames item)
        {
            // Here I need to check first if it is a door
            // or a item to be inspected. 

            return $" return a message to the Game Handler";
        }*/

        //public void Inspect(Door door)
        //{
        //}

        // Show Inventory


        public string Show()
        {
            if (inventory.Count() > 0)
            {
                string str = "";
                foreach (Item m in inventory)
                {
                    str += m.Name + ", ";

                }

                return "You are now carrying " + str + $"and you can carry {bagSize} objects at most with you.";
            }
            else
            {
                return $"Your backpack is empty at the moment. You can carry {bagSize} objects at most with you.";

            }
        }
    }
}
