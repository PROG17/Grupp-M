using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{


    public class Item : GameObjects
    {
        public int canBeCombinedWith;    // We need to think better this field

        // Note the Item NAME might be an Enum (int) as defined in the Door Class
        // and not string 

        public Item(string name, string description) : base(name, description)
        {
            // the following are already in the base class. The Base class constructor will be called ;-)
            //this.name = name;
            //this.description = description;
        }
    }
}