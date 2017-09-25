using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Player
    {
        public string name { get; set; }
        public Dir currentPos { get; set; } = Dir.NORTH;          // Default Position. Refers to Enum Dir(ections) in the Enum file
        public RNames curRoom { get; set; } = RNames.Entrance;    // Default Room. --- Maybe unnecessary

        private int bagSize = 4;               // Max objects that can be carried

        //public List<Item> Inventory = new List<Item>(1)
        //        {new Item("EMPTY", "", INames.EMPTY, ItemPos.NONE) };   // Only Player can access its Inventory

        public List<Item> inventory;



        private string msg;

        // Constructor
        public Player(string name)
        {
            var inventory = new List<Item>(bagSize);
            this.name = name;
        }

        public Player(string name, Dir curPos, RNames curRoom, List<Item> inventory)
        {
            this.inventory = inventory;

            // reset the Inventory with some EMPTY items
            //for (int i=0; i < bagSize; i++)
            //{
            //    inventory.Add(new Item("EMPTY", "", INames.EMPTY, ItemPos.NONE));
            //}

            this.name = name;
            currentPos = curPos;
            this.curRoom = curRoom;
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
            DStatus doorStatus = LoadGame.rooms[curRoom].exitDoors[(int)dir].status;

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
            else
            {
                curRoom = LoadGame.rooms[curRoom].exitDoors[(int)dir].leadsToRoom;
                if(LoadGame.rooms[curRoom].visited)
                    GFXText.PrintTextWithHighlights(LoadGame.rooms[curRoom].description,1,1,false);
                else
                    GFXText.PrintTextWithHighlights(LoadGame.rooms[curRoom].description, 1, 1, true);
                LoadGame.rooms[curRoom].visited = true;
                return "";
            }
            return msg;
        }



        public string Get(INames item)
        {
            var roomItems = LoadGame.rooms[curRoom].roomItems;

            for (int i = 0; i < roomItems.Count(); i++)
            {
                // I search for the item in the list within the current room
                if (roomItems[i].name.ToUpper() == item.ToString() && roomItems[i].pickUp)  // item is enum! need conversion to string
                {
                    // Do I have space in the bag?
                    if (inventory.Count() < bagSize && inventory.Count() >= 0)
                    {
                        // Update the item location in Room object. I do not touch the object in the room!

                        roomItems[i].belongsTo = ItemPos.Inventory;

                        // I need to test which of the following works !
                        // Both following ways are OK. tested.
                        // Commented out tmp

                        // var tmp = new Item(roomItems[i].Name, roomItems[i].Description, roomItems[i].CombWith, roomItems[i].BelongsTo);
                        var tmp1 = new Item(roomItems[i]);   // Using the copy constructor                        
                        inventory.Add(tmp1);              // Can ADD the item from room to Player Inventory
                        roomItems.Remove(roomItems[i]);   // Now I can finally remove the Object from Room List

                        return $"You have just collected [{tmp1.name}] and now you have {inventory.Count()} objects of {bagSize} in your bag";
                    }
                    else
                    {
                        return $"Sorry! Your bag is Full and you have {bagSize} objects";
                    }
                }
                else if (roomItems[i].name.ToUpper() == item.ToString() && !roomItems[i].pickUp)        // item exists but cannot be picked up
                    return $"Cannot pickup {item}.";
            }
            // The User tries to GET an object which is NOT in the room
            return $"The {item} is not in this room. Please try again";

        }

        public string Drop(INames item)
        {
            // need to check if the item is in my bag first
            // then I can do all the actions and messages
            var roomItems = LoadGame.rooms[curRoom].roomItems;  // just to make the names shorter in the code..

            Item itemInBag = new Item();
            bool found = false;
            for (int i = 0; i < inventory.Count(); i++)
            {
                if (inventory[i].name.ToUpper() == item.ToString())
                {
                    found = true;
                    itemInBag = inventory[i];
                    break;
                }
            }
            if (found)
            {
                // need to move the item from inventory in the room
                itemInBag.belongsTo = ItemPos.Room;

                var tmp1 = new Item(itemInBag);   // Using the copy constructor 
                roomItems.Add(tmp1);
                inventory.Remove(itemInBag);

                return $"You have just dropped [{tmp1.name}] and now you have {inventory.Count()} objects in your backpack";
            }
            else
            {
                return $"I am sorry but you don't have {item.ToString()} in your backpack";
            }
        }

        // Use() is overloaded
        public void Use(INames item)
        {
            var roomItems = LoadGame.rooms[curRoom].roomItems;
            for (int i = 0; i < roomItems.Count(); i++)
            {
                if (roomItems[i].name.ToUpper() == item.ToString() && !roomItems[i].isUsed)
                {
                    roomItems[i].isUsed = true;
                    if (roomItems[i].name.ToUpper() == "CHAIN")
                    {
                        var key = new Item("Key", "key description", INames.EMPTY, ItemPos.Room, true);
                        roomItems.Add(key);
                        Console.Clear();
                        GFXText.PrintTextWithHighlights("A [key] falls out.", 1, 1, true);
                        Console.Write("\n\n");
                        return;
                    }
                }
                else if (roomItems[i].name.ToUpper() == item.ToString() && roomItems[i].isUsed)
                {
                    Console.WriteLine("\n{0} already used...", item);
                    return;
                }
            }
            Console.WriteLine("Cannot use {0}", item);
        }

        public string Use(INames item1, INames item2)
        {
            // Check if item1/2 is door also

            return $" return a message to the Game Handler";
        }

        // Look in the current Room
        public string Look()
        {
            Console.Clear();
            GFXText.PrintTextWithHighlights(LoadGame.rooms[curRoom].description, 1, 1, false);
            GFXText.PrintTextWithHighlights(LoadGame.rooms[curRoom].description2, 1, 5, false);
            Console.Write("\n\n");

            return null;
            //return $" return a message to the Game Handler";
        }

        public string Look(string arg1)
        {
            // Look through items in the current room, return its description
            var roomItems = LoadGame.rooms[curRoom].roomItems;
            for (int i = 0; i < roomItems.Count(); i++)
            {
                if (roomItems[i].name.ToUpper() == arg1.ToUpper())
                {
                    return roomItems[i].description;
                }
            }

            // Look through items in inventory, return its description
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].name.ToUpper() == arg1.ToUpper())
                    return inventory[i].description;
            }
            return null;
        }

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
                    str += m.name + ',';

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
