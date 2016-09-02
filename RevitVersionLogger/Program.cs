using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitVersionLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            var userFolders = GetAllUserFolders();
        }

        private static List<string> GetAllUserFolders()
        {
            var usersToIgnore = new string[] { "administrator", "ajc", "ajcadmin", "caduser", "all users", "default", "default user", "public" };
            var folders = Directory.EnumerateDirectories(@"C:\Users\").ToList();

            var userFolders = new List<string>();

            foreach (var dir in folders)
            {
                var directoryUser = dir.Substring(9).Trim().ToLower();

                for (int i = 0; i < usersToIgnore.Length; i++)
                {
                    if (usersToIgnore[i].Equals(directoryUser))
                    {
                        break;
                    }
                    else if (i == usersToIgnore.Length - 1)
                    {
                        userFolders.Add(dir);
                    }
                }
            }

            return userFolders;
        }
    }
}
