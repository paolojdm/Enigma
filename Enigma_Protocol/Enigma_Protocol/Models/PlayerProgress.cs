namespace Enigma_Protocol.Models
{
    public class PlayerProgress
    {
        // Parameterless constructor
        public PlayerProgress()
        {
            SolvedPuzzles = new List<Puzzle>(); // Initialize the collection
        }

        // Constructor with parameters
        public PlayerProgress(int playerProgressId, string playerName, int currentRoomId, Room currentRoom, List<Puzzle> solvedPuzzles)
        {
            PlayerProgressId = playerProgressId;
            PlayerName = playerName;
            CurrentRoomId = currentRoomId;
            CurrentRoom = currentRoom;
            SolvedPuzzles = solvedPuzzles?? new List<Puzzle>(); // Initialize if null
        }

        // Properties
        public int PlayerProgressId { get; set; }  // Primary Key
        public string PlayerName { get; set; }  // Track by player name
        public int CurrentRoomId { get; set; }  // Foreign key to Room
        public Room CurrentRoom { get; set; }  // Navigation property to Room

        // Corrected type: List<Puzzle> that this player has solved
        public ICollection<Puzzle> SolvedPuzzles { get; set; } = new List<Puzzle>();  // Puzzles the player has solved
    }
}
