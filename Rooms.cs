using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservationSDK
{
    internal static class Rooms
    {
        private static List<Room> _vacantRooms = new List<Room>();
        private static List<Room> _availableRooms = new List<Room>();
        private static List<Room> rooms = new List<Room>();
        private static Dictionary<string, int> _roomNumbers = new Dictionary<string, int>();

        static Rooms()
        {
            Initialize();
        }
        private enum RoomNumber
        {
            A1,
            B1,
            C1,
            D1,
            E1,
            A2,
            B2,
            C2,
            D2,
            E2,
            A3,
            B3,
            C3,
            D3,
            E3,
            A4,
            B4,
            C4,
            D4,
            E4,
        };

        public enum RoomStatus
        {
            Occupied,
            Available,
            MarkedForRepair,
            Vacant
        };

        public enum RoomType
        {
            Standard,
            Deluxe,
            Suit
        };

        private static int GetRoomFromRoomNumber(string strRoomNumber, ref Room room)
        {
            int result;
            try
            {
                int roomId = _roomNumbers[strRoomNumber];
                result = GetRoom(roomId, ref room);
            }
            catch(Exception )
            {
                result = -1;
            }
            return result;
        }

        private static int GetRoomIdFromRoomNumber(string strRoomNumber)
        {
            int floor = strRoomNumber[1] - '0';
            int offset = (floor - 1) * 5;
            int roomId = 0;
            int roomValue = 0;
            if (floor % 2 == 0)
            {
                switch(strRoomNumber[0])
                {
                    case 'E':
                        roomValue = 1;
                        break;
                    case 'D':
                        roomValue = 2;
                        break;
                    case 'C':
                        roomValue = 3;
                        break;
                    case 'B':
                        roomValue = 4;
                        break;
                    case 'A':
                        roomValue = 5;
                        break;
                }

            }
            else
            {
                switch (strRoomNumber[0])
                {
                    case 'A':
                        roomValue = 1;
                        break;
                    case 'B':
                        roomValue = 2;
                        break;
                    case 'C':
                        roomValue = 3;
                        break;
                    case 'D':
                        roomValue = 4;
                        break;
                    case 'E':
                        roomValue = 5;
                        break;
                }
            }
            roomId = offset + roomValue;
            return roomId;
        }

        public static int GetRoom(int id, ref Room room)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].GetRoomId() == id)
                {
                    room = rooms[i];
                    return 0;
                }
            }
            return -1;
        }

        public static void Initialize()
        {
            int numRooms = Enum.GetNames(typeof(RoomNumber)).Length;
            for (int i = 0; i < numRooms; i++)
            {
                string strRoomNumber = Enum.GetName(typeof(RoomNumber), i);
                int roomId = GetRoomIdFromRoomNumber(strRoomNumber);
                Room room = new Room(RoomStatus.Available, RoomType.Standard, strRoomNumber, roomId);

                _roomNumbers.Add(strRoomNumber, roomId);
                rooms.Add(room);
                _availableRooms.Add(room);
            }
        }

        public static string GetNearestAvailableRoom()
        {
            string roomNumber = string.Empty;
            if(_availableRooms.Count > 0)
            {
                int roomDistance = _availableRooms.Min(x => x.GetRoomId());
                roomNumber = _roomNumbers.FirstOrDefault(x => x.Value == roomDistance).Key;
                Room room = new Room();
                GetRoomFromRoomNumber(roomNumber, ref room);
                room.RoomStatus = RoomStatus.Occupied;
                _availableRooms.Remove(room);
                return roomNumber;
            }
            return roomNumber;
        }

        public static int Checkout(string strRoomNumber, ref Room room)
        {
            int result = -1;
            if (GetRoomFromRoomNumber(strRoomNumber, ref room) != -1)
            {
                if(room.RoomStatus == RoomStatus.Occupied)
                {
                    room.RoomStatus = RoomStatus.Vacant;
                    _vacantRooms.Add(room);
                    return 0;
                }
            }
            return result;
        }

        public static List<string> GetAllAvailableRooms()
        {
            List<string> listAvlRooms = new List<string>();
            for (int i = 0; i < _availableRooms.Count; i++)
            {
                listAvlRooms.Add(_availableRooms[i].GetRoomNumber());
            }
            return listAvlRooms;
        }

        public static bool MarkRoomAsAvailable(string strRoomNumber)
        {
            Room room = new Room();
            if (GetRoomFromRoomNumber(strRoomNumber, ref room) != -1)
            {
                room.RoomStatus = RoomStatus.Available;
                _vacantRooms.Remove(room);
                _availableRooms.Add(room);
                return true;
            }
            return false;
        }

        public static bool MarkRoomForRepair(string strRoomNumber, ref Room room)
        {
            if(GetRoomFromRoomNumber(strRoomNumber, ref room) != -1)
            {
                if(room.RoomStatus != RoomStatus.Available && room.RoomStatus != RoomStatus.Occupied)
                {
                    room.RoomStatus = RoomStatus.MarkedForRepair;
                    _availableRooms.Remove(room);
                    return true;
                }
            }
            return false;
        }

        public static bool MarkRoomAsRepaired(string strRoomNumber)
        {
            Room room = new Room();
            if (GetRoomFromRoomNumber(strRoomNumber, ref room) != -1)
            {
                if(room.RoomStatus == RoomStatus.MarkedForRepair)
                {
                    room.RoomStatus = RoomStatus.Vacant;
                    return true;
                }
            }
            return false;
        }
    }
}
