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
       // public int Position { get; set; } = 0;               // the position of the door only possible 4 values (0 to 3). Default = 0 = North
        public DStatus Status { get; set; } = DStatus.WALL;    // Only possible 3 values 0 = open, 1 = closed, 2 = wall
        public int CanBeOpenWith { get;  set; }                // Default = 0 = Key 

        // Constructor
        // the values in the param list must be constrained to the enum above

        public Door(int pos, int stat, int open)
        {
            Position = pos;
            Status = stat;
            CanBeOpenWith = open;
        }

        

    }
}
