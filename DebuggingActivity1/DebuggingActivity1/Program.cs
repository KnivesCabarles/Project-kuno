using System;

namespace DebuggingActivity1
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.Write("\nEnter Product Here (or type 'exit' to quit): ");
                string product = Console.ReadLine();

                if (product.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting program...");
                    break;
                }

                Inventory.InsertProduct(product);
                Inventory.DisplayProducts();
            }
        }
    }
}
