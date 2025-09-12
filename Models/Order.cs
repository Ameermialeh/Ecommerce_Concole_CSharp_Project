using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Concole_CSharp_Project.Models
{
    internal class Order
    {
        public static int idcount = 0;
        public int Id { get; set; }

        public string Description { get; set; }

        public Customer Customer { get; set; }

        public DateTime CrateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public List<Product> Products { get; set; }


        public Order()
        {
            Id = ++idcount;
            CrateAt = DateTime.Now;
            Customer = new Customer();
            Products = [];
        }
    }
}
