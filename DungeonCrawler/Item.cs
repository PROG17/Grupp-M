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

        // Note the Item NAME might be an Enum (int) as defined in the Door Class
        // and not string 

        public Item()
        {

        }

        public Item(string name, string description) : base(name, description)
        {
            // the following are already in the base class. The Base class constructor will be called ;-)
            //this.name = name;
            //this.description = description;
        }

        public Item(string name, string description, INames combWith) : base(name, description)
        {
            CombWith = combWith;
        }

        public Item(string name, string description, ItemPos location) : base(name, description)
        {
            BelongsTo = location;
        }

        public Item(string name, string description, INames combWith, ItemPos location) : base(name, description)
        {
            CombWith = combWith;
            BelongsTo = location;
        }


        // Copy constructor
        public Item(Item source)
        {
            CombWith = source.CombWith;
            BelongsTo = source.BelongsTo;
            Name = source.Name;
            Description = source.Description;
            
        }
    }
}