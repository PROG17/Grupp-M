using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public enum Dir { North, East, South, West }
    
    // Maybe WALL to be removed
    public enum DStatus { Open, Closed, WALL }

    // These lists of names are flexible and can grow as we wish
    public enum INames { clue1, clue2, clue3, Key, Ax, Mailbox, Bottle, Cork, Box, Torch, Note, Chandelier, Throne, EMPTY }
    public enum RNames { Entrance, BathRoom, DiningRoom, BedRoom }
}
