using Ecommerce_Concole_CSharp_Project.Models;
using Ecommerce_Concole_CSharp_Project.Service;

namespace Ecommerce_Concole_CSharp_Project
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Ameer e-commerce platform\n");
            CustomerService customerService = new CustomerService();
            ProductService productService = new ProductService();
            OrderService orderService = new OrderService();
            EmployeeService employeeService = new EmployeeService();

            while (true)
            {
                Menu();
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    int choice = Convert.ToInt32(input);
                    MenuChoose(choice, customerService, productService, orderService, employeeService);
                }
            }
        }

        public static void Menu()
        {
            Console.WriteLine(" ----------------------------------");
            Console.WriteLine("|  1. Login as Customer            |");
            Console.WriteLine("|  2. Login as Employee            |");
            Console.WriteLine("|  3. Create new Customer account  |");
            Console.WriteLine("|  4. Exit                         |");
            Console.WriteLine(" ----------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n   Please choose a number:\n");
            Console.ResetColor();
        }
        public static void CustomerMenu()
        {
            //Future edit > upgrade to VIP
            Console.WriteLine(" --------------------------------------- ");
            Console.WriteLine("|  1. Show all Products                 |");
            Console.WriteLine("|  2. Search by ID for product          |");
            Console.WriteLine("|  3. Search by Name for product        |");
            Console.WriteLine("|  4. Find Product With maximum price   |");
            Console.WriteLine("|  5. Find Product With minimum price   |\n");
            Console.WriteLine("|  6. Create new Order                  |");
            Console.WriteLine("|  7. Show my orders                    |");
            Console.WriteLine("|  8. Exit                              |");
            Console.WriteLine(" --------------------------------------- ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n   Please choose a number:\n");
            Console.ResetColor();
        }
        public static void EmployeeMenu()
        {
            //Future edit > upgrade customer
            Console.WriteLine(" ----------------------------------------------");
            Console.WriteLine("|  1. Show all Orders                          |");
            Console.WriteLine("|  2. Create new Order for specific customer   |\n");
            Console.WriteLine("|  3. Show all Products                        |");
            Console.WriteLine("|  4. Add new Product                          |");
            Console.WriteLine("|  5. Delete Product                           |");
            Console.WriteLine("|  6. Update Product                           |\n");
            Console.WriteLine("|  7. Show all customers                       |");
            Console.WriteLine("|  8. Find customer by ID                      |");
            Console.WriteLine("|  9. Exit                                     |");
            Console.WriteLine(" ----------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n   Please choose a number:\n");
            Console.ResetColor();
        }

        public static void MenuChoose(int choice, CustomerService customerService, ProductService productService, OrderService orderService, EmployeeService employeeService)
        {
            if (choice == 1)
            {
                LoginAsCustomer(customerService, productService, orderService, employeeService);
            }
            else if (choice == 2)
            {
                LoginAsEmployee(customerService, productService, orderService, employeeService);
            }
            else if (choice == 3)
            {
                CustomerService.CreateNewCustomer();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }
        public static void CustomerChoose(ProductService productService, OrderService orderService, Customer currentCustomer)
        {
            while (true)
            {
                CustomerMenu();
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                
                int num = Convert.ToInt32(input);
                if (num == 1)
                {
                    productService.ViewAllProducts();
                }
                else if (num == 2)
                {
                    productService.SearchByID();
                }
                else if (num == 3)
                {
                    productService.SearchByName();
                }
                else if (num == 4)
                {
                    productService.FindProductByMaxPrice();
                }
                else if (num == 5)
                {
                    productService.FindProductByMinPrice();
                }
                else if (num == 6)
                {
                    CustomerService.CreateNewOrderByCustomer(orderService, productService, currentCustomer);
                }
                else if (num == 7)
                {
                    CustomerService.showOrdersForCustomer(currentCustomer.Id);
                }
                else
                {
                    break;
                }
                
            }
        }
        public static void EmployeeChoose(ProductService productService, OrderService orderService, CustomerService customerService)
        {
            while (true)
            {
                EmployeeMenu();
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                int number = Convert.ToInt32(input);
                if (number == 1)
                {
                    orderService.ViewAllOrders();
                }
                else if (number == 2)
                {
                    EmployeeService.CreateNewOrderByEmployee(orderService, productService, customerService);
                }
                else if (number == 3)
                {
                    productService.ViewAllProducts();
                }
                else if (number == 4)
                {
                    productService.AddProduct();
                }
                else if (number == 5)
                {
                    productService.Delete();
                }
                else if (number == 6)
                {
                    productService.Update();
                }
                else if (number == 7)
                {
                    customerService.ShowAllCustomers();
                }
                else if (number == 8)
                {
                    customerService.FindCustomerByID();
                }
                else
                {
                    break;
                }
            }
        }

        public static bool ChickCustomerValidation(int customerID, string customerPassword)
        {
            if (CustomerService.CheckIfExiste(customerID) && customerPassword != null && CustomerService.CheckPassword(customerID, customerPassword))
            {
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(CustomerService.CheckIfExiste(customerID) ? "Wrong Password!!" : "Customer account Does not Exist !!! Try another ID");
            Console.ResetColor();
            return false;
        }
        public static bool ChickEmployeeValidation(int employeeID, string employeePassword)
        {
            if (EmployeeService.CheckIfExiste(employeeID) && employeePassword != null && EmployeeService.CheckPassword(employeeID, employeePassword))
            {
                return true;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(EmployeeService.CheckIfExiste(employeeID) ? "Wrong Password!!" : "Employee account Does not Exist !!! Try another ID");
            Console.ResetColor();
            return false;
        }

        public static void LoginAsCustomer(CustomerService customerService, ProductService productService, OrderService orderService, EmployeeService employeeService)
        {
            Console.WriteLine("Enter Your Id:  ");
            int customerID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Your Password:  ");
            string? customerPassword = Console.ReadLine();

            if (ChickCustomerValidation(customerID, customerPassword!))
            {
                Customer currentCustomer = CustomerService.getCustomer(customerID)!;
                Console.WriteLine($"\nWelcome {currentCustomer.Name} ...\n");
                CustomerChoose(productService, orderService, currentCustomer);
            }
        }
        public static void LoginAsEmployee(CustomerService customerService, ProductService productService, OrderService orderService, EmployeeService employeeService)
        {
            Console.WriteLine("Enter Your Id:  ");
            int employeeID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Your Password:  ");
            string? employeePassword = Console.ReadLine();

            if (ChickEmployeeValidation(employeeID, employeePassword!))
            {
                Employee currentEmployee = EmployeeService.getEmployee(employeeID)!;
                Console.WriteLine($"\nWelcome {currentEmployee.Name} ...\n");
                EmployeeChoose(productService, orderService, customerService);
            }
        }

    }
}
