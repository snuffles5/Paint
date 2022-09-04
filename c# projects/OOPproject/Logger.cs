using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OOPproject
{
    
    class Logger
    {
        static string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString() + $"{ DateTime.Now.ToFileTime()}_log";
        public static void WriteLog(string message)
        {
            //string path = "c:\\temp";
            string fileName;
            string path1 = path;
            using (StreamWriter writer = new StreamWriter(path1, true))
            {
                writer.WriteLine($"{DateTime.Now} : {message}");
            }
        }
    }
}
