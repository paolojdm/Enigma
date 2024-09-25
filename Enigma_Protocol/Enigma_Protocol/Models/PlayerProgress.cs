namespace Enigma_Protocol.Models
{
    public class PlayerProgress
    {
        public PlayerProgress(int playerProgressId, string playerName,
            int currentRoomId, Room currentRoom, List<Puzzle> solvedPuzzles)
        {
            PlayerProgressId = playerProgressId;
            PlayerName = playerName;
            CurrentRoomId = currentRoomId;
            CurrentRoom = currentRoom;
            SolvedPuzzles = solvedPuzzles;
        }

        public int PlayerProgressId { get; set; }  // Primary Key
        public string PlayerName { get; set; }  // Track by player name
        public int CurrentRoomId { get; set; }  // Foreign key to Room
        public Room CurrentRoom { get; set; }  // Navigation property to Room
        public List<Puzzle> SolvedPuzzles { get; set; }  // Puzzles the player has solved
    }
}
