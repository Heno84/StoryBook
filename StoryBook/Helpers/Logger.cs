using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StoryBook.Helpers
{
    public class Logger
    {
        private static string _filePath;

        public Logger(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                File.CreateText(filePath).Close();
            }
        }

        public static void Log(string logType, string message)
        {
            using (StreamWriter writer = File.AppendText(_filePath))
            {
                writer.WriteLine(DateTime.Now);
                writer.WriteLine(logType);
                writer.WriteLine(message);
                writer.WriteLine("---------------------------------------------");
                writer.WriteLine("\n");
            }
        }
    }

    class LogType
    {
        public static string success = "Success:";
        public static string error = "Error:";
        public static string warning = "Warning:";
        public static string info = "Info:";
    }
}