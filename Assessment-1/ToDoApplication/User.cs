using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication
{
    public class User
    {
        public int employeeID;
        public string name;
        public User(int id,string username)
        {
            employeeID = id;
            name = username;
        }
    }
}
