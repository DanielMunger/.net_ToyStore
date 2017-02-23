using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTracker.Models.Repositories
{
        public interface IToyRepository
        {
        IQueryable<Toy> Toys { get; }
        Toy Create(Toy toy);
        Toy Edit(int id, string name, string description, int cost, int price, byte[] picture);
        void Delete(Toy toy);

        }
    
}
