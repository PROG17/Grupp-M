// using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{

    // Description 
    // 
    // Room class contains a description of a squared room with its Name as basic information and some more specific details about
    // - Doors:     the position of a door in a room can be 0 = North, 1 = East,.. and is used as an index in the array 'exitDoors[4]'
    //              while the value of an element exitDoor[position] gives the status of the door: Closed, Open or if there is a bare Wall 
    //              instead of a door
    // - roomItems: a list of object Items is part of each Room instances. The list is updated according to the action of the Player.
    // - EndPoint:  Is true only if the player has reached the last room ..Alive ;-)
    // - Visited:   Is true if the player has visited the room at east once, false otherwise.
    // - Description2: Additional description only visible if the Player use the command LOOK
    //
    // - Constructors: We have 2 types. One which calls the base-class one and the 2nd which allows us to add an additional description
    //                 to the room. 

                    

    public class Room : GameObjects
    {

        // Properties
        public bool Visited { get; set; } = false;              // Keeps track if user have been in the room before
        public bool EndPoint { get; set; } = false;             // Is the room the last ONE = endpoint?
        public string Description2 { get; set; }  = "You find nothing new in here."; // Second "hidden" description, only visible if player use LOOK

        public Door[] exitDoors = new Door[4];                  // Index is the pos in the room (North,East,..) 
        public List<Item> roomItems = new List<Item>();


        // Room constructor(s)

        public Room(string name, string description) : base(name, description)
        {            
            // Description2 = "You find nothing new in here.";
            //  Visited = false;
        }

        public Room(string name, string description, string description2) : base(name, description)
        {
            
            Description2 = description2;
            // Visited = false;
        }


    }
}