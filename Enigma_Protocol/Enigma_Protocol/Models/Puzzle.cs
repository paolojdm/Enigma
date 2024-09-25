namespace Enigma_Protocol.Models
{
    public class Puzzle
    {
        public Puzzle(int puzzleId, string question, string answer, int roomId, Room room)
        {
            PuzzleId = puzzleId;
            Question = question;
            Answer = answer;
            RoomId = roomId;
            Room = room;
        }

        public int PuzzleId { get; set; }  // Primary Key
        public string Question { get; set; }  // The puzzle question or task
        public string Answer { get; set; }  // Correct answer
        public int RoomId { get; set; }  // Foreign key to Room
        public Room Room { get; set; }  // Navigation property to Room
        public List<PlayerProgress> SolvedByPlayers { get; set; }
    }
}

