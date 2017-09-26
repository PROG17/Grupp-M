using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Room : GameObjects
    {
        // private bool visited;
        // private string description2;

        // Properties
        public bool Visited { get; set; }              // Keeps track if user have been in the room before
        public bool EndPoint { get; set; } = false;             // Is the room the last ONE = endpoint?
        public string Description2 { get; set; }                // Second "hidden" description, only visible if player use LOOK

        public Door[] exitDoors = new Door[4];          // Index is the pos in the room (North,East,..). Contains Door objects. 
        public List<Item> roomItems = new List<Item>();


        // Room constructor(s)

        public Room(string name, string description) : base(name, description)
        {
            // this.name = name;                        // Name & Description are from base class. Don´t need this part.
            // this.description = description;
            Description2 = "You find nothing new in here.";
            Visited = false;
        }

        public Room(string name, string description, string description2) : base(name, description)
        {
            // this.name = name;
            // this.description = description;
            this.Description2 = description2;
            Visited = false;
        }


    }
}