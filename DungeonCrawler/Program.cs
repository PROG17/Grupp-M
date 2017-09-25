using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DungeonCrawler
{    
    class DungeonCrawler
    {
        static void Main(string[] args)
        {            
            // the following are used in the input check
            
            // var cmdList = new List<string>() { "GO", "GET", "DROP", "USE", "ON", "LOOK", "INSPECT", "SHOW" };

            var dirList = new List<string>() { "NORTH", "EAST", "SOUTH", "WEST" };
            var itemList = new List<string>() { "CLUE1", "CLUE2", "CLUE3", "KEY", "AX", "MAILBOX",
                "BOTTLE", "CORK", "BOX", "TORCH", "NOTE", "CHANDELIER", "THRONE", "PAINTING", "DOOR"};

            // I can search in this dictionary when parsing the input
            Dictionary<Action, List<string>> myCmds = new Dictionary<Action, List<string>>()
                                  { { Action.GO, dirList },
                                    { Action.GET, itemList },
                                    { Action.DROP, itemList },
                                    { Action.USE, itemList },
                                    { Action.ON, itemList },
                                    { Action.LOOK, itemList },
                                    { Action.INSPECT, itemList },
                                    { Action.SHOW, itemList } };
            
            // Load the Game and create Rooms, items, etc.
            LoadGame.Init();
            
            // The handler will create the player and operate all the actions
            var handler = new GameHandler();

            Console.WriteLine("\nPlease digit your name:");
            string playName = Console.ReadLine();
            handler.InitPlayer(playName);

            while (true)
            {
                Console.WriteLine("\nDigit a command or type [H] for list of commands");
                string input = Console.ReadLine();
                var argums = input.Split(' ');


                switch (argums.Length)
                {
                    case 1:
                        // The user has only typed a command with no enough arguments
                        // Or he has typed H-for Help
                        if (argums[0].ToUpper() == "H")
                        {
                            // Show help text
                            Console.WriteLine("  --- Command List ---");
                            Console.WriteLine("GO <Dir> - to Move in the direction of Dir can be East, West, South and Nord" +
                                            "\nGET object - to collect an object and move it in the rucksack" +
                                            "\nDROP object - to leave an object from the rucksack in the current room" +
                                            "\nUSE own_object ON rum_object - to apply an action with objects" +       // bad text, revise
                                            "\nLOOK - List all objects and Exits in the room" +
                                            "\nINSPECT object/Door - show a description of the object/door" +
                                            "\nSHOW - List the objects in the rucksack" +
                                            "\nQ or Quit - to Quit the game");
                        }
                        else if (argums[0].ToUpper() == "Q" || argums[0].ToUpper() == "QUIT")
                        {
                            Console.WriteLine($"Thanks for playing {playName}! Welcome back!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("I beg your pardon? ");
                        }
                        break;
                    case 2:
                        // The user has typed a command and an argument
                        // Note: convert the String in the mapping Enum of type Action

                        Action aKey = (Action)Enum.Parse(typeof(Action), argums[0].ToUpper());
                        // if (!cmdList.Contains(argums[0].ToUpper()))

                        if (!myCmds.ContainsKey(aKey) || !myCmds[aKey].Contains(argums[1].ToUpper()))
                        {
                            // Error user has typed an invalid command
                            // repeat the while loop
                            Console.WriteLine("Unrecognized command. What do you wanna do?");
                        }
                        else
                        {
                            // Command + dir/item are valid. Due to the Dict check, I know for sure that 
                            // if arg0 is GO/Other cmd then arg1 is a direction respective item/door 

                            // the 2nd and LAST argument is recognized. Now start the right Action!
                            handler.InvokeAction(argums);
                        }
                        break;

                    case 3:
                        // The user has typed a command with 3 words. Invalid!
                        Console.WriteLine("Unrecognized command. What do you wanna do?");
                        break;

                    case 4:
                        // the user has typed a command with 4 words. The only command which is valid
                        // is: USE item1 ON item2/door

                        Action aKey1 = (Action)Enum.Parse(typeof(Action), argums[0].ToUpper());

                        if (aKey1 != Action.USE || argums[2].ToUpper() != "ON" ||
                            !myCmds[aKey1].Contains(argums[1].ToLower()) || !myCmds[aKey1].Contains(argums[3].ToLower()))
                        {
                            // Error user has typed an invalid command
                            // repeat the while loop
                            Console.WriteLine("Unrecognized command. What do you wanna do?");
                        }
                        else
                        {
                            // The user has typed exactly USE  item1 ON item2/door 
                            handler.InvokeAction(argums);
                        }
                        break;
                    default:
                        Console.WriteLine("Unrecognized command. What do you wanna do?");
                        break;
                }



            }

        }
    }





}


