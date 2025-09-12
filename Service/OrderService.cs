using Ecommerce_Concole_CSharp_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Concole_CSharp_Project.Service
{
    internal class OrderService
    {
        static List<Order> orderList = new List<Order>();
        public void CreateNewOrder(Order newOrder)
        {
            orderList.Add(newOrder);
        }
        public Order AddNewOrder(List<Product> lop, Customer currCustomer)
        {
            Order order = new Order();

            order.Customer = currCustomer;
            order.Products = lop;
            orderList.Add(order);
            return order;
        }

        public void ViewAllOrders()
        {
            if (orderList == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No orders found.");
                Console.ResetColor();
                return;
            }

            foreach (var order in orderList)
            {
                DisplayOrder(order);
                Console.WriteLine("\n------------------------------\n");
            }
        }

        public static void DisplayOrder(Order order)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Order ID : {order.Id}, Customer name : {order.Customer.Name}");
            Console.ResetColor();
            if (order.Products == null)
            {
                Console.WriteLine("  No products in this order.");
                return;
            }
            int index = 1;
            foreach (var product in order.Products)
            {
                Console.WriteLine($"{index++}. ID:   {product.Id}   ,   Name:   {product.Name},   Price: {product.Price}$   ");
            }
        }
    }
}
