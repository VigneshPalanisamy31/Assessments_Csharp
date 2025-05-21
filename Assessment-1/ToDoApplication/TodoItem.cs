using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication
{
    public class TodoItem
    {
        public string taskHeading;
        public string description;
        public DateOnly targetDate;
        public int recurrance;
        public string status;


        public TodoItem(DateOnly userdate, string heading, string desc, int recurr, string stat)
        {
            taskHeading = heading;
            description = desc;
            targetDate = userdate;
            recurrance = recurr;
            status = stat;

        }
    }
}
