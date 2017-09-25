﻿using System;
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

            player = new Player(name, Dir.SOUTH, RNames.DiningRoom, inventory);
        }




        // arg[0] = Command
        // arg[1] = Direction or Item/Door
        // arg[2] = ON
        // arg[3] = Item
        public void InvokeAction(string[] arg)
        {
            switch (arg[0].ToUpper())
            {
                case nameof(Action.GO): // Tested Ok

                    // I need to retrieve the matching Enum value for arg[1]
                    Dir newDir = (Dir)Enum.Parse(typeof(Dir), arg[1].ToUpper());
                    Console.WriteLine(player.Go(newDir));

                    break;

                case nameof(Action.GET): // TESTED Ok

                    // I need to retrieve the matching Enum value for arg[1]
                    INames getItem = (INames)Enum.Parse(typeof(INames), arg[1].ToUpper());
                    Console.WriteLine(player.Get(getItem));
                
                    break;

                case nameof(Action.DROP): // Tested Ok

                    // I need to retrieve the matching Enum value for arg[1]
                    INames dropItem = (INames)Enum.Parse(typeof(INames), arg[1].ToUpper());

                    // The Drop methods will do all the checks and return the output messages
                    Console.WriteLine(player.Drop(dropItem));
                    
                    break;

                case nameof(Action.USE):
                    // I need to retrieve the matching Enum value for arg[1]
                    INames useItem1 = (INames)Enum.Parse(typeof(INames), arg[1].ToUpper());
                    INames onItem2 = (INames)Enum.Parse(typeof(INames), arg[3].ToUpper());

                    player.Use(useItem1, onItem2);


                    break;

                case nameof(Action.ON):
                    // I do not need this because it is part of the command USE...ON...
                    break;

                case nameof(Action.LOOK):

                    player.Look();
                    break;

                case nameof(Action.INSPECT):

                    INames inspItem = (INames)Enum.Parse(typeof(INames), arg[1].ToUpper());
                    player.Inspect(inspItem);

                    break;

                case nameof(Action.SHOW): // Tested Ok

                    Console.WriteLine(player.Show());
                    break;
                default:

                    break;


            }


        }
    }
}
