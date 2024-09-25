namespace Enigma_Protocol.Models
{
    public class Puzzle
    {
        // Parameterless constructor
        public Puzzle()
        {
            SolvedByPlayers = new List<PlayerProgress>(); // Initialize the navigation property
        }

        public Puzzle(int puzzleId, string question, string answer, int roomId, Room room)
        {
            PuzzleId = puzzleId;
            Question = question;
            Answer = answer;
            RoomId = roomId;
            Room = room;

            SolvedByPlayers = new List<PlayerProgress>();  // Initialize the collection
        }

        public int PuzzleId { get; set; }  // Primary Key
        public string Question { get; set; }  // The puzzle question or task
        public string Answer { get; set; }  // Correct answer
        public int RoomId { get; set; }  // Foreign key to Room
        public Room Room { get; set; }  // Navigation property to Room (that needs fixing)

        //public List<PlayerProgress> SolvedByPlayers { get; set; } // Questa property dovrebbe indicare chi ha risolto il puzzle?
        // Perche' se si dovrebbe essere una lista di User e non PlayerProgress.
        public ICollection<PlayerProgress> SolvedByPlayers { get; set; } = new List<PlayerProgress>(); // This property indicates who solved the puzzle
    }
}

