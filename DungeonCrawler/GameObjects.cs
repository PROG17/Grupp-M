using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class GameObjects
    {
        // Properties (public)
        public string name { get; set; }
        public string description { get; set; }

        // Constructor(s)

        public GameObjects()
        {
            name = "Unnamed";
            description = "No description";
        }

        public GameObjects(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

    }
}