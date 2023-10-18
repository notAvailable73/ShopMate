using System;

namespace SPL_PROJECT
{
    public class user
    {
        public string userName;
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime date_of_birth { get; set; }
        public Cart cart;
        public user(string userName,string name, string password, string email, DateTime date)
        {
            this.userName = userName;
            this.name = name;
            this.password = password;
            this.email = email;
            this.date_of_birth = date;
            cart = new Cart();
        }
    }
}
