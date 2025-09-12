using Ecommerce_Concole_CSharp_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Concole_CSharp_Project
{
    interface IDiscount
    {
        public double getdiscount();
    }

    public class VipDiscount : IDiscount
    {
        public double getdiscount()
        {
            return 0.30;
        }
    }
    public class NormalDiscount : IDiscount
    {
        public double getdiscount()
        {
            return 0.10;
        }
    }
    internal class Discount
    {
        public static IDiscount getdiscount(CustomerType customerType)
        {
            if (customerType == CustomerType.Normal)
                return new NormalDiscount();
            else
                return new VipDiscount();
        }
    }
}
