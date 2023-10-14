using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin();

            Console.WriteLine("Press 1 to Create New Account");
            Console.WriteLine("Press 2 to Sign In to Existing Account");

            string x = Console.ReadLine();
            int command= Convert.ToInt32(x);


            if (command == 1)
            {
                Console.WriteLine("Enter UserName:");
                string username = Console.ReadLine();

                Console.WriteLine("Enter Password:");
                string password = Console.ReadLine();

                Console.WriteLine("Enter E-mail:");
                string mail = Console.ReadLine();

                Console.WriteLine("Enter Date of Birth");
                string date_of_birth = Console.ReadLine();
                DateTime date = Convert.ToDateTime(date_of_birth);

                admin.CreateUser(username, password, mail, date);
            }

            else if (command == 2)
            {
                Console.WriteLine("Enter UserName:");
                string username = Console.ReadLine();

                Console.WriteLine("Enter Password:");
                string password = Console.ReadLine();

                string user_file = @"C:\ShopMate\user.txt.txt";
                
                if (File.Exists(user_file))
                {
                    StreamReader sr = new StreamReader(user_file);

                    string line;
                    bool flag= false;
                    string user_name;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] s = null;
                        s = line.Split(',');

                        string name = s[0];
                        string pass = s[1];
                        string main = s[2];
                        DateTime date = Convert.ToDateTime(s[3]);

                        if(name==username && password==pass)
                        {
                            flag = true;
                            user_name = name;
                            break;
                        }
                    }

                    sr.Close();

                    if (flag) 
                    {
                        Console.WriteLine($"Welcome User:{username}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Username or Password");
                    }
                }

            }
            
            Console.ReadLine();
        }
    }
}
