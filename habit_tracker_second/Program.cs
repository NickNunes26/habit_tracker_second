using System.Data.SqlTypes;
using Microsoft.Data.Sqlite;


string connectionString = @"Data Source=habit_tracker.db";



using (var connection = new SqliteConnection(connectionString))
{
    using (var tableCmd = connection.CreateCommand())
    {
        //First part of the Command is create a table open the connection and close it right after

        tableCmd.CommandText =
            @"CREATE TABLE IF NOT EXISTS codingHours (
            Date TEXT NOT NULL UNIQUE,
            Quantity INTEGER NOT NULL
            )";

        connection.Open();

        tableCmd.ExecuteNonQuery();

        connection.Close();

        DataBaseCmd dataBaseCmd = new DataBaseCmd(connection);
        
        //Here the code directs you to InitialCheck, which will start the process of collecting data. At this point the connection is closed.

        while (dataBaseCmd.quitProgram == false)
        {
            Console.WriteLine("What would you like to do? (Add/Remove/Check/Update/Exit)");
            dataBaseCmd.MainMenu(Console.ReadLine());         
        }

        //Connection must come out closed.



    }

    

}