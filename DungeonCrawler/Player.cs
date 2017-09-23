using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Player
    {
        private int bagSize = 4;         // Max 4 objects can be carried

        public string Name { get; set; }

        private List<Item> Inventory;                // Only Player can access its Inventory
        public Dir CurrentPos = Dir.North;           // Default Position. Refers to Enum Dir(ections) in the Enum file

        public RNames curRoom = RNames.Entrance; // Default Room. --- Maybe unnecessary

        private string msg;

        // Constructor
        public Player(string name)
        {
            var Inventory = new List<Item>(bagSize);  // Create the list, initially empty
            Name = name;
        }

        // Methods

        // Take in the Direction and returns a Message to GameHandler
        // to help the player making next move

        public string Go(Dir dir)
        {

            // Note (int) because dir is enum.
            DStatus doorStatus = Globals.rooms[curRoom].ExitDoors[(int)dir].Status;

            // Check first if I can move East (if there is a door or a wall)
            // I need the following regardless the position N,E, W,E.

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
            var roomItems = Globals.rooms[curRoom].RoomItems;

            for (int i = 0; i < roomItems.Count(); i++)
            {
                // I search for the item in the list within the current room
                if (roomItems[i].Name == item.ToString())  // item is enum! need conversion to string
                {
                    // Do I have space in the bag?
                    if (Inventory.Count() < bagSize)
                    {
                        // Update the item location in Room object. I do not touch the object in the room!

                        roomItems[i].BelongsTo = ItemPos.Inventory;

                        // I need to test which of the following works !

                        var tmp = new Item(roomItems[i].Name, roomItems[i].Description, roomItems[i].CombWith);
                        var tmp1 = new Item(roomItems[i]);   // Using the copy constructor


                        Inventory.Add(tmp1);              // Can ADD the item from room to Player Inventory
                        roomItems.Remove(roomItems[i]);   // Now I can finally remove the Object from Room List

                        return $"You have just collected [{tmp1.Name}] and now you have {Inventory.Count()} objects of {bagSize} in your bag";
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

        public void Drop(int item)
        {


        }

        public void Use(int item, Door door)
        {

        }

        public void Use(int item1, int item2)
        {


        }

        // Look in the current Room
        public void Look()
        {


        }

        public void Inspect(int item)
        {

        }

        public void Inspect(Door door)
        {


        }


    }
}
