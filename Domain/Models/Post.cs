using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Post
    {
        public int ID { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? PostBody { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishTime { get; set; } = DateTime.Now;

        public int Rating { get; set; }
    }
}
