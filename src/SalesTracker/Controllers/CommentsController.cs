using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesTracker.Models;
using Microsoft.AspNetCore.Identity;
using SalesTracker.Models.Repositories;
using System.Diagnostics;

namespace SalesTracker.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private IToyRepository toyRepo;
        public CommentsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db, IToyRepository thisRepo = null)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View(_db.Comments.ToList());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(string body)
        {
            Comment newComment = new Comment();
            newComment.Body = body;
            newComment.User = await _userManager.GetUserAsync(User);
            _db.Comments.Add(newComment);
            _db.SaveChanges();
            return View("Index", _db.Comments.ToList());
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id)
        {
            return RedirectToAction("Index");
        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
