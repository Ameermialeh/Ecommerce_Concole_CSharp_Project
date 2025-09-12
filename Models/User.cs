using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Concole_CSharp_Project.Models
{
    public enum Role { Customer, Emplyee, Manager }
    internal abstract class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime? date { get; set; }

        public abstract Role access();

    }
}
