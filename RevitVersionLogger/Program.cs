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

            try
            {
                var revit2016Info = new RevitInfo(userFolders, "2016");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                var revit2017Info = new RevitInfo(userFolders, "2017");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static List<string> GetAllUserFolders()
        {
            var usersToIgnore = new HashSet<string> { "administrator", "ajc", "ajcadmin", "caduser", "all users", "default", "default user", "public" };
            var folders = Directory.EnumerateDirectories(@"C:\Users\").ToList();

            folders.RemoveAll(f => usersToIgnore.Contains(f.Substring(9).Trim().ToLower())); // Removes all the users folder to ignore from the folders list

            return folders;
        }
    }
}
