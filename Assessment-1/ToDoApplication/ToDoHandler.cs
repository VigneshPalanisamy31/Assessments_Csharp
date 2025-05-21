using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication
{
    internal class ToDoHandler
    {
        public static void ToDoOps(int userID,string username)
        {
            bool exitApp = false;
            while (!exitApp)
            {
                        UserFileOperation.UserOps(userID.ToString(),username);
                        TodoOperation todoOperations = new TodoOperation(userID.ToString());
                        bool exit = false;
                        while (!exit)
                        {
                            Console.Clear();
                            todoOperations.Dashboard();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n-------------Tracker Options--------------");
                            Console.WriteLine("\n1.Add Task");
                            Console.WriteLine("2.Edit Task");
                            Console.WriteLine("3.Update Status");
                            Console.WriteLine("4.Delete Task");
                            Console.WriteLine("5.View To-Do List");
                            Console.WriteLine("6.Calendar");
                            Console.WriteLine("7.Exit");
                            Console.ResetColor();
                            int userchoice = Validator.GetValidInt("choice");
                            switch (userchoice)
                            {

                                case 1:
                                    todoOperations.AddTask();
                                    break;
                                case 2:
                                    todoOperations.EditTask();
                                    break;
                                case 3:
                                    todoOperations.UpdateStatus();
                                    break;
                                case 4:
                                    todoOperations.DeleteTask();
                                    break;
                                case 5:
                                    todoOperations.ViewTasks();
                                    break;
                                case 6:
                                    todoOperations.Calendar();
                                    break;
                                case 7:
                                    Console.WriteLine("\nExiting....");
                                    exit = true;
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nPlease Enter a valid choice");
                                    Console.ResetColor();
                                    break;

                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        UserFileOperation.SaveFile(todoOperations.TodoList, userID.ToString());
                        break;

                }
            }

        }

    }
