using Application.Interfaces;
using CourseBlogApp.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseBlogApp.Controllers
{
    public class RolesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IUnitOfWork unitofwork)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitofwork;
        }
        public IActionResult Index() => View(_roleManager.Roles.ToList());

        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(_userManager.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }
            return NotFound();
        }

        public IActionResult ListOfReportedComments() =>
            View(_unitOfWork.Repo<Comment>().Find(x => x.isReported == true));

        public async Task<IActionResult> ApproveReportedComment(int commentid)
        {
            var comment = await _unitOfWork.Repo<Comment>().GetByIdAsync(commentid);
            comment.isReported = false;
            _unitOfWork.Repo<Comment>().Update(comment);
            await _unitOfWork.Repo<Comment>().SaveChangesAsync();

            return RedirectToAction("ListOfReportedComments");
        }

        public async Task<IActionResult> DeleteReportedComment(int commentid)
        {
            var comment = await _unitOfWork.Repo<Comment>().GetByIdAsync(commentid);
            _unitOfWork.Repo<Comment>().Delete(comment);
            await _unitOfWork.Repo<Comment>().SaveChangesAsync();

            return RedirectToAction("ListOfReportedComments");
        }
    }
}
