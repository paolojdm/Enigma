namespace Enigma_Protocol.Models
{
    public class Puzzle
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int RoomId { get; set; }

        // Navigation Properties
        public Room Room { get; set; }
    }
}
