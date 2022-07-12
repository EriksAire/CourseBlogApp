using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage ="Password must be between 8 and 50 characters")]
        [JsonIgnore] public string Password { get; set; }
    }
}
