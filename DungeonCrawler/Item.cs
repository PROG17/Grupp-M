using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Item : GameObjects
    {
        public Item(string name,string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}