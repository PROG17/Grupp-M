using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Player
    {
        public string name;
        public List<Item> Inventory = new List<Item>();
        public int position; // refers to Enum Directions in the Door class
        


        // Methods

        public void Go(int direction)
        {

        }

        public void Get (int item)
        {

        }

        public void Drop (int item)
        {


        }

        public void Use (int item, Door door)
        {

        }

        public void Use (int item1, int item2)
        {


        }

        // Look in the current Room
        public void Look()
        {


        }

        public void Inspect(int item)
        {

        }

        public void Inspect (Door door)
        {


        }


    }
}
