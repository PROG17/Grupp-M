using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Room : GameObjects
    {
        // Room properties

        private bool visited;
        private string description2;

        public Door [] ExitDoors = new Door[4]; // Each row is the pos in the room (North,East,..). If no door ==> null object
        
        public bool EndPoint { get; set; }              // Is the room the last ONE = endpoint?
        public List<Item> roomItems = new List<Item>(); 


        public Room(string name, string description) : base(name, description)
        {
            // this.name = name;                     // Name & Description are from base class. Don´t need this.
            // this.description = description;
            this.description2 = "You find nothing new in here.";
            visited = false;
        }

        public Room(string name, string description, string description2)
        {
            this.name = name;
            this.description = description;
            this.description2 = description2;
            visited = false;
        }

        public string Description2
        {
            get
            {
                return this.description2;
            }
        }

        public bool Visited
        {
            get
            {
                return visited;
            }
            set
            {
                visited = value;
            }
        }
    }
}