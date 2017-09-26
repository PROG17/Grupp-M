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
        public bool visited { get; set; }              // Keeps track if user have been in the room before
        public bool endPoint { get; set; } = false;             // Is the room the last ONE = endpoint?
        public string description2 { get; set; }                // Second "hidden" description, only visible if player use LOOK

        public Door[] exitDoors = new Door[4];          // Index is the pos in the room (North,East,..). Contains Door objects. 
        public List<Item> roomItems = new List<Item>();


        // Room constructor(s)

        public Room(string name, string description) : base(name, description)
        {
            // this.name = name;                        // Name & Description are from base class. Don´t need this part.
            // this.description = description;
            description2 = "You find nothing new in here.";
            visited = false;
        }

        public Room(string name, string description, string description2) : base(name, description)
        {
            // this.name = name;
            // this.description = description;
            this.description2 = description2;
            visited = false;
        }


    }
}