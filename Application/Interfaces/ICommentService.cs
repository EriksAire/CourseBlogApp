using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICommentService
    {
        Task AddComment(Comment comment);
        Task DeleteComment(int id);
        Task EditComment(int id, Comment comment);
        Task<IEnumerable<Comment>> GetAllComments();
        Task<Comment> GetComment(int id);
    }
}
