using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Concole_CSharp_Project.Models
{
    internal class Employee : User
    {
        public static int idcount = 1;
        public Employee()
        {
            Id = idcount++;
        }

        public int Salary { get; set; }
        public override Role access()
        {
            return Role.Emplyee;
        }

    }
}
