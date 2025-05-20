using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace ToDoApplication
{
    internal class Validator
    {
        /// <summary>
        /// Function to validate date as per requested format
        /// </summary>
        /// <returns>validated date</returns>
        public static DateOnly GetValidDate()
        {

            DateOnly validDate;
            while (true)
            {
                Console.WriteLine("\nEnter the date :");
                string input = Console.ReadLine();
                if (DateOnly.TryParseExact(input, "dd-MM-yyyy", null, DateTimeStyles.None, out validDate))
                {
                    if (validDate > DateOnly.FromDateTime(DateTime.Now))
                    {
                        return validDate;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease enter a date in future...");
                        Console.ResetColor();
                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease enter a valid date .. (dd-MM-yyyy)");
                    Console.ResetColor();

                }


            }
        }
 
        public static int GetValidInt(string displaymsg)
        {
            Console.WriteLine($"\nEnter the {displaymsg} :");
            string userInput = Console.ReadLine();
            int value;
            while (!int.TryParse(userInput, out value) || value < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nInvalid {displaymsg}... Please enter a valid {displaymsg}");
                Console.ResetColor();
                Console.WriteLine($"Enter {displaymsg} :");
                userInput = Console.ReadLine();
            }
            return value;
        }
        /// <summary>
        /// Function to validate name
        /// </summary>
        /// <param name="displaymsg"></param>
        /// <returns>validated name</returns>
        public static string GetValidName(string displaymsg)
        {
            Console.WriteLine($"\nEnter {displaymsg} :");
            string userInput = Console.ReadLine();
            while (!Regex.IsMatch(userInput, @"[A-Za-z]+([ '-.][A-Zz-z]+)*$"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nInvalid {displaymsg}... Please enter a valid {displaymsg}");
                Console.ResetColor();
                Console.WriteLine($"\nEnter {displaymsg} :");
                userInput = Console.ReadLine();

            }
            return userInput;

        }
        /// <summary>
        /// Function to validate task heading
        /// </summary>
        /// <returns>validated task heading</returns>
        public static string GetValidTaskHeading()
        {

            while (true)
            {
                Console.WriteLine($"\nEnter the task heading :");
                string userInput = Console.ReadLine();
                if (Regex.IsMatch(userInput, @"^[A-Za-z].{0,99}$"))
                {
                    return userInput;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Task Heading");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nNote : Task Heading must begin with a character and  must be less than 100 characters.");
                    Console.ResetColor();
                }

            }
        }
        /// <summary>
        /// Function to validate the recurrence value
        /// </summary>
        /// <returns>validated recurrence</returns>
        public static int GetValidRecurrance()
        {
            while (true)
            {
                int recurrence = GetValidInt("recurrence number");
                if (recurrence >0 && recurrence <= 31)
                {
                    return recurrence;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Recurrence limit (Maximum limit:31)");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nNote :Recurrence must be a positive value less than or equal to 31.");
                    Console.ResetColor();
                }
            }
        }

        public static bool IsUserIdAvailable(int userId, string filepath)
        {
            var workbook=new XLWorkbook(filepath);
            if (workbook.Worksheets.Contains(userId.ToString()))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Function to validate password
        /// </summary>
        /// <returns>validated password</returns>
        public static string GetValidPassword()
        {
            Console.WriteLine("Enter the password:");
            string password=Console.ReadLine();
            while (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"))
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Invalid PassWord");
                Console.ForegroundColor=ConsoleColor.Yellow;
                Console.WriteLine("Password must have atleast one uppercase letter,one lowercase letter,one digit and length must be within 8 to 15");
                Console.ResetColor();
                Console.WriteLine("Enter the password:");
                password = Console.ReadLine();

            }
            return password;
        }

    }
}
