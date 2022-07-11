using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Services
{
    class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddComment(Comment comment)
        {
            _unitOfWork.Repo<Comment>().Add(comment);

            await _unitOfWork.Repo<Comment>().SaveChangesAsync();
        }

        public async Task DeleteComment(int id)
        {
            var comment = await _unitOfWork.Repo<Comment>().GetByIdAsync(id);

            if(comment != null)
            {
                _unitOfWork.Repo<Comment>().Delete(comment);
            }

            await _unitOfWork.Repo<Comment>().SaveChangesAsync();
        }

        public async Task EditComment(int id, Comment comment)
        {
            var tmpComment = await _unitOfWork.Repo<Comment>().GetByIdAsync(id);

            if (tmpComment != null)
            {
                _unitOfWork.Repo<Comment>().SetValues(tmpComment,comment);
            }

            await _unitOfWork.Repo<Comment>().SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _unitOfWork.Repo<Comment>().GetAllAsync();
        }

        public async Task<Comment> GetComment(int id)
        {
            var comment = await _unitOfWork.Repo<Comment>().GetByIdAsync(id);

            if (comment != null)
            {
                return comment;
            }

            throw new NullReferenceException("No Comment found");
        }
    }
}
