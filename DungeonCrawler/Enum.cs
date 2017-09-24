﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public enum ItemPos { Room, Inventory, NONE }

    public enum Dir { NORTH, EAST, SOUTH, WEST }
    
    // Maybe WALL to be removed
    public enum DStatus { Open, Closed, WALL }

    // These lists of names are flexible and can grow as we wish
    public enum INames { CLUE1, CLUE2, CLUE3, KEY, AX, MAILBOX, BOTTLE, CORK, BOX, TORCH, NOTE, CHANDELIER, THRONE, PAINTING, EMPTY, DOOR }
    public enum RNames { Entrance, BathRoom, DiningRoom, BedRoom }

    // These is the list of all possible commands

    public enum Action { GO, GET, DROP, USE, ON, LOOK, INSPECT, SHOW }
    
}
