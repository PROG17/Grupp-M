// using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    // Description 
    // 
    // Door class contains:
    //
    // - Status         : (Enum) Open, closed or wall
    // - CanBeOpenWith  : (Enjum) Equal to INames.Key if can be open with a key
    // - LeadsToRoom    : (Enum) Set to the name of the room leading from this door
    // - Description    : Optional description of the door
    // 
    // - Constructors   : 2 are available.




namespace DungeonCrawler
{
    // Moved the Enum data in Enum file

    public class Door
    {
       // public int Position { get; set; } = 0;                // the position of the door only possible 4 values (0 to 3). Default = 0 = North
        public DStatus Status { get; set; } = DStatus.WALL;     // Only possible 3 values 0 = open, 1 = closed, 2 = wall
        public INames CanBeOpenWith { get; set; } = INames.KEY; // Default = 0 = Key
        public RNames LeadsToRoom { get; }                      // Used to decide what room the player will come to if leaving through this door

        // Is this needed?
        public string Description { get; set; }                 // A door show a description when INSPECTED

        // Constructor
        // the values in the param list must be constrained to the enum above

        public Door(DStatus stat, INames opener)
        {
            // Position = pos;
            Status = stat;
            CanBeOpenWith = opener;
        }

        public Door(DStatus stat, INames opener, RNames leadsToRoom)
        {
            // Position = pos;
            Status = stat;
            CanBeOpenWith = opener;
            this.LeadsToRoom = leadsToRoom;
        }
    }
}
