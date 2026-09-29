using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kalendar.Classes
{
    public class FileService
    {
        private static readonly string folder = GetResultFolder();
        private static readonly string path = Path.Combine(folder, "data.txt");
        private static readonly object fileLock = new object();

        private static string GetResultFolder()
        {
            DirectoryInfo? directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (directory != null)
            {
                if (Directory.GetFiles(directory.FullName, "*.csproj").Length > 0)
                    return Path.Combine(directory.FullName, "Result");

                directory = directory.Parent;
            }

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Result");
        }

        static FileService()
        {
            Directory.CreateDirectory(folder);
            using (File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
            }
        }

        public static string PathFile => path;

        public static void SaveEvent(DateTime date, string description)
        {
            string line = date.ToString("dd.MM.yyyy") + "|" + description;
            lock (fileLock)
            {
                File.AppendAllText(path, line + Environment.NewLine, new UTF8Encoding(false));
            }
        }

        public static List<string> GetEvents(DateTime date)
        {
            List<string> events = new List<string>();

            string searchDate = date.ToString("dd.MM.yyyy");

            lock (fileLock)
            {
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|', 2);
                    if (parts.Length == 2 && parts[0] == searchDate)
                        events.Add(parts[1]);
                }
            }

            return events;
        }
    }
}