using System;
namespace HotelReservationSDK
{
    internal class Room:IRoom
    {
        private Rooms.RoomStatus _roomStatus;
        private Rooms.RoomType _roomType;
        public string _roomNumber;
        public int _roomId;

        public Room(Rooms.RoomStatus roomStatus = Rooms.RoomStatus.Available, 
            Rooms.RoomType roomType =Rooms.RoomType.Standard, 
            string roomNumber = "A1", int roomId = 1)
        {
            _roomStatus = roomStatus;
            _roomId = roomId;
            _roomType = roomType;
            _roomNumber = roomNumber;
        }

        public Rooms.RoomStatus RoomStatus
        {
            get
            {
                return _roomStatus;
            }

            set
            {
                _roomStatus = value;
            }
        }

        public int GetRoomId()
        {
            return _roomId;
        }

        public Rooms.RoomType GetRoomType()
        {
            return _roomType;
        }

        public string GetRoomNumber()
        {
            return _roomNumber;
        }
    }
}
