using System.Collections.Generic;
namespace HotelReservationSDK
{
    public class Reception
    {
        private  HouseKeeping houseKeeping = new HouseKeeping();
        public  string RequestRoom()
        {
            return GetNearestAvailableRoom();
        }

        private  string GetNearestAvailableRoom()
        {
            return Rooms.GetNearestAvailableRoom();
        }

        public int Checkout(string roomNumber)
        {
            Room room = new Room();
            int result = Rooms.Checkout(roomNumber, ref room);
            return result;
        }

        public List<string> GetAllAvailableRooms()
        {
            return Rooms.GetAllAvailableRooms();
        }

        private void CleanRoom(string roomNumber)
        {
            houseKeeping.CleanRoom(roomNumber);
            return;
        }

        public bool MarkRoomAsRepaired(string roomNumber)
        {
            return Rooms.MarkRoomAsRepaired(roomNumber);
        }

        public bool MarkRoomAsAvailable(string roomNumber)
        {
            CleanRoom(roomNumber);
            return Rooms.MarkRoomAsAvailable(roomNumber);
        }

        public bool MarkRoomForRepair(string roomNumber)
        {
            Room room = new Room();
            bool result = false;
            result =  Rooms.MarkRoomForRepair(roomNumber, ref room);
            if(result)
               houseKeeping.RepairRoom(roomNumber);
            return result;
        }
    }
}
