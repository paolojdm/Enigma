namespace Enigma_Protocol.Models
{
    public class CurrentRoomViewModel
    {
        public PlayerProgress? PlayerProgress { get; set; }

        //public Room CurrentRoom { get; set; } // NOT USED BECAUSE PlayerProgress already has it

        //public Puzzle? CurrentPuzzle { get; set; } // NOT USED BECAUSE THERE ARE MORE PUZZLES IN THE ROOM NOW

        public IEnumerable<Puzzle>? CurrentRoomPuzzles { get; set; }

        public string? RoomBackgroundMusic { get; set; }
        public string? RoomImage {  get; set; }
        public string? Var1 {  get; set; } //empty variable to use later

        //public int GetRemainingRoomTime()
        //{
        //    // Assuming you have a time limit for the room in seconds
        //    int roomTimeLimit = 1800; // e.g., 30 minutes
        //    DateTime roomStartTime = PlayerProgress.RoomStartTime;

        //    int elapsedSeconds = (int)(DateTime.UtcNow - roomStartTime).TotalSeconds;
        //    int remainingTime = roomTimeLimit - elapsedSeconds;

        //    return remainingTime > 0 ? remainingTime : 0; // Return 0 if time's up
        //}


    }
}
