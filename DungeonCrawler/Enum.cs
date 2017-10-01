using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    // Enum constats used by most of the classes.

    public enum ItemPos { Room, Inventory, NONE }

    public enum Dir { NORTH, EAST, SOUTH, WEST }

    // Maybe WALL to be removed
    public enum DStatus { Open, Closed, WALL }

    // Remaking INames to clean up unused(?) vars
    public enum INames
    {
        KEY,NOTE, FIREPLACE, BRONZEPIECE , MATCHES, TORCH, CHANDELIER, THRONE, PAINTING, EMPTY, DOOR, CHAIN, IVY, REMAINS,
        HAND, WINDOW, PANTRY, BREAD, BLOOD, FLAMINGTORCH, BRAZIER, LOCKER, BED, SILVERPIECE, GOLDPIECE, MEDALLION, PANEL
    }

    public enum RNames { Entrance, DiningRoom, LivingRoom, Kitchen, Cellar, Bedroom, Bathroom, Endroom }

    // These is the list of all possible commands

    public enum Action { GO, GET, DROP, USE, ON, LOOK, SHOW }

}