using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ToDoApplication
{
    internal class UserLogin
    {
        public static void Main(string[] args)
        {
           
            bool exitApp = false;
            while (!exitApp)
            {
              
                Console.WriteLine("============Welcome to TO-Do Application=============");
                Console.WriteLine("\n1.Register User");
                Console.WriteLine("\n2.Login User");
                Console.WriteLine("\n3.Exit");
                string filepath = "ToDo.xlsx";
                int choice = Validator.GetValidInt("choice");
                List<User> users = new List<User>();
                ToDoHandler applicationUI = new ToDoHandler();
                UserFileOperation.FileIntegrity();
                switch (choice)
                {
                    case 1:
                        int userID = Validator.GetValidInt("userID");
                        if (!Validator.IsUserIdAvailable(userID,filepath))
                        {
                            string username = Validator.GetValidName("User Name");
                            var workbook=new XLWorkbook(filepath);
                            string password=Validator.GetValidPassword();
                            var userWorksheet = workbook.Worksheet("UserNameList");
                            var usedrow = userWorksheet.LastRowUsed().RowNumber() + 1;
                            userWorksheet.Cell(usedrow, 1).Value = userID.ToString();
                            userWorksheet.Cell(usedrow, 2).Value = username;
                            userWorksheet.Cell(usedrow, 3).Value = password;

                            workbook.Save();
                            ToDoHandler.ToDoOps(userID, username);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("User ID Already Exists");
                            Console.ResetColor();
                        }
                        break;
                    case 2:
                        int loginID = Validator.GetValidInt("userID");
                        if (Validator.IsUserIdAvailable(loginID, filepath))
                        {
                            string password = Validator.GetValidPassword();
                            var workbook=new XLWorkbook(filepath);  
                            var worksheet=workbook.Worksheet("UserNameList");
                            var userRow = worksheet.RowsUsed().FirstOrDefault(r => r.Cell(1).GetString().Equals(loginID.ToString()));
                            if(password.Equals(userRow.Cell(3).GetString()))
                            {
                                ToDoHandler.ToDoOps(loginID, userRow.Cell(2).GetString());
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Incorrect Password");
                                Console.ResetColor();
                            }


                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("User ID not found");
                            Console.ResetColor();
                        }
                        break;

                    case 3:
                        Console.WriteLine("\nExiting....");
                        Console.ReadKey();
                        exitApp = true;
                       
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease Enter a valid choice");
                        Console.ResetColor();
                        break;
                }

            }
        }
    }
}
