// using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    // This class is responsible of ALL the interactions between Program and all the other objects: room, items
    // It will initialize the game and handle all the input/outpup
    class GameHandler
    {
        // Create a player

        Player player;

        // Set the name of the player
        public void InitPlayer(string name)
        {
            // I need to initialize the inventory HERE otherwise it won´t be
            // visible here and VS will complain in Player Class
            // of undefined Inventory !

            var inventory = new List<Item>();

           // player = new Player(name, Dir.SOUTH, RNames.Entrance, inventory);
            player = new Player(name, RNames.Entrance, inventory);
        }




        // arg[0] = Command
        // arg[1] = Direction or Item/Door
        // arg[2] = ON
        // arg[3] = Item
        public void InvokeAction(string[] arg)
        {
            switch (arg[0].ToUpper())
            {
                case nameof(Action.GO): // Tested Ok                    // I need to retrieve the matching Enum value for arg[1]
                    Dir newDir;
                    if (arg[1].ToUpper() == "LEFT") newDir = (Dir)Enum.Parse(typeof(Dir), "WEST");                    
                    else if (arg[1].ToUpper() == "RIGHT") newDir = (Dir)Enum.Parse(typeof(Dir), "EAST");
                    else if (arg[1].ToUpper() == "FORWARD") newDir = (Dir)Enum.Parse(typeof(Dir), "NORTH");
                    else if (arg[1].ToUpper() == "BACK") newDir = (Dir)Enum.Parse(typeof(Dir), "SOUTH");
                    else newDir = (Dir)Enum.Parse(typeof(Dir), arg[1].ToUpper());
                    Console.WriteLine(player.Go(newDir));

                    break;

                case nameof(Action.GET): // TESTED Ok

                    // I need to retrieve the matching Enum value for arg[1]
                    INames getItem = (INames)Enum.Parse(typeof(INames), arg[1].ToUpper());
                    Console.Clear();
                    GFXText.PrintTextWithHighlights(player.Get(getItem), 1, 1, false);
                    Console.Write("\n\n");
                    //Console.WriteLine(player.Get(getItem));
                
                    break;

                case nameof(Action.DROP): // Tested Ok

                    // I need to retrieve the matching Enum value for arg[1]
                    INames dropItem = (INames)Enum.Parse(typeof(INames), arg[1].ToUpper());

                    // The Drop methods will do all the checks and return the output messages
                    Console.WriteLine(player.Drop(dropItem));
                    
                    break;

                case nameof(Action.USE):
                    // I need to retrieve the matching Enum value for arg[1]
                    // USE with 1 more arg equals the player is trying to use an item by itself
                    if (arg.Length == 2)
                    {
                        INames useItem1 = (INames)Enum.Parse(typeof(INames), arg[1].ToUpper());
                        player.Use(useItem1);
                    }
                    // USE with more args equals the player is trying to use an item with another item
                    else
                    {
                        INames useItem1 = (INames)Enum.Parse(typeof(INames), arg[1].ToUpper());
                        INames onItem2 = (INames)Enum.Parse(typeof(INames), arg[3].ToUpper());
                        Console.WriteLine(player.Use(useItem1, onItem2));
                    }
                    break;

                case nameof(Action.ON):
                    // I do not need this because it is part of the command USE...ON...
                    break;

                case nameof(Action.LOOK):
                    Console.Clear();
                    if (arg.Length == 1) player.Look();
                    else
                    {   
                        GFXText.PrintTextWithHighlights(player.Look(arg[1]), 1, 2, false);
                        Console.Write("\n\n");
                    }
                    break;


                // Removed Inspect()
                /*case nameof(Action.INSPECT):

                    INames inspItem = (INames)Enum.Parse(typeof(INames).Name, arg[1].ToUpper());
                    player.Inspect(inspItem);

                    break;*/

                case nameof(Action.SHOW): // Tested Ok

                    Console.WriteLine(player.Show());
                    break;
                default:

                    break;


            }


        }
    }
}
