using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OOPproject
{
    
    class Logger
    {
        readonly static string fileName = $"{ DateTime.Now.ToFileTime()}_log.log";

        static string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString() + $"{fileName}";
        public static void WriteLog(string message)
        {
            string path1 = path;
            using (StreamWriter writer = new StreamWriter(path1, true))
            {
                writer.WriteLine($"{DateTime.Now} : {message}");
            }
        }
    }
}
