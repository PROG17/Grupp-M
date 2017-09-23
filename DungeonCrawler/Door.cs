using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    // *** these Enum will be put on a separate file

    public enum Directions { North, East, South, West }   
    public enum DoorStatus { Open, Closed }
    public enum ItemNames { Key, Ax }   // This list of names is flexible and can grow as we wish
    
    // ******************************************

    public class Door
    {
        public int Position { get; set; } = 0;     // the position of the door only possible 4 values (0 to 3). Default = 0 = North
        public int Status { get; set; } = 1;       // Only possible 2 values 0 to 1. Default = 1 = Closed
        public int CanBeOpenWith { get;  set; }    // Default = 0 = Key 

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
