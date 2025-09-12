using Ecommerce_Concole_CSharp_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Concole_CSharp_Project.Service
{
    internal class EmployeeService
    {
        static List<Employee> emplyeeList;
        public EmployeeService()
        {
            emplyeeList = new List<Employee>();

            Employee e = new Employee();
            e.Name = "Ameer";
            e.Email = "ameerinad@gmail.com";
            e.Salary = 2000;
            e.Password = "100";
            emplyeeList.Add(e);

        }
        public static Employee? getEmployee(int idemp)
        {
            return emplyeeList.FirstOrDefault(c => c.Id == idemp);
        }
        public static bool CheckPassword(int empID, string empPass)
        {
            Employee? emp = getEmployee(empID);
            if (emp != null && emp.Password == empPass)
                return true;
            return false;
        }
        public static bool CheckIfExiste(int empID)
        {
            Employee? emp = getEmployee(empID);
            if (emp != null)
                return true;
            return false;
        }

        public static void CreateNewOrderByEmployee(OrderService orderService, ProductService productService, CustomerService customerService)
        {

            Console.WriteLine("Enter Customer ID :");
            int custID = Convert.ToInt32(Console.ReadLine());
            Customer customer = CustomerService.getCustomer(custID)!;

            Console.WriteLine("Enter the number of products you want to buy:");
            int count = Convert.ToInt32(Console.ReadLine());
            List<Product> listOfProducts = CustomerService.AddProductsToList(productService, count);

            if (listOfProducts.Count > 0)
            {
                Console.WriteLine("Confirm the Order (y/n)?");
                char c = char.Parse(Console.ReadLine()!);
                CustomerService.ConfirmOrder(orderService, listOfProducts, customer, c);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No Products Added");
                Console.ResetColor();
            }

        }
    }
}
