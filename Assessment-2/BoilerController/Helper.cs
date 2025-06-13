using System;
using System.Collections.Generic;
using System.Linq;
namespace BoilerControllerApplication
{
    internal class Helper
    {
        /// <summary>
        /// Function to display the message to console in green color.
        /// </summary>
        /// <param name="displayMessage"></param>
        public static void WriteInGreen(string displayMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(displayMessage);
            Console.ResetColor();
        }
        /// <summary>
        /// Function to display the message to console in red color.
        /// </summary>
        /// <param name="displayMessage"></param>
        public static void WriteInRed(string displayMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(displayMessage);
            Console.ResetColor();
        }
        /// <summary>
        /// Function to display the message to console in yellow color.
        /// </summary>
        /// <param name="displayMessage"></param>
        public static void WriteInYellow(string displayMessage)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(displayMessage);
            Console.ResetColor();
        }
        /// <summary>
        /// Function to load log from file and return the list.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> CreateOrLoadErrorLog(string fileName)
        {
            List<string> eventLogs = new();
            if (File.Exists(fileName))
                eventLogs= File.ReadAllLines(fileName).ToList();
            return eventLogs;
        }
    
    }
}
