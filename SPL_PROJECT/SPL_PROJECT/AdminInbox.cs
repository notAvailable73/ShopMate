using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public static class AdminInbox
    {
        static List<string> Adminmessages = new List<string>();

        public static void addMessage(string message)
        {
            Adminmessages.Add(message);
        }
        public static void GetAdminInbox()
        {
            string path = $@"C:\ShopMate\Inbox\Admin_inbox.txt";
            StreamReader sr = new StreamReader(path);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                addMessage(line);
            }
            sr.Close();
        }

        public static void sendQuantityWarning(IProduct product)
        {
            string Message = $"* Warning!!...You Have Less Than 5 {product.name}s Left In Your Inventory ";
            string formattedDateTime = Database.GetTime();
            Message += $"({formattedDateTime})";
            Database.addMessage(Message, "Admin");
        }
        public static void sendEmptyInventoryWarning(IProduct product)
        {
            string Message = $"* Warning!!...You Have No {product.name}s Left In Your Inventory ";
            string formattedDateTime = Database.GetTime();
            Message += $"({formattedDateTime})";
            Database.addMessage(Message, "Admin");
        }
       
        public static string load()
        {
            string s = "";
            for (int i = Adminmessages.Count - 1; i >= 0; i--)
            {
                s += $"{Adminmessages[i]}\n\n";
            }
            return s;

        }
        public static void emptyList()
        {
            Adminmessages.Clear();
        }
    }
}
