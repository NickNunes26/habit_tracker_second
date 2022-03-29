using System;

public class DataBaseCmd
{
	public int hoursCoded;
	public string todayDate = "";

	public void AddItemToDataBase()
	{

		Console.WriteLine("What day is today?");
		todayDate = Console.ReadLine();


		Console.WriteLine("How many hours did you code today?");
		hoursCoded = Convert.ToInt32(Console.ReadLine());


	}
}
