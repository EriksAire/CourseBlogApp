using CourseBlogApp.Models;
using System.Collections.Generic;

namespace CourseBlogApp.ViewModels
{
    public class BlogViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public IEnumerable<Post> TopPosts { get; set; }

        public IEnumerable<Post> LastCommentedPosts { get; set; }
    }
}
