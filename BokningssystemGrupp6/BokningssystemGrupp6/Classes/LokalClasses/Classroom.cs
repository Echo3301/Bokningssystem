using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    //Classroom inherits from Rooms and interface IRoom and IListable
    internal class Classroom : Rooms, IRoom, IListable
    {
        //Max amount of seats that fit in this room
        public int SeatLimit { get; set; }
        //If the room has a projector
        public bool HasProjector { get; set; }
        //If the rom has a whiteboard
        public bool HasWhiteboard { get; set; }

        //Json deserializer requiers an empty constructor
        public Classroom() :base (){ }
        public Classroom(string roomName, string roomType, int seatAmount, int seatLimit, bool hasProjector, bool hasWhiteboard)
            : base(roomName, roomType, seatAmount)
        {
            SeatLimit = seatLimit;
            HasProjector = hasProjector;
            HasWhiteboard = hasWhiteboard;
        }
    }
}
