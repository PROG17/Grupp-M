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
    // NOTE that you then need to update the dictionary myCmds & lists dirList, itemList in Program, 
    // the initialization of the rooms in LoadGame 

    // OLD INAMES, PROBABLY OBSOLETE BUT KEEP FOR NOW IN CASE OF BREAKING GAME
    /*public enum INames
    {
        CLUE1, CLUE2, CLUE3, KEY, AX, MAILBOX, BOTTLE, CORK, BOX, TORCH,
        NOTE, CHANDELIER, THRONE, PAINTING, EMPTY, DOOR, CHAIN, IVY, REMAINS, HAND,
        WINDOW, PANTRY, BREAD, MATCHES
    }*/

    // Remaking INames to clean up unused(?) vars
    public enum INames
    {
        KEY, TORCH, CHANDELIER, THRONE, PAINTING, EMPTY, DOOR, CHAIN, IVY, REMAINS,
        HAND, WINDOW, PANTRY, BREAD, MATCHES, BLOOD
    }

    public enum RNames { Entrance, DiningRoom, LivingRoom, Kitchen, Cellar, Bedroom, Bathroom }

    // These is the list of all possible commands

    public enum Action { GO, GET, DROP, USE, ON, LOOK, SHOW }

}