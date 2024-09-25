namespace Enigma_Protocol.Models
{
    public class Room
    {
        public Room(int roomId, string name, string description, List<Puzzle> puzzles)
        {
            RoomId = roomId;
            Name = name;
            Description = description;
            Puzzles = puzzles;
        }

        public int RoomId { get; set; }  // Primary Key
        public string Name { get; set; }  // e.g., "Mysterious Lab"
        public string Description { get; set; }  // Short description of the room
        public List<Puzzle> Puzzles { get; set; }  // One-to-many relationship with puzzles
    }
}
