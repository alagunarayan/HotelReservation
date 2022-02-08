
namespace HotelReservationSDK
{
    internal class HouseKeeping
    {
        private void Clean(string roomNumber)
        {
            ChangeBedSheets(roomNumber);
            SweepRoom(roomNumber);
            return;
        }

        private void ChangeBedSheets(string roomNumber)
        {
            // code to change bed sheets
        }

        private void SweepRoom(string roomNumber)
        {
            // code to sweep room
        }

        public void CleanRoom(string roomNumber)
        {
            Clean(roomNumber);
        }

        public void RepairRoom(string roomNumber)
        {
            // can put code here to do repair work
            return;
        }
    }
}
