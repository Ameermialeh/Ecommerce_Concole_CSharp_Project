using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Concole_CSharp_Project.Models
{
    internal class Product
    {
        public static int idcount = 0;
        public Product(string Name, double price, int quantity)
        {
            this.Name = Name;
            this.Price = price;
            this.Quantity = quantity;
            this.CreatAt = DateTime.Now;
            Id = ++idcount;
        }

        public DateTime CreatAt { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public int Quantity { get; set; }
        public string Description { get; set; }

        public string Category { get; set; }
        public double Price { get; set; }

    }
}
