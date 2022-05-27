using System.ComponentModel.DataAnnotations;

namespace CourseBlogApp.Models
{
    public class Post
    {
        public string ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string PostBody { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishTime { get; set; } = DateTime.Now;

        public int NumberOfLike { get; set; }

        public int NumberOfDislikes { get; set; }
    }
}
