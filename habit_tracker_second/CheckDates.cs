using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace habit_tracker_second
{
    public class CheckDates
    {


        public static bool CheckDateFormat(string dateFromInput)
        {
            try
            {
                String[] separated = dateFromInput.Split("/", 3);

                int year = Convert.ToInt32(separated[0]);
                int month = Convert.ToInt32(separated[1]);
                int day = Convert.ToInt32(separated[2]);
                
                //This if will check if the year is between 2020 and 2030, if the day is not a negative value, and if the length of the strings is the appropriate one.

                if (year < 2020 || year > 2030 || day < 1 || separated[0].Length != 4 || separated[1].Length != 2 || separated[2].Length != 2)
                    return false;

                //This switch will make sure you have the maximum number of days in the adequate months. i.e. you don't get February 30th.
                switch (month)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        if (day > 31)
                            return false;
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        if (day > 30)
                            return false;
                        break;
                    case 2:
                        if (Convert.ToDouble(year) % 4 != 4)
                        {
                            if (day > 28)
                                return false;
                        } else
                        {
                            if (day > 29)
                                return false;
                        }
                        break;
                    default:
                        return false;
                }


                
            }
            catch (Exception)
            {
                return false;

                throw;
            }
            
            
            return true;
        }
        

        //This simple function will check if the date is found or not in the system.
        public static bool CheckRepeatedDate(string dateFromInput, SqliteConnection sqlConnecion)
        {

            var command = sqlConnecion.CreateCommand();

            command.CommandText = "SELECT count(*) FROM codingHours WHERE Date='" + dateFromInput + "'";

            int count = Convert.ToInt32(command.ExecuteScalar());

            if (count == 0)
                return false;

            return true;

        }

    }
}
