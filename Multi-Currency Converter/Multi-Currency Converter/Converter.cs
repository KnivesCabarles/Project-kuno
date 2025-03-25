using System;
using MySql.Data.MySqlClient;

namespace CurrencyConverter
{
    class Converter
    {
        private static string connectionString = "server=localhost;database=currency_converter;user=root;password=;";

        public static void ConvertCurrency(int userId)
        {
            Console.Write("Enter currency to convert from (e.g., USD): ");
            string fromCurrency = Console.ReadLine();
            Console.Write("Enter currency to convert to (e.g., PHP): ");
            string toCurrency = Console.ReadLine();
            Console.Write("Enter amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            decimal convertedAmount = amount * 55.0m; // Dummy conversion rate

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO conversion_history (user_id, from_currency, to_currency, amount, converted_amount) VALUES (@userId, @fromCurrency, @toCurrency, @amount, @convertedAmount)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@fromCurrency", fromCurrency);
                cmd.Parameters.AddWithValue("@toCurrency", toCurrency);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@convertedAmount", convertedAmount);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine($"Converted Amount: {convertedAmount} {toCurrency}");
        }

        public static void ViewHistory(int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT from_currency, to_currency, amount, converted_amount, conversion_date FROM conversion_history WHERE user_id=@userId ORDER BY conversion_date DESC";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\nConversion History:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["from_currency"]} -> {reader["to_currency"]}: {reader["amount"]} = {reader["converted_amount"]} on {reader["conversion_date"]}");
                }
            }
        }

        public static void AddFavoritePair(int userId)
        {
            Console.Write("Enter currency to convert from (e.g., USD): ");
            string fromCurrency = Console.ReadLine();
            Console.Write("Enter currency to convert to (e.g., PHP): ");
            string toCurrency = Console.ReadLine();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO favorite_pairs (user_id, from_currency, to_currency) VALUES (@userId, @fromCurrency, @toCurrency)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@fromCurrency", fromCurrency);
                cmd.Parameters.AddWithValue("@toCurrency", toCurrency);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Favorite pair added!");
        }
    }
}
