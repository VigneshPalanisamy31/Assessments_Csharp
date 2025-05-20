using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ConsoleTables;
namespace ToDoApplication
{
    internal class TodoOperation
    {
        string filepath = "ToDo.xlsx";
        public List<TodoItem> TodoList = new List<TodoItem>();
        public TodoOperation(string userID)
        {
            using (var workbook = new XLWorkbook(filepath))
            {
                var ws = workbook.Worksheet(userID);
                int row = 2;
                while (!ws.Cell(row, 1).IsEmpty())
                {
                    TodoList.Add(
                     new TodoItem(DateOnly.Parse(ws.Cell(row, 1).GetString()), ws.Cell(row, 2).GetString(),ws.Cell(row, 3).GetString(),(int)ws.Cell(row,4).GetDouble(), ws.Cell(row, 5).GetString()));
                    row++;
                }
            }

        }
        /// <summary>
        /// Function to view all to-do tasks of a user.
        /// </summary>

        public void ViewTasks()
        {
            string green = "\u001b[32m";
            string yellow = "\u001b[33m";
            string cyan = "\u001b[36m";
            string red = "\u001b[31m";
            string reset = "\u001b[0m";

            if (TodoList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nNo Tasks Found....");
                Console.ResetColor();
                return;
            }

            var table = new ConsoleTable("ID", "Target Date", "Task Heading", "Status", "Recurrence");
            int count = 1;
            foreach (TodoItem item in TodoList)
            {
                string color = item.status.Equals("Done") ? green : item.status.Equals("In-Progress") ? cyan : yellow;
                table.AddRow(count, $"{color}{item.targetDate}", $"{item.taskHeading}", $"{item.status}", $"{item.recurrance}{reset}");
                count++;
            }
            table.Write(Format.Default);
        }

        /// <summary>
        /// Function to add new to-do tasks of a user.
        /// </summary>

        public void AddTask()
        {
            DateOnly date = Validator.GetValidDate();
            string taskHeading =Validator.GetValidTaskHeading() ;
            Console.WriteLine("\nEnter the description");
            string desc = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nRecurrence Choices");
            Console.WriteLine("\n1.Date\n2.Month\n3.Year");
            Console.ResetColor();
            int _choice = Validator.GetValidInt("choice");
            while (!(_choice > 0&&_choice<4))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice");
                Console.ResetColor();
                _choice = Validator.GetValidInt("choice");

            }
            int recurrance = Validator.GetValidRecurrance();
            if (recurrance== 0)
            {
                TodoList.Add(new TodoItem(date, taskHeading, desc, recurrance, "Yet to start"));
            }
            switch (_choice)
            {
                case 1:
                   
                    for (int i = 0; i < recurrance; i++)
                    {
                        TodoList.Add(new TodoItem(date.AddDays(i), taskHeading, desc,  recurrance - i - 1, "Yet to start"));
                    }
                    break;
                case 2:
                    for (int i = 0; i < recurrance; i++)
                    {
                        TodoList.Add(new TodoItem(date.AddMonths(i), taskHeading, desc,  recurrance - i-1, "Yet to start"));
                    }
                    break;
                case 3:
                    for (int i = 0; i < recurrance; i++)
                    {
                        TodoList.Add(new TodoItem(date.AddYears(i), taskHeading, desc,  recurrance - i-1, "Yet to start"));
                    }
                    break;
                default:
                    break;
                    

            }
 
            TodoList = TodoList.OrderBy(t => t.targetDate).ToList();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTask added to To-Do List Successfully");
            Console.ResetColor();
        }


        /// <summary>
        /// Function to edit existing to-do tasks of a user.
        /// </summary>
        public void EditTask()
        {
            ViewTasks();
            Console.WriteLine("\nNote the id of the task you wish to edit..");
            int id = Validator.GetValidInt("id");
            if (id < 1 || id > TodoList.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nSorry...Id Not Available");
                Console.ResetColor();
            }
            else
            {
                var item = TodoList[id - 1];
                Console.WriteLine($"{id,-5}{item.targetDate,0}{item.taskHeading,15}{item.status,30}");
                string oldtaskHeading = item.taskHeading;
                string newTaskHeading = Validator.GetValidTaskHeading();
                string newdescription = "";
                Console.WriteLine("Do you wish to edit the description?[y/n]");
                string editConfirmation = Console.ReadLine();
                while (!(editConfirmation.Equals("y") || editConfirmation.Equals("n") || editConfirmation.Equals("Y") || editConfirmation.Equals("N")))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please choose [y/n]");
                    Console.ResetColor();
                    editConfirmation = Console.ReadLine();
                }
                if (editConfirmation.Equals("y") || editConfirmation.Equals("Y"))
                {
                    Console.WriteLine("Enter the new description");
                     newdescription=Console.ReadLine();

                }
                Console.WriteLine("Do you wish to edit all recurrences ? [y/n]");
                string editAllConfirmation = Console.ReadLine();
                while (!(editAllConfirmation.Equals("y") || editAllConfirmation.Equals("n") || editAllConfirmation.Equals("Y") || editAllConfirmation.Equals("N")))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please choose [y/n]");
                    Console.ResetColor();
                    editAllConfirmation = Console.ReadLine();
                }
                if (editAllConfirmation.Equals("y") || editAllConfirmation.Equals("Y"))
                {
                    foreach (TodoItem todo in TodoList)
                    { 
                        if (todo.taskHeading.Equals(oldtaskHeading))
                        {
                            todo.taskHeading = newTaskHeading;
                            if (!newdescription.Equals(""))
                                todo.description = newdescription;

                        }
                    }
                }
                else
                {
                    item.taskHeading = newTaskHeading;
                    item.recurrance = 0;
                    if (!newdescription.Equals(""))
                        item.description = newdescription;
                    int count = 0;
                    foreach (TodoItem todo in TodoList)
                    {
                        count++;
                        if (todo.taskHeading.Equals(oldtaskHeading))
                        {
                            if(count<id)
                             todo.recurrance -= 1;
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nTask edited Successfully");
                Console.ResetColor();
                TodoList = TodoList.OrderBy(t => t.targetDate).ToList();
                ViewTasks();
            }


        }

        /// <summary>
        /// Function to update status of a to-do task of a user.
        /// </summary>
        public void UpdateStatus()
        {
            ViewTasks();
            Console.WriteLine("\nNote the id of the task you wish to update status..");
            int id = Validator.GetValidInt("id");
            if (id < 1 || id > TodoList.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nSorry...Id Not Available");
                Console.ResetColor();
            }
            else
            {
                var item = TodoList[id - 1];
                Console.WriteLine($"{id,-5}{item.targetDate,0}{item.taskHeading,10}{item.status,30}");
                Console.WriteLine("\nChoose the status of your task :");
                Console.WriteLine("\n1.Yet to start");
                Console.WriteLine("2.In-Progress");
                Console.WriteLine("3.Done");
                int choice = Validator.GetValidInt("choice");
                while (choice < 1 || choice > 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid choice");
                    Console.ResetColor();
                    choice = Validator.GetValidInt("choice");

                }
                item.status = choice == 1 ? "Yet to start" : choice == 2 ? "In-Progress" : "Done";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nStatus updated Successfully");
                Console.ResetColor();
            }

        }
        /// <summary>
        /// Function to delete  to-do tasks of a user.
        /// </summary>
        public void DeleteTask()
        {
            ViewTasks();
            Console.WriteLine("\nNote the id of the task you wish to delete ..");
            int id = Validator.GetValidInt("id");
            if (id < 1 || id > TodoList.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nSorry...Id Not Available");
                Console.ResetColor();
            }
            else
            {
                var item = TodoList[id - 1];
                Console.WriteLine($"{id,-5}{item.targetDate,0}{item.taskHeading,10}{item.status,30}");
                Console.WriteLine("Do you wish to delete this task ? [y/n]");
                string deleteConfirmation = Console.ReadLine();
                string oldtaskHeading = item.taskHeading;
                while (!(deleteConfirmation.Equals("y") || deleteConfirmation.Equals("n") || deleteConfirmation.Equals("Y") || deleteConfirmation.Equals("N")))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please choose [y/n]");
                    Console.ResetColor();
                    deleteConfirmation = Console.ReadLine();
                }
                if (deleteConfirmation.Equals("y") || deleteConfirmation.Equals("Y"))
                {
                    Console.WriteLine("Do you wish to edit all recurrences ? [y/n]");
                    string deleteAll = Console.ReadLine();
                    while (!(deleteAll.Equals("y") || deleteAll.Equals("n") || deleteAll.Equals("Y") || deleteAll.Equals("N")))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please choose [y/n]");
                        Console.ResetColor();
                        deleteAll = Console.ReadLine();
                    }
                    if (deleteAll.Equals("y") || deleteAll.Equals("Y"))
                    {
                        foreach (TodoItem todo in TodoList)
                        {
                            if (todo.taskHeading.Equals(oldtaskHeading))
                            {
                               TodoList.Remove(todo);

                            }
                        }
                    }
                    else
                    {
                        TodoList.Remove(item);
                        int count = 0;
                        foreach (TodoItem todo in TodoList)
                        {
                            count++;
                            if (todo.taskHeading.Equals(oldtaskHeading))
                            {
                                if (count < id)
                                    todo.recurrance -= 1;

                            }
                        }

                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nTask deleted Successfully");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nCanceling delete...");
                    Console.ResetColor();
                }

            }

        }






    }
}
