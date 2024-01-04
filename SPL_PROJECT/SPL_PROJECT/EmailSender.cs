using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public interface Sender
    {
        void Sent(string message, string username);
    }
    internal class EmailSender:Sender
    {
        public void Sent(string message,string userName) 
        {
            Database.createInbox(userName);
            string path = $@"C:\ShopMate\Inbox\{userName}_inbox.txt";
            File.AppendAllText(path, message);
        }
    }
}
