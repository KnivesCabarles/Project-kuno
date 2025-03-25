using System;

namespace CurrencyConverter
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Multi-Currency Converter!");
            while (true)
            {
                Console.WriteLine("\n1. Register\n2. Login\n3. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UserManage.RegisterUser();
                        break;
                    case "2":
                        UserManage.LoginUser();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
