using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTracker.Models
{
    public class Toy
    {   [Key]
        public int ToyId { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Price { get; set; }
        public byte[] Picture { get; set; }
        public string Description { get; set; }
        public virtual Sale Sale { get; set; }

        public Toy()
        {

        }
        public Toy(string name, string description, int cost, int price, byte[] picture )
        {
            Name = name;
            Description = description;
            Cost = cost;
            Price = price;
            Picture = picture;

        }
    }
}
