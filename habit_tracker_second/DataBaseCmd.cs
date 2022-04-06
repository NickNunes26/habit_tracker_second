using System;
using Microsoft.Data.Sqlite;
using habit_tracker_second;


public class DataBaseCmd
{
	public int hoursCoded;
	public string todayDate;
	public bool quitProgram = false;

	private SqliteConnection sqlConnection;

	//First things first, my DataBaseCmd object requires the connection string, so here is where I construct it bringing that info.
	public DataBaseCmd(SqliteConnection sqltable)
    {
		sqlConnection = sqltable;
    }

	//Initial check is in charge of checking for the validity of the options, collecting the date and redirecting to the appropriate sector. Connection at this point is closed.
	public void MainMenu(string infoFromMain)
    {

		while(CheckAvailableOptions(infoFromMain) == false)
        {
			Console.WriteLine("Please select a valid option");
			infoFromMain = Console.ReadLine();
		}


		if (infoFromMain == "Exit")
        {
			quitProgram = true;
			return;
        }

		Console.WriteLine("What day is today?");
		todayDate = Console.ReadLine();

		while (CheckDates.CheckDateFormat(todayDate) == false)
		{
			Console.WriteLine("Please input a valid date format. It must be YYYY/MM/DD");
			todayDate = Console.ReadLine();

		}

		sqlConnection.Open();

		switch (infoFromMain)
        {
			case "Add":
				AddItemToDataBase();
				break;
			case "Remove":
				RemoveItemFromDataBase();
				break;
			case "Check":
				CheckItemFromDataBase();
				break;
			case "Update":
				UpdateItemFromDataBase();
				break;
			default:
				break;
        }

		sqlConnection.Close();


	}

	//This will simply check if the user chose an adequate choice.
	private static bool CheckAvailableOptions(string infoFromInitialCheck)
    {
		switch (infoFromInitialCheck)
        {
			case "Add":
			case "Remove":
			case "Check":
			case "Update":
			case "Exit":
				return true;
			default:
				return false;
        }

    }

	//This method will Add a new item to the database. Comes with a checking for repeating values. In case it is repeating, it will either send the user to Update or go back to the main menu.
	private void AddItemToDataBase()
	{

		if (CheckDates.CheckRepeatedDate(todayDate, sqlConnection) == true)
		{
			Console.WriteLine("You already have some data from this day, would you like to update the data instead? (Y/N)");

			if (Console.ReadLine() == "Y")
			{
				UpdateItemFromDataBase();
				return;
			}
			return;
		}

		Console.WriteLine("How many hours did you code today?");

		var inputError = true;
		do
		{
			try
			{

				hoursCoded = Convert.ToInt32(Console.ReadLine());
				inputError = true;
			}
			catch (Exception)
			{
				Console.WriteLine("Please input a valid amount of hours");
				inputError = false;
			}
		}
		while (inputError == false);

		var query =
			@"INSERT INTO codingHours (Date, Quantity) VALUES (@Date, @Quantity)";

		SqliteCommand command = new SqliteCommand(query, sqlConnection);

		command.Parameters.AddWithValue("@Date", todayDate);
		command.Parameters.AddWithValue("@Quantity", hoursCoded);
		command.ExecuteNonQuery();


	}

	//This method will Update an existing item From the database. Comes with a checking for repeating values. In case it is a new value, it will either send the user to Add or go back to the main menu.
	private void UpdateItemFromDataBase()
    {

		if (CheckDates.CheckRepeatedDate(todayDate, sqlConnection) == false)
		{
			Console.WriteLine("You do not have any data from this day, would you like to create a new entry instead? (Y/N)");

			if (Console.ReadLine() == "Y")
			{
				AddItemToDataBase();
				return;
			}
			return;
		}

		Console.WriteLine("How many hours?");
		var inputError = true;
		do
		{
			try
			{

				hoursCoded = Convert.ToInt32(Console.ReadLine());
				inputError = true;
			}
			catch (Exception)
			{
				Console.WriteLine("Please input a valid amount of hours");
				inputError = false;
			}
		}
		while (inputError == false);

		var query =
			@"UPDATE codingHours SET Quantity = " + hoursCoded + " WHERE Date = '" + todayDate + "'";

		SqliteCommand command = sqlConnection.CreateCommand();

		command.CommandText = query;
		command.ExecuteNonQuery();

	}

	//This method will Remove an existing item from the database. Comes with a checking for repeating values. In case the value is not found, it will go back to the main menu.
	private void RemoveItemFromDataBase()
    {
		if (CheckDates.CheckRepeatedDate(todayDate, sqlConnection) == false)
		{
			Console.WriteLine("You do not have any data from this day, redirecting to Main Menu.");
			return;
		}

		var query =
			@"DELETE FROM codingHours WHERE Date = '" + todayDate + "'";

		SqliteCommand command = sqlConnection.CreateCommand();

		command.CommandText = query;
		command.ExecuteNonQuery();

	}

	//This method will return on screen the number of hours you coded on the given date.
	private void CheckItemFromDataBase()
    {
		if (CheckDates.CheckRepeatedDate(todayDate, sqlConnection) == false)
		{
			Console.WriteLine("You do not have any data from this day, redirecting to Main Menu.");
			return;
		}

		var query =
			@"SELECT Quantity FROM codingHours WHERE Date = '" + todayDate + "'";

		SqliteCommand command = new SqliteCommand(query, sqlConnection);

		Console.WriteLine(String.Format("You coded {0} hours on {1}", command.ExecuteScalar(), todayDate));

	}

}
