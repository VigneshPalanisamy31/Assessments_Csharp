using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ToDoApplication
{
    public class UserFileOperation
    {
        public static string filepath = "todo.xlsx";

        /// <summary>
        /// Function to create a file if it is not available
        /// </summary>
        public static void FileIntegrity()
        {
            if (!File.Exists(filepath))
            {
                var workbook = new XLWorkbook();
                workbook.Worksheets.Add("UserNameList");
                var worksheet = workbook.Worksheet("UserNameList");
                worksheet.Cell(1, 1).Value = "User ID";
                worksheet.Cell(1, 2).Value = "User Name";
                worksheet.Cell(1, 3).Value = "Password";
                workbook.SaveAs(filepath);
            }
        }
        /// <summary>
        /// Function to display creation and retrieval messages 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="username"></param>
        public static void UserOps(string userID,string username)
        {
            FileIntegrity();
            using (var workbook = new XLWorkbook(filepath))
            {
                if (!workbook.Worksheets.Contains(userID))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nCreating new user ");
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(1000);
                        Console.Write(". ");
                    }
                    workbook.Worksheets.Add(userID);
                    workbook.SaveAs(filepath);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nRetrieving ");
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(1000);
                        Console.Write(". ");
                    }

                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nWelcome {username}");
                Console.ResetColor();
                Thread.Sleep(1000);
            }

        }
        public static  void SaveFile(List<TodoItem> TodoList, string userID)
        {
            using (var workbook = new XLWorkbook(filepath))
            {
                var ws = workbook.Worksheet(userID);
                ws.Cell(1, 1).Value = "Target Date";
                ws.Cell(1, 2).Value = "Task Heading";
                ws.Cell(1, 3).Value = "Description";
                ws.Cell(1, 4).Value = "Recurrence";
                ws.Cell(1, 5).Value = "Status";
                for (int i = 0; i < TodoList.Count; i++)
                {
                    ws.Cell(i + 2, 1).Value = TodoList[i].targetDate.ToString();
                    ws.Cell(i + 2, 2).Value = TodoList[i].taskHeading;
                    ws.Cell(i + 2, 3).Value = TodoList[i].description;
                    ws.Cell(i + 2, 4).Value = TodoList[i].recurrance;
                    ws.Cell(i + 2, 5).Value = TodoList[i].status;
                }
                workbook.SaveAs(filepath);

            }
        }

        public static void LoadUsers(string filepath, List<User> UserList)
        {
            using (var workbook = new XLWorkbook(filepath))
            {
                var ws = workbook.Worksheet("UserNameList");
                int row = 2;
                while (!ws.Cell(row, 1).IsEmpty())
                {
                   UserList.Add(
                     new User((int)ws.Cell(2,1).GetDouble(),ws.Cell(2,2).GetString()));
                    row++;
                }
            }
        }

    }
}
