using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSDK
{
    interface IRoom
    {
        int GetRoomId();// id represents distance from Reception
        Rooms.RoomType GetRoomType();
        string GetRoomNumber();
        Rooms.RoomStatus RoomStatus
        {
            get;
            set;
        }
    }
}
