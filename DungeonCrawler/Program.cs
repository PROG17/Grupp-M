using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DungeonCrawler
{
    // Global class to use certain, very limited, variables across the program
    public class Globals
    {
        public const int RoomNameXPos = -1, RoomNameYPos = 2;
        public const int RoomDescriptionXPos = 1, RoomDescriptionYPos = 5;
        public const int RoomDescription2XPos = 2, RoomDescription2YPos = 10;
    }

    // "Real" class starts here
    class DungeonCrawler
    {
        static void Main(string[] args)
        {
            string tmpstring="";    //used to fix user input
            // the following are used in the input check
            
            var cmdList = new List<string>() { "GO", "GET", "DROP", "USE", "ON", "LOOK", "SHOW" };

            var dirList = new List<string>() { "FORWARD", "BACK", "LEFT", "RIGHT", "NORTH", "EAST", "SOUTH", "WEST" };
            var itemList = new List<string>() { "CLUE1", "CLUE2", "CLUE3", "KEY", "AX", "MAILBOX",
                "BOTTLE", "CORK", "BOX", "TORCH", "NOTE", "CHANDELIER", "THRONE", "PAINTING", "DOOR","CHAIN","IVY"};

            // I can search in this dictionary when parsing the input
            Dictionary<Action, List<string>> myCmds = new Dictionary<Action, List<string>>()
                                  { { Action.GO, dirList },
                                    { Action.GET, itemList },
                                    { Action.DROP, itemList },
                                    { Action.USE, itemList },
                                    { Action.ON, itemList },
                                    { Action.LOOK, itemList },
                                    //{ Action.INSPECT, itemList },
                                    { Action.SHOW, itemList } };
            
            // Load the Game and create Rooms, items, etc.
            LoadGame.Init();
            
            // The handler will create the player and operate all the actions
            var handler = new GameHandler();


            // Welcome message and first room description
            Console.Write("\nWhat is your name, traveller? ");
            string playName = Console.ReadLine();
            string input = "";

            handler.InitPlayer(playName);

            Console.WriteLine("Welcome, {0}! A world of adventure awaits you!\n\n", playName);
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            GFXText.PrintTxt(Globals.RoomNameXPos, Globals.RoomNameYPos, -5, 20, LoadGame.rooms[RNames.Entrance].name, false, false);
            //GFXText.PrintTextWithHighlights(LoadGame.rooms[RNames.Entrance].name, Globals.RoomNameXPos, Globals.RoomNameYPos, true);
            GFXText.PrintTextWithHighlights(LoadGame.rooms[RNames.Entrance].description,Globals.RoomDescriptionXPos,Globals.RoomDescriptionYPos,true);
            Console.Write("\n\n");
            LoadGame.rooms[RNames.Entrance].visited = true;
            // end of description

            while (true)
            {
                input = "";
                Console.WriteLine("\nEnter a command or type [H] for list of commands");
                while(input == "") 
                input = Console.ReadLine();

                // fix for program crash if command starts with a single space
                while (input[0] == ' ')
                {
                    for (int i = 1; i < input.Length; i++)
                    {
                        tmpstring += input[i].ToString();
                    }
                    input = tmpstring;
                    tmpstring = "";
                }                
                // end fix

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
                            Console.WriteLine("GO <Dir> - to Move in the direction of Dir can be FORWARD, BACK, LEFT, RIGHT" +
                                            "\nGET item - to collect an object and move it in the backpack" +
                                            "\nDROP item - to leave an object from the backpack in the current room" +
                                            "\nUSE an item, either just the object itself or ON another item" +       // bad text, revise. revised/t
                                            "\nLOOK - List all objects and Exits in the room" +
                                            //"\nINSPECT object/Door - show a description of the object/door" +
                                            "\nSHOW - List the objects in the backpack" +
                                            "\nQ or Quit - to Quit the game");
                        }
                        else if (argums[0].ToUpper() == "Q" || argums[0].ToUpper() == "QUIT")
                        {
                            Console.WriteLine($"Thanks for playing {playName}! Welcome back!");
                            return;
                        }
                        else if (argums[0].ToUpper() == "SHOW")
                        {
                            handler.InvokeAction(argums);
                        }
                        else if (argums[0].ToUpper() == "LOOK")
                        {
                            handler.InvokeAction(argums);
                        }
                        else
                        {
                            Console.WriteLine("I beg your pardon? ");
                        }
                        break;
                    case 2:
                        // The user has typed a command and an argument
                        // Note: convert the String in the mapping Enum of type Action

                        // Currently needed to make sure program does not crash while trying to parse Enum that does not exist
                        if (!cmdList.Contains(argums[0].ToUpper()))
                        {
                            Console.WriteLine("Invalid command.");
                            break;
                        }

                        Action aKey = (Action)Enum.Parse(typeof(Action), argums[0].ToUpper());
                        // if (!cmdList.Contains(argums[0].ToUpper()))

                        if (!myCmds.ContainsKey(aKey) || !myCmds[aKey].Contains(argums[1].ToUpper()))
                        {
                            // Error user has typed an invalid command
                            // repeat the while loop
                            Console.WriteLine("case 2 Unrecognized command. What do you wanna do?");
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
                        Console.WriteLine("case 3 Unrecognized command. What do you wanna do?");
                        break;

                    case 4:
                        // the user has typed a command with 4 words. The only command which is valid
                        // is: USE item1 ON item2/door
                        if (!cmdList.Contains(argums[0].ToUpper()))
                        {
                            Console.WriteLine("Invalid command.");
                            break;
                        }

                        Action aKey1 = (Action)Enum.Parse(typeof(Action), argums[0].ToUpper());

                        if (aKey1 != Action.USE || argums[2].ToUpper() != "ON" ||
                            !myCmds[aKey1].Contains(argums[1].ToUpper()) || !myCmds[aKey1].Contains(argums[3].ToUpper()))       // was ToLower(), always returned error message
                        {
                            // Error user has typed an invalid command
                            // repeat the while loop
                            Console.WriteLine("case 4 Unrecognized command. What do you wanna do?");
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


        //public static string TrimSpacesBetweenString(string s)
        //{
        //    var mystring = s.RemoveTandNs().Split(new string[] { " " }, StringSplitOptions.None);
        //    string result = string.Empty;
        //    foreach (var mstr in mystring)
        //    {
        //        var ss = mstr.Trim();
        //        if (!string.IsNullOrEmpty(ss))
        //        {
        //            result = result + ss + " ";
        //        }
        //    }
        //    return result.Trim();

        //}
    }





}


