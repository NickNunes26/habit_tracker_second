using System;
using Microsoft.Data.Sqlite;

public class DataBaseCmd
{
	public int hoursCoded;
	public string todayDate = "";
	public bool exit = false;

	private SqliteConnection sqlConnecion;
	private SqliteCommand table;

	public DataBaseCmd(SqliteCommand sqltable)
    {
		table = sqltable;
    }


	public void AddItemToDataBase()
	{

		Console.WriteLine("What day is today?");
		todayDate = Console.ReadLine();

		table.CommandText = 
			@"EXISTS ( 
			SELECT" + todayDate + ")";

		Console.WriteLine(message);




		Console.WriteLine("How many hours did you code today?");
		hoursCoded = Convert.ToInt32(Console.ReadLine());



		table.CommandText =
						@"INSERT INTO codingHours (
                        Date, Quantity)
                        VALUES (" + todayDate + hoursCoded + ")";


	}

	public void Exit()
    {
		exit = true;
    }

	public void UpdateDataBaseItem()
    {
		Console.WriteLine("What day would you like to update?");
		todayDate = Console.ReadLine();

		Console.WriteLine("How many hours?");
		hoursCoded = Convert.ToInt32(Console.ReadLine());
    }

	public void UpdateDataBaseItem(string dateInserted)
	{

		Console.WriteLine("How many hours?");
		hoursCoded = Convert.ToInt32(Console.ReadLine());


	}


}
