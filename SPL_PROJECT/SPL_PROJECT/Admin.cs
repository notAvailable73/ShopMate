using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public class Admin
    {
        public void CreateUser(string username,string password,string mail, DateTime date)
        {
            user u1=new user(username, password, mail, date);
            Console.WriteLine($"User Created Successfully with username:{username}");
        }


    }
}
