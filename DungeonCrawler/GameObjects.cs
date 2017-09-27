using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public abstract class GameObjects
    {
        // Properties (public)
        public string Name { get; set; }
        public string Description { get; set; }

        // Constructor(s)

        public GameObjects()
        {
            Name = "Unnamed";
            Description = "No description";
        }

        public GameObjects(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

    }
}