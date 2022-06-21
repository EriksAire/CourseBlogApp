namespace Domain.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public string? Name { get; set; }

        public string? CommentBody { get; set; }

        public int PostID { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.Now;

        public bool isReported { get; set; } = false;
    }
}
