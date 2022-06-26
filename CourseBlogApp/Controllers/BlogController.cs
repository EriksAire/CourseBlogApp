using Application.Interfaces;
using CourseBlogApp.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseBlogApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            var viewmodel = new BlogViewModel();
            viewmodel.Posts = await _unitOfWork.Repo<Post>().GetAllAsync();
            viewmodel.Comments = await _unitOfWork.Repo<Comment>().GetAllAsync();

            viewmodel.Posts = viewmodel.Posts.OrderBy(c => c.PublishTime).Take(5);
            viewmodel.TopPosts = viewmodel.Posts.OrderByDescending(s => s.Rating).Take(3);
            var searchstrings = viewmodel.Comments.OrderByDescending(c => c.CommentDate).Select(s => s.PostID).Distinct().Take(3).ToArray();
            var topList = new List<Post>();

           
            viewmodel.LastCommentedPosts = topList;
            return View("Index", viewmodel);
        }
    }
}