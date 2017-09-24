using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DungeonCrawler
{
    public static class Globals
    {
        public const int windowWidth = 120;
        public const int windowHeight = 30;

        //public static Dictionary<string, Item> items = new Dictionary<string, Item>();
        //public static Dictionary<string, Room> rooms = new Dictionary<string, Room>();

        public static Dictionary<INames, Item> items = new Dictionary<INames, Item>();
        public static Dictionary<RNames, Room> rooms = new Dictionary<RNames, Room>();
    }

    class DungeonCrawler
    {
        static void Main(string[] args)
        {
            // var handler = new GameHandler(); // here most of the actions from Program will be moved


            // the following should be used in the input check
            // If we use Enum then we can remove the List below!

            var dirList = new List<string>() { "NORTH", "EAST", "SOUTH", "WEST" };

            var cmdList = new List<string>() { "GO", "GET", "DROP", "USE", "ON", "LOOK", "INSPECT", "SHOW" };

            var itemList = new List<string>()
            { "clue1", "clue2", "clue3", "key", "ax", "mailbox",
                "bottle", "cork", "box", "torch", "note", "chandelier", "throne", "painting", "door"};

            // initialize the player

            // var player = new Player("Egidio", Dir.South, RNames.DiningRoom);
            
            // The handler will create the player and operate all the actions
            var handler = new GameHandler();

            Console.SetWindowSize(Globals.windowWidth, Globals.windowHeight);

            LoadGame.Load();

            // Formatting Text
            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.Entrance].Name, 5, 5, false);
            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.Entrance].Description, 10, 6, true);

            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.BathRoom].Name, 5, 10, false);
            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.BathRoom].Description, 10, 11, true);
            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.BathRoom].Description2, 10, 12, true);


            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.DiningRoom].Name, 5, 14, false);
            GFXText.PrintTextWithHighlights(Globals.rooms[RNames.DiningRoom].Description, 5, 16, true);

            //GFXText.PrintTextWithHighlights(Globals.rooms["entrance"].Name, 5, 5, false);
            //GFXText.PrintTextWithHighlights(Globals.rooms["entrance"].Description, 10, 6, true);

            //GFXText.PrintTextWithHighlights(Globals.rooms["bathroom"].Name, 5, 10, false);
            //GFXText.PrintTextWithHighlights(Globals.rooms["bathroom"].Description, 10, 11, true);
            //GFXText.PrintTextWithHighlights(Globals.rooms["bathroom"].Description2, 10, 12, true);

            string input;
            while (true)
            {
                Console.WriteLine("Digit a command or type [H] for list of commands");
                input = Console.ReadLine();
                var argums = input.Split(' ');
                switch (argums.Length)
                {
                    case 0:
                        // User does not give an input
                        Console.WriteLine("I beg your pardon?");
                        break;
                    case 1:
                        // The user has only typed a command with no enough arguments
                        Console.WriteLine("Sorry! You need to specify a valid command with actions");
                        break;
                    case 2:
                        // The user has typed a command and an argument
                        if (!cmdList.Contains(argums[0].ToUpper()))
                        {
                            // Error user has typed an invalid command
                            // repeat the while loop
                            Console.WriteLine("Unrecognized command. What do you wanna do?");
                        }
                        else
                        {
                            // Command is valid. Now I need to check the 2nd argument is an Item/Door or Direction 
                            if (!itemList.Contains(argums[1].ToLower()) && !dirList.Contains(argums[1].ToUpper()))
                            {
                                // Error with 2nd argument
                                Console.WriteLine($"Invalid use of the command {argums[0]}");
                            }
                            else
                            {
                                InvokeAction(argums);

                                // the 2nd and LAST argument is recognized. Now start the right Action!
                            }


                        }

                        // CheckFirstArg(argums[0]);




                        break;

                }




                // The Helper should be Completed and perhaps written with one only string
                // Plus it should be activated when the command is "H"
                Console.WriteLine("  --- Command List ---");
                Console.WriteLine("GO <Dir> - to Move in the direction of Dir can be East, West, South and Nord);
            }
        }

        



        }
    }
}
