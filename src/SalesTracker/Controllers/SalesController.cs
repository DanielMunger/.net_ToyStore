using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesTracker.Models;
using SalesTracker.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace SalesTracker.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _db; 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private IToyRepository toyRepo;
        public SalesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db, IToyRepository thisRepo = null )
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            if (thisRepo == null)
            {
                this.toyRepo = new EFToyRepository();
            }
            else
            {
                this.toyRepo = thisRepo;
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Sale sale)
        {
            return RedirectToAction("Index");
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
        public IActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }
       
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var thisToy = toyRepo.Toys.FirstOrDefault(ToysController => ToysController.ToyId == id);

            
            thisToy.NumberOf -= 1;
            
            var thisSale = new Sale();
            thisSale.User = await _userManager.GetUserAsync(User);
            thisSale.Total += thisToy.Price;
            _db.Add(thisSale);
            _db.SaveChanges();


            thisSale.Toys.Append(thisToy);
            _db.Add(thisSale);
            _db.SaveChanges();
            //Debug.WriteLine(thisSale.Toys[0].Name);
            Debug.WriteLine("saleId:" + thisSale.SaleId);
            return View("Index");
        }
    }
}
