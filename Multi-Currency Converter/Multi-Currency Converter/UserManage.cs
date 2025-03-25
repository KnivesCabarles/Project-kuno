using System;
using MySql.Data.MySqlClient;

namespace CurrencyConverter
{
    class UserManage
    {
        private static string connectionString = "server=localhost;database=currency_converter;user=root;password=;";

        public static void RegisterUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter full name: ");
            string fullName = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO users (username, password, full_name, email) VALUES (@username, @password, @fullName, @email)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@fullName", fullName);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Registration successful!");
        }

        public static void LoginUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id FROM users WHERE username=@username AND password=@password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int userId = reader.GetInt32("id");
                    Console.WriteLine("Login successful!");
                    UserMenu(userId);
                }
                else
                {
                    Console.WriteLine("Invalid username or password.");
                }
            }
        }

        private static void UserMenu(int userId)
        {
            while (true)
            {
                Console.WriteLine("\n1. Convert Currency\n2. View History\n3. Add Favorite Pair\n4. Logout");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Converter.ConvertCurrency(userId);
                        break;
                    case "2":
                        Converter.ViewHistory(userId);
                        break;
                    case "3":
                        Converter.AddFavoritePair(userId);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
