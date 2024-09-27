namespace Enigma_Protocol.Models
{
    public class CommentReviewViewModel
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public string OriginalComment { get; set; }
        public string AdminComment { get; set; }
    }
}
