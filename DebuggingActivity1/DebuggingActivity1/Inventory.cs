using System;
using System.Collections.Generic;

namespace DebuggingActivity1
{
    class Inventory
    {
        public static List<string> products = new List<string>();

        public static void InsertProduct(string productName)
        {
            products.Add(productName);
            Console.WriteLine($"'{productName}' has been added to the inventory.");
        }

        public static void DisplayProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("The inventory is empty.");
                return;
            }

            Console.WriteLine("\nProducts in Inventory:");
            foreach (string product in products)
            {
                Console.WriteLine($"- {product}");
            }
        }
    }
}
