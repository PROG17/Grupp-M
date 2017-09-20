using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Item
    {
        private static int idCount=0;
        private int id, value;
        private string name, description;

        public Item()
        {
            idCount++;
            this.id = idCount;
            this.value = -1;
            this.name = "Unnamed";
            this.description = "No description";
        }

        public Item(int value, string name, string description)
        {
            idCount++;
            this.id = idCount;
            this.value = value;
            this.name = name;
            this.description = description;
        }

        public int Id
        {
            get
            {
                return this.id;
            }

        }
        public int Value
        {
            get
            {
                return this.value;
            }

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
