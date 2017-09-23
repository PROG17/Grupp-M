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
            string msg;

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



        public void Get(INames item)
        {




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
