namespace Enigma_Protocol.Models
{
    public class CurrentRoomViewModel
    {
        public PlayerProgress PlayerProgress { get; set; }

        //public Room CurrentRoom { get; set; } // NOT USED BECAUSE PlayerProgress already has it
        public Puzzle CurrentPuzzle { get; set; }

        public string? RoomBackgroundMusic { get; set; }
        public string? RoomImage {  get; set; }
        public string? Var1 {  get; set; } //empty variable to use later

    }
}
