using System.Data.SqlTypes;
using Microsoft.Data.Sqlite;


string connectionString = @"Data Source=habit_tracker.db";
string reader;



using (var connection = new SqliteConnection(connectionString))
{
    using (var tableCmd = connection.CreateCommand())
    {
        tableCmd.CommandText =
            @"CREATE TABLE IF NOT EXISTS codingHours (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Date TEXT NOT NULL UNIQUE,
            Quantity INTEGER NOT NULL
            )";

        connection.Open();

        tableCmd.ExecuteNonQuery();

        DataBaseCmd dataBaseCmd = new DataBaseCmd(tableCmd);

        while (dataBaseCmd.exit == false)
        {
            

            Console.WriteLine("What would you like to do?");
            reader = Console.ReadLine();

            switch (reader)
            {
                case "Add":
                    dataBaseCmd.AddItemToDataBase();
                    break;
                case "Remove":
                    Console.WriteLine("Pending implementation of Remove");
                    break;
                case "Check":
                    Console.WriteLine("Pending implementation of Check");
                    break;
                case "Update":
                    dataBaseCmd.UpdateDataBaseItem();
                    break;
                case "Exit":
                default:
                    dataBaseCmd.Exit();
                    break;

            }
            
            
            
        }

        


        connection.Close();

        
    }

    

}