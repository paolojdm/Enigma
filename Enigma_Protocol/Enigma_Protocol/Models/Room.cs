namespace Enigma_Protocol.Models
{
    public class Room
    {
        // Parameterless constructor for EF
        public Room()
        {
            Puzzles = new List<Puzzle>(); // Initialize the collection to prevent null references
            PlayerProgresses = [];
        }

        public Room(int roomId, string name, string description)
        {
            RoomId = roomId;
            Name = name;
            Description = description;
            Puzzles = new List<Puzzle>(); // Initialize with an empty collection
            PlayerProgresses = [];
        }

        public int RoomId { get; set; }  // Primary Key
        public string Name { get; set; }  // e.g., "Mysterious Lab"
        public string Description { get; set; }  // Short description of the room

        // Change List<Puzzle> to ICollection<Puzzle> for EF compatibility
        public ICollection<Puzzle> Puzzles { get; set; }  // One-to-many relationship with puzzles
        public ICollection<PlayerProgress> PlayerProgresses { get; set; }  // One-to-many relationship with puzzles
    }
}
