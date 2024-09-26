namespace Enigma_Protocol.Models
{
    public class PlayerProgress
    {
        public int Id { get; set; }
        public int CurrentRoomId { get; set; }
        public int SolvedPuzzles { get; set; }
        public int PlayerID { get; set; }

        // Navigation Properties
        public Room CurrentRoom { get; set; }
        public User User { get; set; }
    }
}
