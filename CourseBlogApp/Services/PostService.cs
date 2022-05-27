using CourseBlogApp.Interfaces;
using CourseBlogApp.Models;

namespace CourseBlogApp.Services
{
    public class PostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _unitOfWork.Repo<Post>().GetAllAsync();
        }

        public async Task AddPost(Post post)
        {
            _unitOfWork.Repo<Post>().Add(post);

            await _unitOfWork.Repo<Post>().SaveChangesAsync();
        }

        public async Task EditPost(string id, Post post)
        {
            var tmpPost = await _unitOfWork.Repo<Post>().GetByIdAsync(id);

            _unitOfWork.Repo<Post>().SetValues(tmpPost, post);
            await _unitOfWork.Repo<Post>().SaveChangesAsync();
        }

        public async Task DeletePost(string id)
        {
            var post = await _unitOfWork.Repo<Post>().GetByIdAsync(id);

            _unitOfWork.Repo<Post>().Delete(post);
            await _unitOfWork.Repo<Post>().SaveChangesAsync();
        }
    }
}
