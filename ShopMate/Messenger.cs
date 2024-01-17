using System;
using System.IO;

namespace ShopMate
{
    public class Messenger
    {
        public void sendMessage(string userName, string message)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\inbox\\{userName}inbox.txt");
            string info = $"{message};{DateTime.Now}\n";
            File.AppendAllText(path, info);
        }
        public void watchMessage(string userName)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, $"Database\\inbox\\{userName}inbox.txt");
            string[] Inbox = File.ReadAllLines(path);

            foreach (string messageinfo in Inbox)
            {

                string[] part = messageinfo.Split(';');
                string message = part[0];
                string time = part[1];
                Console.WriteLine("*" + message + " " + time + "\n\n");
            }


            Console.WriteLine("\n\nPress ant key to go back to dashboard.");
            Console.ReadKey();
        }
    }
}
