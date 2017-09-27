// using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Player
    {
        public string Name { get; set; }

        // What is 'currentPos' used for?
       // public Dir CurrentPos { get; set; } = Dir.NORTH;          // Default Position. Refers to Enum Dir(ections) in the Enum file
        public RNames CurRoom { get; set; } = RNames.Kitchen;    // Default Room. --- Maybe unnecessary

        private int bagSize = 5;               // Max objects that can be carried

        //public List<Item> Inventory = new List<Item>(1)
        //        {new Item("EMPTY", "", INames.EMPTY, ItemPos.NONE) };   // Only Player can access its Inventory

         public List<Item> inventory;



        private string msg;

        // Constructor
        public Player(string name)
        {
            var inventory = new List<Item>(bagSize);
            Name = name;
        }

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
        // ***********************************************************************
        // NOTE !!ALL THE OUTPUT MESSAGES SHOULD BE PUT in an Array or Dictionary
        // so the Method GO and all the others need to return ONLY 
        // **********************************************************************
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
                msg = "You find a door which is Closed. What would you like to do?";
            }
            // If the players tries to move through an open door, the following will take place
            else
            {
                Console.Clear();
                // NOTE - A bit confusing CurRoom as index and then assigned ;-))

                CurRoom = LoadGame.rooms[CurRoom].exitDoors[(int)dir].LeadsToRoom;                      // Reads from list of rooms to find out where to go
                if (LoadGame.rooms[CurRoom].Visited)                                                    // If the new room already visited (player backtracking), text will be printed instantly
                {
                    GFXText.PrintTxt(Globals.RoomNameXPos, Globals.RoomNameYPos, 0, 0, LoadGame.rooms[CurRoom].Name, false, false);
                    //GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].name, Globals.RoomNameXPos, Globals.RoomNameYPos, false);
                    GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].Description, Globals.RoomDescriptionXPos, Globals.RoomDescriptionYPos, false);
                }
                else
                {
                    GFXText.PrintTxt(Globals.RoomNameXPos, Globals.RoomNameYPos, Globals.TextTrail, Globals.TextDelay, LoadGame.rooms[CurRoom].Name, false, false);
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
                    if (inventory.Count() <= bagSize && inventory.Count() >= 0)
                    {
                        // Update the item location in Room object. I do not touch the object in the room!

                        roomItems[i].BelongsTo = ItemPos.Inventory;

                        // I need to test which of the following works !
                        // Both following ways are OK. tested.
                        // Commented out tmp

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

                        return $"You have just collected [{tmp1.Name}] and now you have {inventory.Count()} objects of {bagSize} in your bag";
               
                    }
                    else
                    {
                        return $"Sorry! Your bag is Full and you have {bagSize} objects";
                    }
                }
                else if (roomItems[i].Name.ToUpper() == item.ToString() && !roomItems[i].Pickup)        // item exists but cannot be picked up
                    return $"Cannot pickup {item}.";
            }
            // The User tries to GET an object which is NOT in the room
            return $"The {item} is not in this room. Please try again";

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

                return $"You have just dropped [{tmp1.Name}] and now you have {inventory.Count()} objects in your backpack";
            }
            else
            {
                return $"I am sorry but you don't have {item.ToString()} in your backpack";
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
                        GFXText.PrintTextWithHighlights("A [key] falls out.", 2, 2, true);
                        Console.Write("\n\n");
                        return;
                    }
                    // END OF BLOCK CHAIN

                    // BLOCK REMAINS
                    if (roomItems[i].Name.ToUpper() == "REMAINS")
                    {
                        roomItems[i].IsUsed = true;
                        var hand = new Item("Hand", "It smells really foul. Carved into the hand is a number: 42.", INames.EMPTY, ItemPos.Inventory, true);
                        inventory.Add(hand);
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
                        var bread = new Item("Bread", "A fresh loaf of bread. Looks and smells really good.", INames.EMPTY, ItemPos.Inventory, true);
                        inventory.Add(bread);
                        Console.Clear();
                        GFXText.PrintTextWithHighlights("You open the pantry and find a loaf of [bread] which you pick up.", 2, 2, true);
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
                        GFXText.PrintTxt(-1, 5, Globals.TextTrail, Globals.TextDelay, "For some reason you decide to consume the half-rotten hand...",true,false);
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
                        GFXText.PrintTextWithHighlights("The bread tastes delicious. Inside you find a [piece]!",Globals.RoomDescriptionXPos,Globals.RoomDescriptionYPos,true);
                        var piece2 = new Item("Piece2", "A golden piece of something bigger. What can it be?", INames.EMPTY, ItemPos.Inventory, true);
                        inventory.Remove(inventory[i]);     // REMOVE OLD ITEM
                        inventory.Add(piece2);              // ADD NEW ITEM
                        return;
                    }
                    // END OF BLOCK BREAD
                }
            }
            // End of logic for using items in inventory

            Console.WriteLine("Cannot use {0}", item);
        }

        public string Use(INames item1, INames item2)
        {
            // Check if item1/2 is door also
            bool gotMatches=false, gotTorch=false;

            // DOOR OPENER
            // This code should be able to use any key named 'key' to unlock any door
            // Either make sure the player only can carry 1 key at a time, or change this/add more code to allow multiple simultaneously key usage
            if (item1 == INames.KEY && item2 == INames.DOOR)
            {
                for (int i = 0; i < LoadGame.rooms[CurRoom].exitDoors.Count(); i++)
                {
                    if(LoadGame.rooms[CurRoom].exitDoors[i].CanBeOpenWith == INames.KEY && LoadGame.rooms[CurRoom].exitDoors[i].Status != DStatus.Open)
                    {
                        for (int inv = 0; inv < inventory.Count(); inv++)
                        {
                            if(inventory[inv].Name.ToUpper()==INames.KEY.ToString())
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

            else if (item1 == INames.MATCHES && item2 == INames.TORCH)
            {
                for (int inv = 0; inv < inventory.Count(); inv++)
                {
                    if (inventory[inv].Name.ToUpper() == INames.MATCHES.ToString())
                    {
                        inventory.Remove(inventory[inv]);
                        gotMatches = true;
                    }
                    if (inventory[inv].Name.ToUpper() == INames.TORCH.ToString())
                    {
                        inventory.Remove(inventory[inv]);
                        gotTorch = true;
                    }
                }
                if (gotMatches && gotTorch)
                {
                    var litTorch = new Item("LitTorch", "A lit torch. This should light up even the darkest of places.", INames.EMPTY, ItemPos.Room, true);
                    inventory.Add(litTorch); 
                    Console.Clear();
                    GFXText.PrintTextWithHighlights("", 2, 2, true);
                    Console.Write("\n\n");
                    return "You use the matches on the torch and now have a [LitTorch]";

                }
            }
            // END OF DOOR OPENER

            return $" return a message to the Game Handler";
        }

        // Look in the current Room
        // Look with no arguments returns both descriptions of the room instantly, and a list of current 'room inventory'
        // Does not list usable items in the room, only items the player is able to pickup
        public string Look()
        {
            Console.Clear();
            GFXText.PrintTxt(Globals.RoomNameXPos, Globals.RoomNameYPos, 0, 0, LoadGame.rooms[CurRoom].Name, false, false);
            //GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].name, Globals.RoomNameXPos, Globals.RoomNameYPos, false);
            GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].Description, Globals.RoomDescriptionXPos, Globals.RoomDescriptionYPos, false);
            GFXText.PrintTextWithHighlights(LoadGame.rooms[CurRoom].Description2, Globals.RoomDescription2XPos, Globals.RoomDescription2YPos, false);
            Console.Write("\n\n");
            int lineCheck=0;    // Linecheck is needed to break lines between found items, since PrintTextWithHighlights() is used
            for (int i = 0; i < LoadGame.rooms[CurRoom].roomItems.Count; i++)
            {
                if (LoadGame.rooms[CurRoom].roomItems[i].Pickup)
                {
                    // Message to print when an item is found ===== REVISE TEXT?
                    GFXText.PrintTextWithHighlights($"In this room there's also a [{LoadGame.rooms[CurRoom].roomItems[i].Name}].", 2, 12+lineCheck, false);
                    lineCheck++;
                }
            }
            return null;
            //return $" return a message to the Game Handler";
        }


        // Look() with arguments returns description of specific items
        public string Look(string arg1)
        {
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
                    str += m.Name + ',';

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
