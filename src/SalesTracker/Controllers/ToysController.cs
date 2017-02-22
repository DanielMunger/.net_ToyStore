using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesTracker.Models;
using SalesTracker.Models.Repositories;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Diagnostics;

namespace SalesTracker.Controllers
{
    public class ToysController : Controller
    {
        private IToyRepository toyRepo;
        public ToysController(IToyRepository thisRepo = null)
        {
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
            return View(toyRepo.Toys.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string name, string description, int cost, int price, IFormFile picture)
        {
            
            byte[] pictureArray = new byte[0];
            
            if (picture.Length > 0)
            {
                using (var fileStream = picture.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    pictureArray = ms.ToArray();
                }
            }
            Toy newToy = new Toy(name, description, cost, price, pictureArray);
            Debug.WriteLine(newToy.Name);
            toyRepo.Create(newToy);
            return RedirectToAction("Index");
        }

        public IActionResult Details (int id)
        {
            var thisToy = toyRepo.Toys.FirstOrDefault(ToysController => ToysController.ToyId == id);
            return View(thisToy);
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
