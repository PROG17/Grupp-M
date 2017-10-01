using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// ONLY TESTING (Egidio).. Do not use it for now. (ie. do not do using Common ;-)
// Some bugs to fix

// Common Namespace is currently NOT used!
// 
namespace Common
{

    // Global class to use certain, very limited, variables across the program
    public class Globals
    {
        public const int RoomNameXPos = -1, RoomNameYPos = 2;
        public const int RoomDescriptionXPos = 1, RoomDescriptionYPos = 5;
        public const int RoomDescription2XPos = 2, RoomDescription2YPos = 10;
        public const int TextDelay = 0, TextTrail = 0;  // Set to 0 for testing purposes, otherwise Delay 20, Trail -5?
        public const int SleepTime = 3000;                 // Set to 0 for testing purposes, otherwise 2000? 3000?
    }

    // Common constant structure HERE for easy upgrades
    //
    public enum ItemPos { Room, Inventory, NONE }

    public enum Dir { NORTH, EAST, SOUTH, WEST }

    // Maybe WALL to be removed
    public enum DStatus { Open, Closed, WALL }

    // These lists of names are flexible and can grow as we wish
    // NOTE that you then need to update the dictionary myCmds & lists dirList, itemList in Program, 
    // the initialization of the rooms in LoadGame 

    public enum INames
    {
        CLUE1, CLUE2, CLUE3, KEY, AX, MAILBOX, BOTTLE, CORK, BOX, TORCH,
        NOTE, CHANDELIER, THRONE, PAINTING, EMPTY, DOOR, CHAIN, IVY, REMAINS, HAND,
        WINDOW, PANTRY, BREAD, MATCHES
    }

    public enum RNames { Entrance, DiningRoom, LivingRoom, Kitchen, Cellar, Bedroom, Bathroom }

    // These is the list of all possible commands

    public enum Move { GO, GET, DROP, USE, ON, LOOK, SHOW }

}