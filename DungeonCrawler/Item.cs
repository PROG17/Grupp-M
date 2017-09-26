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
        public bool Pickup {get;}           // Is the player able to pickup the item?
        public bool IsUsed { get; set; }    // Is the item used? Used for fixed items in rooms to limit certain actions to 1 time

        // Note the Item NAME might be an Enum (int) as defined in the Door Class
        // and not string 

        public Item()
        {

        }

        public Item(string name, string description, bool pickUp) : base(name, description)
        {
            this.Pickup = pickUp;
            // the following are already in the base class. The Base class constructor will be called ;-)
            //this.name = name;
            //this.description = description;
        }

        public Item(string name, string description, INames combWith,bool pickUp) : base(name, description)
        {
            this.CombWith = combWith;
            this.Pickup = pickUp;

        }

        public Item(string name, string description, ItemPos location,bool pickUp) : base(name, description)
        {
            BelongsTo = location;
            this.Pickup = pickUp;

        }

        public Item(string name, string description, INames combWith, ItemPos location, bool pickUp) : base(name, description)
        {
            this.CombWith = combWith;
            BelongsTo = location;
            this.Pickup = pickUp;
        }


        // Copy constructor
        public Item(Item source)
        {
            CombWith = source.CombWith;
            BelongsTo = source.BelongsTo;
            Name = source.Name;
            Description = source.Description;
            Pickup = source.Pickup;
            
        }
    }
}