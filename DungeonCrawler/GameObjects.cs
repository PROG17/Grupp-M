using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class GameObjects
    {
        protected string name, description;

        public GameObjects()
        {
            this.name = "Unnamed";
            this.description = "No description";
        }

        public GameObjects(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

        }
        public string Description
        {
            get
            {
                return this.description;
            }
        }
    }
}
