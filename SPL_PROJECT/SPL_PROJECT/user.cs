using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    internal class user
    {
        private string name {  get; set; }
        private string password { get; set; }
        private string email { get; set; }
        private DateTime date_of_birth { get; set; }

        public user(string name, string password, string email,DateTime date)
        {
            this.name = name;
            this.password = password;
            this.email = email;
            this.date_of_birth = date;

            string user_file = @"C:\ShopMate\user.txt.txt";

            
            string info = $"{name},{password},{email},{date}";
            File.AppendAllText(user_file,info);

        }
    }
}
