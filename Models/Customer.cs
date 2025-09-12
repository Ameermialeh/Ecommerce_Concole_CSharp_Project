using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Concole_CSharp_Project.Models
{
    public enum CustomerType { Normal, Vip }
    internal class Customer : User
    {
        public static int idcount = 1;

        public string VisaInfo { get; set; }

        public DateTime CreateAt { get; set; }

        public List<Order> Orders { get; set; }

        public CustomerType Type { get; set; }

        public override Role access()
        {
            return Role.Customer;
        }

        public Customer(CustomerType customerType = CustomerType.Normal)
        {
            this.Type = customerType;
            this.CreateAt = DateTime.Now;
            idcount *= 3352;
            idcount %= 1800;
            Id = idcount;
            this.Orders = new List<Order>();
        }
    }
}
