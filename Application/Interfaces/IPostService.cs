using Domain.Models;

namespace Application.Interfaces
{
    public interface IPostService
    {
        Task AddPost(Post post);
        Task DeletePost(int id);
        Task EditPost(int id, Post post);
        Task<IEnumerable<Post>> GetAllPosts();
    }
}