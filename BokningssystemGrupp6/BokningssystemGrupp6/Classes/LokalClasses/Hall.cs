using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    //Hall inherits from Rooms and interface IRoom and IListable
    internal class Hall: Rooms, IRoom, IListable
    {
        //Maximum amount of seats allowed
        public int SeatLimit { get; set; } 
        //If room has a projector
        public bool HasProjector { get; set; } 
        //If room has a whiteboard
        public bool HasWhiteboard { get; set; }
         
        //Json deserializer requiers an empty constructor
        public Hall(): base (){}

        public Hall(string roomName, string roomType, int seatAmount, int seatLimit, bool hasProjector, bool hasWhiteboard)
            : base(roomName, roomType, seatAmount)
        {
            SeatLimit = seatLimit;
            HasProjector = hasProjector;
            HasWhiteboard = hasWhiteboard;
        }
    }
}
