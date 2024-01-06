using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_PROJECT
{
    public class inbox
    {
        List<string> messages = new List<string>();

        public void addMessage(string message)
        {
            messages.Add(message);
        }

        public void SendWelcomeMessage(string userName)
        {
            string Message = $"* Welcome To ShopMate {userName} ";            
            string formattedDateTime = Database.GetTime();           
            Message += $"({formattedDateTime})";
            Database.addMessage(Message,userName);
        }
        public void sendPurchaseMessage(string userName) {            
            string Message = $"* You Have Successfully Bought Products From Your Cart.Thank you for shopping with ShopMate  ";
            string formattedDateTime = Database.GetTime();
            Message += $"({formattedDateTime})";
            Database.addMessage(Message, userName);
        }
        public void sendRemindProduct(string userName)
        {
            string Message = $"* OOPS...You Forgot To Buy Wonderful Products In Your Cart ";
            string formattedDateTime = Database.GetTime();
            Message += $"({formattedDateTime})";
            Database.addMessage(Message, userName);
        }


        public string load()
        {
            string s = "";
            for (int i = messages.Count - 1; i >= 0; i--)
            {
                s += $"{messages[i]}\n\n";
            }
            return s;

        }
        public void emptyList()
        {
            messages.Clear();
        }
    }
}
