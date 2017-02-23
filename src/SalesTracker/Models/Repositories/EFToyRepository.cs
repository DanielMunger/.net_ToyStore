using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace SalesTracker.Models.Repositories
{
    public class EFToyRepository : IToyRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public EFToyRepository(ApplicationDbContext connection = null)
        {
            if(connection == null)
            {
                this.db = new ApplicationDbContext();
            }
            else
            {
                this.db = connection;
            }
        }
        public IQueryable<Toy> Toys
        { get { return db.Toys; } }

        public Toy Create(Toy toy)
        {
            Debug.WriteLine(toy.Name);
            db.Toys.Add(toy);
            db.SaveChanges();
            return toy;
        }
        public Toy Edit(int id, string name, string description, int cost, int price, byte[] picture)
        {
            var toyFromDb = db.Toys.FirstOrDefault(toy => toy.ToyId == id);
            if (toyFromDb != null)
            {
                toyFromDb.Name = name;
                toyFromDb.Price = price;
                toyFromDb.Cost = cost;
                toyFromDb.Description = description;
                toyFromDb.Picture = picture;
                db.SaveChanges();
            }
           
            return toyFromDb;
        }
        public void Delete(Toy toy)
        {
            db.Toys.Remove(toy);
            db.SaveChanges();
        }
    }
}
