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
        Toy Edit(Toy toy);
        void Delete(Toy toy);

        }
    
}
