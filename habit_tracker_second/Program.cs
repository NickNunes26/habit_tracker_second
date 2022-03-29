using System.Data.SqlTypes;
using Microsoft.Data.Sqlite;


string connectionString = @"Data Source=habit_tracker.db";

DataBaseCmd dataBaseCmd = new DataBaseCmd();

using (var connection = new SqliteConnection(connectionString))
{
    using (var tableCmd = connection.CreateCommand())
    {
        tableCmd.CommandText =
            @"CREATE TABLE IF NOT EXISTS codingHours (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Date TEXT,
            Quantity INTEGER
            )";

        connection.Open();

        tableCmd.ExecuteNonQuery();

        dataBaseCmd.AddItemToDataBase();

        tableCmd.CommandText =
            @"INSERT INTO codingHours (
            Date, Quantity)
            VALUES (" + dataBaseCmd.todayDate + dataBaseCmd.hoursCoded + ")";




        connection.Close();

        
    }

    

}