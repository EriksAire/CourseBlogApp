﻿using Application.Interfaces;
using CourseBlogApp.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace CourseBlogApp.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPostService _postService;

        public HomeController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IPostService postService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _postService = postService;
        }


        [Route("[action]")]
        public async Task<IActionResult> Index(string searchstring)
        {
            var model = new ViewModel();
            model.Comments = await _unitOfWork.Repo<Comment>().GetAllAsync();
            model.Posts = await _unitOfWork.Repo<Post>().GetAllAsync();
            if (!String.IsNullOrEmpty(searchstring))
            {
                model.Posts = _unitOfWork.Repo<Post>().Find(s => s.Title.Contains(searchstring));
            }

            return View("Index", model);
        }

        [ExcludeFromCodeCoverage]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable> Posts()
        {
            return await _postService.GetAllPosts();
        }


        //public IActionResult CreatePost()
        //{
        //    return View("Create");
        //}

        [Route("CreatePost")]
        [HttpPost]
        public async Task CreatePost([FromBody] Post post)
        {
             await _postService.AddPost(post);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{id}")]
        public async Task EditPost(int id, [FromBody] Post post)
        {
            if (id != post.ID)
            {
                throw new NullReferenceException();
            }
            await _postService.EditPost(id, post);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]/{id}")]
        public async Task Delete(int id)
        {
            await _postService.DeletePost(id);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ButtonClick(int id, bool like)
        {
            var post = await _unitOfWork.Repo<Post>().GetByIdAsync(id);
            if (like)
            {
                post.NumberOfLike++;
            }
            else
            {
                post.NumberOfDislikes++;
            }

            _unitOfWork.Repo<Post>().Update(post);
            await _unitOfWork.Repo<Post>().SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task CreateComment(string commentbody, int postid)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            _unitOfWork.Repo<Comment>().Add(new Comment
            {
                Name = user.UserName,
                CommentBody = commentbody,
                PostID = postid
            });

            await _unitOfWork.Repo<Comment>().SaveChangesAsync();
        }

        public async Task<IActionResult> ReportComment(int commentid)
        {
            var comment = await _unitOfWork.Repo<Comment>().GetByIdAsync(commentid);
            comment.isReported = true;
            _unitOfWork.Repo<Comment>().Update(comment);
            await _unitOfWork.Repo<Comment>().SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
