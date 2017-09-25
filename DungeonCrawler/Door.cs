using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    // Moved the Enum data in Enum file

    public class Door
    {
       // public int Position { get; set; } = 0;                // the position of the door only possible 4 values (0 to 3). Default = 0 = North
        public DStatus status { get; set; } = DStatus.WALL;     // Only possible 3 values 0 = open, 1 = closed, 2 = wall
        public INames canBeOpenWith { get; set; } = INames.KEY; // Default = 0 = Key
        public RNames leadsToRoom { get; }

        public string Description { get; set; }                 // A door show a description when INSPECTED

        // Constructor
        // the values in the param list must be constrained to the enum above

        public Door(DStatus stat, INames opener, RNames leadsToRoom)
        {
            // Position = pos;
            status = stat;
            canBeOpenWith = opener;
            this.leadsToRoom = leadsToRoom;
        }

        

    }
}
