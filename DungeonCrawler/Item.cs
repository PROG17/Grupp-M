// using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace DungeonCrawler
{
    // Description
    //
    // Item class describes each object which can be part of a room and that can be collected or not by the Player.
    // 
    // Item properties:
    //
    // - CombWith: It says whether the object can be combined with other objects via the USE command. By default is set to EMPTY.
    // - ItemPos : It indicates the position of the object which can be either a Room or the inventory.
    // - Pickup  : A flag which is TRUE if the object can be collected by the player and moved to the Inventory or not.
    // - IsUsed  : A flag which is TRUE if an action has been applied to it at least once.
    //
    // - Constructors: Different types, according to the properties we want to change.

    public class Item : GameObjects, IEquatable<Item>
    {
        public INames CombWith { get; set; } = INames.EMPTY;    // We need to think better this field
        public ItemPos BelongsTo { get; set; } = ItemPos.NONE;  // State if the Item object belongs to room or inventory
        public bool Pickup {get;}           // Is the player able to pickup the item?
        public bool IsUsed { get; set; }    // Is the item used? Used for fixed items in rooms to limit certain actions to 1 time

        // Note the Item NAME might be an Enum (int) as defined in the Door Class
        // and not string 

        // Constructors
        public Item()
        {

        }

        public Item(string name, string description, bool pickUp) : base(name, description)
        {
            this.Pickup = pickUp;           
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

        public bool Equals(Item other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));

        }
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}