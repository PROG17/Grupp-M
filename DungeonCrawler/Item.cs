using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{


    public class Item : GameObjects
    {
        public INames CombWith { get; set; } = INames.EMPTY;    // We need to think better this field
        public ItemPos BelongsTo { get; set; } = ItemPos.NONE;  // State if the Item object belongs to room or inventory
        public bool pickUp {get;}    // Is the player able to pickup the item?

        // Note the Item NAME might be an Enum (int) as defined in the Door Class
        // and not string 

        public Item()
        {

        }

        public Item(string name, string description, bool pickUp) : base(name, description)
        {
            this.pickUp = pickUp;
            // the following are already in the base class. The Base class constructor will be called ;-)
            //this.name = name;
            //this.description = description;
        }

        public Item(string name, string description, INames combWith,bool pickUp) : base(name, description)
        {
            CombWith = combWith;
            this.pickUp = pickUp;

        }

        public Item(string name, string description, ItemPos location,bool pickUP) : base(name, description)
        {
            BelongsTo = location;
            this.pickUp = pickUp;

        }

        public Item(string name, string description, INames combWith, ItemPos location, bool pickUp) : base(name, description)
        {
            CombWith = combWith;
            BelongsTo = location;
            this.pickUp = pickUp;
        }


        // Copy constructor
        public Item(Item source)
        {
            CombWith = source.CombWith;
            BelongsTo = source.BelongsTo;
            name = source.name;
            description = source.description;
            pickUp = source.pickUp;
            
        }
    }
}