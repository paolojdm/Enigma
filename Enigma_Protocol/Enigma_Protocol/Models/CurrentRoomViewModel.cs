namespace Enigma_Protocol.Models
{
    public class CurrentRoomViewModel
    {
        public PlayerProgress PlayerProgress { get; set; }
        public Room CurrentRoom { get; set; }
        public Puzzle CurrentPuzzle { get; set; }
    }
}
