using System;
using MySql.Data.MySqlClient;

class InventorySystem
{
    static string connectionString = "server=localhost;database=inventory_db;user=root;password=;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nInventory System");
            Console.WriteLine("1. View Inventory");
            Console.WriteLine("2. Add Product");
            Console.WriteLine("3. Take Product");
            Console.WriteLine("4. Exit");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewInventory();
                    break;
                case "2":
                    AddProduct();
                    break;
                case "3":
                    TakeProduct();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void ViewInventory()
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM products";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\nInventory:");
            Console.WriteLine("ID | Product Name | Quantity");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["id"]} | {reader["name"]} | {reader["quantity"]}");
            }
        }
    }

    static void AddProduct()
    {
        Console.Write("\nEnter product name: ");
        string name = Console.ReadLine();
        Console.Write("Enter quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Invalid quantity!");
            return;
        }

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO products (name, quantity) VALUES (@name, @quantity)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product added successfully!");
        }
    }

    static void TakeProduct()
    {
        Console.Write("\nEnter product ID to take: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID!");
            return;
        }

        Console.Write("Enter quantity to take: ");
        if (!int.TryParse(Console.ReadLine(), out int takeQuantity))
        {
            Console.WriteLine("Invalid quantity!");
            return;
        }

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string checkQuery = "SELECT quantity FROM products WHERE id = @id";
            MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@id", id);
            object result = checkCmd.ExecuteScalar();

            if (result == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            int currentQuantity = Convert.ToInt32(result);
            if (currentQuantity < takeQuantity)
            {
                Console.WriteLine("Not enough stock available!");
                return;
            }

            string updateQuery = "UPDATE products SET quantity = quantity - @takeQuantity WHERE id = @id";
            MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
            updateCmd.Parameters.AddWithValue("@id", id);
            updateCmd.Parameters.AddWithValue("@takeQuantity", takeQuantity);
            updateCmd.ExecuteNonQuery();
            Console.WriteLine("Product taken successfully!");
        }
    }
}
