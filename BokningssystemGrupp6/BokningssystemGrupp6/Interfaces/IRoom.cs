using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Interfaces
{
    //Interface for rooms properties
    public interface IRoom
    {
        //Name of room
        string RoomName { get; set; }
        //Type of room
        string RoomType { get; set; }
        //Amount of seats in room
        int SeatAmount { get; set; }
    }
}
