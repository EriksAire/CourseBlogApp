using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Post
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Required]
        [StringLength(500, ErrorMessage ="Text length cannot be bigger than 500 characters")]
        public string? PostBody { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishTime { get; set; } = DateTime.Now;

        public int Rating { get; set; }
    }
}
