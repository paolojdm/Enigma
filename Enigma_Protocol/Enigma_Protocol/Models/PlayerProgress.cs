namespace Enigma_Protocol.Models
{
    public class PlayerProgress
    {
        public int Id { get; set; }
        public int CurrentRoomId { get; set; }
        public int SolvedPuzzles { get; set; }
        public int PlayerID { get; set; }
        public int Current_Lives_Room { get; set; } //Quante vite ha nella stanza (e' quante vite sono rimaste)
        public int Current_Lives_Puzzle { get; set; } //QUanti tentativi ha rimasti per completare il puzzle (e' quante vite sono rimaste)

        public DateTime RoomStartTime { get; set; } //Tempo preso dal momento in cui entra nella stanza
        public DateTime CurrentRoomTime { get; set; } //Si aggiorna ogni secondo, se la differenza tra il RoomStartTime e il CurrentRooomTime supera il tempo indicato il gioco finisce

        // Navigation Properties
        public Room CurrentRoom { get; set; }
        public User User { get; set; }
    }
}
