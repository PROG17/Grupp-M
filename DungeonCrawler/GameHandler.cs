using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class GameHandler
    {
        // Create a player

        Player player = new Player("Egidio", Dir.South, RNames.DiningRoom);


        // arg[0] = Command
        // arg[1] = Direction or Item/Door
        // arg[2] = ON
        // arg[3] = Item
        public void InvokeAction(string[] arg)
        {
            switch (arg[0].ToUpper())
            {
                case nameof(Action.GO):

                    Console.WriteLine(player.Go();

                    break;

                case nameof(Action.GET):

                    break;

                case nameof(Action.DROP):

                    break;

                case nameof(Action.USE):

                    break;

                case nameof(Action.ON):

                    break;

                case nameof(Action.LOOK):

                    break;

                case nameof(Action.INSPECT):

                    break;

                case nameof(Action.SHOW):

                    break;
                default:

                    break;


            }

            // This is responsible of ALL the interactions between Program and all the other objects: room, items
            // It will initialize the game and handle all the input/outpup

        }
    }
}
