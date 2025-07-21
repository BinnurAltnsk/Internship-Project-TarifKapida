namespace tarifkapida.Models.DTO
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime ReviewCreatedAt { get; set; }
        public DateTime ReviewUpdatedAt { get; set; }
    }
}
