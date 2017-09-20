using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Room : GameObjects
    {
        private bool visited;
        private string description2;

        public Room(string name, string description)
        {
            this.name = name;
            this.description = description;
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