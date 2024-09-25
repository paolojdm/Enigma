namespace Enigma_Protocol.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string RoomDescription { get; set; }

        // Navigation Properties
        public ICollection<Puzzle> Puzzles { get; set; }
    }
}
