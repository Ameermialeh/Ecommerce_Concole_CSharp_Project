using Ecommerce_Concole_CSharp_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Concole_CSharp_Project.Service
{
    internal class ProductService
    {
        static List<Product> productList;
        public ProductService()
        {
            productList = new List<Product>();
        }

        public void AddProduct()
        {
            string? prodName;
            int prodQuantity;
            int prodPrice;

            Console.WriteLine("Enter Product Name: ");
            prodName = Console.ReadLine();

            Console.WriteLine("Enter Product Quantity: ");
            prodQuantity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Product Price: ");
            prodPrice = Convert.ToInt32(Console.ReadLine());

            Product newProduct = new Product(prodName!, prodPrice, prodQuantity);

            if (newProduct.Quantity == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This Product has no quantity to add");
                Console.ResetColor();
                return;
            }
            productList.Add(newProduct);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Product {newProduct.Name} added successfully");
            Console.ResetColor();
        }

        public void Update()
        {
            Console.WriteLine("Enter Product ID you want to update");
            int updID = Convert.ToInt32(Console.ReadLine());

            if (ChickIfExist(updID))
            {
                string newName;
                int newPrice;
                int newQuantity;
                char c;
                Product product = getProduct(updID);
                Console.WriteLine($"Old Data for product {product.Id}: \n");
                Console.WriteLine($"Old name: {product.Name}");
                Console.WriteLine($"Old price: {product.Price}");
                Console.WriteLine($"Old Quantity: {product.Quantity}");
                Console.WriteLine("---------------------------------\n");

                Console.WriteLine("Want to update name? (y/n)");
                c = char.Parse(Console.ReadLine()!);
                if (c == 'y')
                {
                    Console.WriteLine("Enter new Name: ");
                    newName = Console.ReadLine()!;
                    product.Name = newName;
                }
                Console.WriteLine("Want to update Price? (y/n)");
                c = char.Parse(Console.ReadLine()!);
                if (c == 'y')
                {
                    Console.WriteLine("Enter new Price: ");
                    newPrice = Convert.ToInt32(Console.ReadLine());
                    product.Price = newPrice;
                }

                Console.WriteLine("Want to update Quantity? (y/n)");
                c = char.Parse(Console.ReadLine()!);
                if (c == 'y')
                {
                    Console.WriteLine("Enter new Quantity: ");
                    newQuantity = Convert.ToInt32(Console.ReadLine());
                    product.Quantity = newQuantity;
                }

                productList = productList.Where(p => p.Id != product.Id).ToList();

                productList.Add(product);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Updated Product successfully");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Product with ID : {updID} does not exist\n");
                Console.ResetColor();
            }
        }

        public void Delete()
        {
            Console.WriteLine("Enter Product ID you want to delete");
            int delID = Convert.ToInt32(Console.ReadLine());

            if (ChickIfExist(delID))
            {
                productList = productList.Where(p => p.Id != delID).ToList();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Product with ID : {delID} deleted successfully\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Product with ID : {delID} does not exist\n");
                Console.ResetColor();
            }

        }

        public void SearchByID()
        {
            Console.WriteLine("Enter ID product:  ");
            int id = Convert.ToInt32(Console.ReadLine());
            Product? product = productList.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Console.WriteLine($"|   ID: {product.Id}   |   Name: {product.Name}   |   Quantity: {product.Quantity}   |   Price: {product.Price}   |\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product Does not exist");
                Console.ResetColor();
            }
        }

        public void SearchByName()
        {
            Console.WriteLine("Enter the name of the Product: \n");
            string name = Console.ReadLine();
            List<Product> productsName = getProducts(p => p.Name == name);
            printProducts("Products same name", productsName);
        }

        public void FindProductByMaxPrice()
        {
            Console.WriteLine("Enter the maximum price of the Product: \n");
            int maxPrice = int.Parse(Console.ReadLine());
            List<Product> productsMax = getProducts(p => p.Price <= maxPrice);
            printProducts("Maxmum price", productsMax);
        }

        public void FindProductByMinPrice()
        {
            Console.WriteLine("Enter the minimum price of the Product\n");
            int minPrice = int.Parse(Console.ReadLine());
            List<Product> productsMin = getProducts(p => p.Price >= minPrice);
            printProducts("Minmum price", productsMin);
        }

        public bool ChickIfExist(int idProduct)
        {
            return productList.Any(p => p.Id == idProduct && p.Quantity > 0);
        }

        public Product getProduct(int idProduct)
        {
            return productList.FirstOrDefault(p => p.Id == idProduct)!;
        }

        public List<Product> getProducts(Func<Product, bool> fillter)
        {
            return productList.Where(fillter).ToList();
        }

        public void ViewAllProducts()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                  All Products \n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("   ID   |   Name   |   Quantity   |   Price ($)   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();
            foreach (var prod in productList)
            {
                Console.WriteLine($"   {prod.Id}   ,   {prod.Name}   ,   {prod.Quantity}   ,   {prod.Price}$");
                Console.WriteLine("--------------------------------------------------");
            }
            Console.WriteLine("\n");
        }

        public static void printProducts(string title, List<Product> products)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"                  {title} \n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("   ID   |   Name   |   Quantity   |   Price ($)   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();

            foreach (var prod in products)
            {
                Console.WriteLine($"   {prod.Id}   ,   {prod.Name}   ,   {prod.Quantity}   ,   {prod.Price}$");
                Console.WriteLine("--------------------------------------------------");
            }
            Console.WriteLine("\n");
        }

        public static void DecreaseQuantity(List<Product> listOfProducts)
        {
            foreach (var product in listOfProducts)
            {
                var existingProduct = productList.FirstOrDefault(p => p.Id == product.Id);
                if (existingProduct != null && existingProduct.Quantity > 0)
                {
                    existingProduct.Quantity--;
                }
            }
        }
    }
}
