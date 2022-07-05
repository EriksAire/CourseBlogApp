
using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Services
{
    public class PostService : IPostService
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

        public async Task EditPost(int id, Post post)
        {
            var tmpPost = await _unitOfWork.Repo<Post>().GetByIdAsync(id);

            if (tmpPost != null)
            {
                _unitOfWork.Repo<Post>().SetValues(tmpPost, post);
            }

            await _unitOfWork.Repo<Post>().SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            var post = await _unitOfWork.Repo<Post>().GetByIdAsync(id);

            if(post != null)
            {
                _unitOfWork.Repo<Post>().Delete(post);
            }

            await _unitOfWork.Repo<Post>().SaveChangesAsync();
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await _unitOfWork.Repo<Post>().GetByIdAsync(id);

            if (post != null)
            {
                return post;
            }

            throw new NullReferenceException("No post found");
        }
    }
}
