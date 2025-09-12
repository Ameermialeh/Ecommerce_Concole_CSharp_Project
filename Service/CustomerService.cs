using Ecommerce_Concole_CSharp_Project.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Concole_CSharp_Project.Service
{
    internal class CustomerService
    {
        static List<Customer> customerList;

        public CustomerService()
        {
            customerList = new List<Customer>();
        }

        public static bool CheckIfExiste(int customerID)
        {
            Customer? cust = getCustomer(customerID);
            if (cust != null)
                return true;
            return false;
        }
        public static bool CheckPassword(int customerID, string customerPass)
        {
            Customer? cust = getCustomer(customerID);
            if (cust != null && cust.Password == customerPass)
                return true;
            return false;
        }
        public static void CreateNewCustomer()
        {
            Customer newCustomer = new Customer();
            newCustomer.Name = EnterInput("Enter Your Name: ", "The name field is empty Try again ");
            newCustomer.Email = EnterInput("Enter Your Email: ", "The email field is empty  Try again ");
            newCustomer.Password = EnterInput("Enter Password: ", "The password field is empty  Try again ");
            newCustomer.date = EnterDate("Enter Your Date (yyyy-MM-dd) :               -optionaly-");

            newCustomer.CreateAt = DateTime.Now;
            newCustomer.Type = CustomerType.Normal;

            customerList.Add(newCustomer);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCreated new Customer Done");
            Console.ResetColor();
            Console.WriteLine($"Welcome {newCustomer.Name} ... Your ID is {newCustomer.Id}, Try to Login\n");
        }
        public static Customer? getCustomer(int idCustomer)
        {
            return customerList.FirstOrDefault(c => c.Id == idCustomer) ?? null;
        }

        public static void CreateNewOrderByCustomer(OrderService orderService, ProductService productService, Customer currentCustomer)
        {
            Console.WriteLine("Enter the number of products you want to buy:");
            int count = Convert.ToInt32(Console.ReadLine());

            List<Product> listOfProducts = AddProductsToList(productService, count);

            if (listOfProducts.Count > 0)
            {
                Console.WriteLine("Confirm the Order (y/n)?");
                char c = char.Parse(Console.ReadLine()!);
                ConfirmOrder(orderService, listOfProducts, currentCustomer, c);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No Products Added\n");
                Console.ResetColor();
            }
        }

        public static void AddOrederToCustomer(Order order, int CustomerId)
        {
            customerList.FirstOrDefault(c => c.Id == CustomerId)?.Orders.Add(order);
        }

        public static void showOrdersForCustomer(int customerID)
        {

            List<Order> orders = new List<Order>();
            orders = customerList.FirstOrDefault(c => c.Id == customerID)!.Orders;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                  My Orders \n");
            Console.ResetColor();

            foreach (var order in orders)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"   Order ID :   {order.Id}");
                Console.ResetColor();
                Console.WriteLine($"   Include Products : ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--------------------------------------------------");
                Console.ResetColor();
                Console.WriteLine("   ID   |   Name   |   Quantity   |   Price ($)   ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--------------------------------------------------");
                Console.ResetColor();
                foreach (var product in order.Products)
                {
                    Console.WriteLine($"   {product.Id}   ,   {product.Name}   ,    {product.Quantity}   ,   {product.Price}$");
                    Console.WriteLine("--------------------------------------------------");

                }
            }
        }

        public List<Customer> getCustomers(Func<Customer, bool> fillter)
        {
            return customerList.Where(fillter).ToList();
        }

        public void ShowAllCustomers()
        {
            List<Customer> customers = getCustomers(c => true);
            if (customers == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No customers found.\n");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                  All Customers \n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("   ID   |   Name   |   Email   |   Type   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();

            foreach (var customer in customers)
            {
                Console.WriteLine($"   {customer.Id}   ,   {customer.Name}   ,   {customer.Email}   ,   {customer.Type}");
                Console.WriteLine("--------------------------------------------------");
            }
            Console.WriteLine("\n");
        }

        public void FindCustomerByID()
        {
            Console.WriteLine("Enter Customer ID:  ");
            int id = Convert.ToInt32(Console.ReadLine());
            Customer? customer = customerList.FirstOrDefault(c => c.Id == id);

            if (customer != null)
            {
                Console.WriteLine($"|   ID: {customer.Id}   |   Name: {customer.Name}   |   Email: {customer.Email}   |   Type: {customer.Type}   |\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Customer Does not exist");
                Console.ResetColor();
            }
        }

        private static string EnterInput(string text, string errorMessage)
        {
            while (true)
            {
                Console.WriteLine(text);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
                Console.ResetColor();
            }
        }
        private static DateTime? EnterDate(string text)
        {
            while (true)
            {
                Console.WriteLine(text);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    return null;

                if (DateTime.TryParseExact(input, "yyyy-MM-dd", null, DateTimeStyles.None, out var date))
                    return date;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid date format. Try again");
                Console.ResetColor();
            }
        }
        public static List<Product> AddProductsToList(ProductService productService, int count)
        {
            var products = new List<Product>();
            for (var i = 1; i <= count; i++)
            {
                Console.WriteLine($"Enter the ID of product {i}: ");
                int idProduct = Convert.ToInt32(Console.ReadLine());

                if (productService.ChickIfExist(idProduct))
                {
                    products.Add(productService.getProduct(idProduct));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{idProduct} added ");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{idProduct} not found Or out of stock\n");
                    Console.ResetColor();
                }
            }
            return products;
        }
        public static void ConfirmOrder(OrderService orderService, List<Product> listOfProducts, Customer currentCustomer, char c)
        {
            if (c == 'y')
            {
                Order order = orderService.AddNewOrder(listOfProducts, currentCustomer);
                AddOrederToCustomer(order, currentCustomer.Id);
                ProductService.DecreaseQuantity(listOfProducts);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Your Order is confirmed\n");
                Console.ResetColor();

                GetBill(order, listOfProducts, currentCustomer);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Your Order is cancled\n");
                Console.ResetColor();
            }
        }

        private static void GetBill(Order order, List<Product> listOfProducts, Customer customer)
        {
            OrderService.DisplayOrder(order);

            var discountType = Discount.getdiscount(customer.Type);

            double totalToPay = listOfProducts.Sum(p => p.Price);
            Console.WriteLine($"------------------------------------------------");

            Console.WriteLine($"Total price : {totalToPay}$");
            Console.WriteLine($"Your dicount : {discountType.getdiscount() * 100}%");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Your final total after discount is : {totalToPay - totalToPay * discountType.getdiscount()}$\n");
            Console.ResetColor();
        }
    }
}
