namespace CourseBlogApp.Models
{
    public class Comment
    {
        public string? ID { get; set; }

        public string? Name { get; set; }

        public string? CommentBody { get; set; }

        public string? PostID { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.Now;

        public bool isReported { get; set; } = false;
    }
}
