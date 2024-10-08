namespace Enigma_Protocol.Models
{
    public class CurrentRoomViewModel
    {
        public PlayerProgress? PlayerProgress { get; set; }
        public IEnumerable<Puzzle>? CurrentRoomPuzzles { get; set; }


        public string? Var1 {  get; set; } //empty variable to use later if needed

        //public int GetRemainingRoomTime()
        //{
        //    // Assuming you have a time limit for the room in seconds
        //    int roomTimeLimit = 1800; // e.g., 30 minutes
        //    DateTime roomStartTime = PlayerProgress.RoomStartTime;

        //    int elapsedSeconds = (int)(DateTime.UtcNow - roomStartTime).TotalSeconds;
        //    int remainingTime = roomTimeLimit - elapsedSeconds;

        //    return remainingTime > 0 ? remainingTime : 0; // Return 0 if time's up
        //} //Time management example


    }
}
