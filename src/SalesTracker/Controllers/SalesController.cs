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
using Microsoft.EntityFrameworkCore;

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
            Toy thisToy = toyRepo.Toys.FirstOrDefault(ToysController => ToysController.ToyId == id);
            thisToy.NumberOf -= 1;
            Sale thisSale = new Sale();
            Debug.WriteLine(thisToy.Name);
            thisSale.User = await _userManager.GetUserAsync(User);
            thisSale.Total += thisToy.Price;
            thisToy.Sale = thisSale;
            _db.Entry(thisToy).State = EntityState.Modified;
            _db.Sales.Add(thisSale);
            _db.SaveChanges();
            Debug.WriteLine(thisSale.SaleId);
            Sale newSale = _db.Sales.FirstOrDefault(s => s.SaleId == thisSale.SaleId);
            
            return View("Index");
        }
    }
}
