namespace Enigma_Protocol.Models
{
    public class PlayerProgress
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int CurrentRoomId { get; set; }

        // Navigation Properties
        public Room CurrentRoom { get; set; }
    }
}
