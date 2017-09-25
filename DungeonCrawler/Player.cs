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
        public Dir CurrentPos { get; set; } = Dir.NORTH;          // Default Position. Refers to Enum Dir(ections) in the Enum file
        public RNames CurRoom { get; set; } = RNames.Entrance;    // Default Room. --- Maybe unnecessary

        private int bagSize = 4;         // Max 4 objects can be carried

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

        public Player(string name, Dir curPos, RNames curRoom, List<Item> inventory)
        {
            this.inventory = inventory;

            // reset the Inventory with some EMPTY items
            //for (int i=0; i < bagSize; i++)
            //{
            //    inventory.Add(new Item("EMPTY", "", INames.EMPTY, ItemPos.NONE));
            //}

            Name = name;
            CurrentPos = curPos;
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
            DStatus doorStatus = LoadGame.rooms[CurRoom].ExitDoors[(int)dir].Status;

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
                msg = "You find a door which is Open towards next room. What would you like to do?";
            }
            return msg;
        }



        public string Get(INames item)
        {
            var roomItems = LoadGame.rooms[CurRoom].RoomItems;

            for (int i = 0; i < roomItems.Count(); i++)
            {
                // I search for the item in the list within the current room
                if (roomItems[i].Name.ToUpper() == item.ToString())  // item is enum! need conversion to string
                {
                    // Do I have space in the bag?
                    if (inventory.Count() < bagSize && inventory.Count() >= 0 )
                    {
                        // Update the item location in Room object. I do not touch the object in the room!

                        roomItems[i].BelongsTo = ItemPos.Inventory;

                        // I need to test which of the following works !

                        var tmp = new Item(roomItems[i].Name, roomItems[i].Description, roomItems[i].CombWith, roomItems[i].BelongsTo);
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
            }
            // The User tries to GET an object which is NOT in the room
            return $"The {item} is not in this room. Please try again";

        }

        public string Drop(INames item)
        {
            // need to check if the item is in my bag first
            // then I can do all the actions and messages


            return $" return a message to the Game Handler";
        }

        // Use() is overloaded
        //public void Use(INames item, Door door)
        //{

        //}

        public string Use(INames item1, INames item2)
        {
            // Check if item1/2 is door also

            return $" return a message to the Game Handler";
        }

        // Look in the current Room
        public string Look()
        {

            return $" return a message to the Game Handler";
        }

        public string Inspect(INames item)
        {
            // Here I need to check first if it is a door
            // or a item to be inspected. 

            return $" return a message to the Game Handler";
        }

        //public void Inspect(Door door)
        //{
        //}

        // Show Inventory
        public string Show()
        {

            return $" return a message to the Game Handler";
        }



    }
}
