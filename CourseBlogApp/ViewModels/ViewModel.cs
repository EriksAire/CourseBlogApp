using Domain.Models;
using System.Collections.Generic;

namespace CourseBlogApp.ViewModels
{
    public class ViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Post> Posts{ get; set; }
    }
}
