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
            GetRevitVersion(userFolders);
        }

        private static void GetRevitVersion(List<string> userFolders)
        {
            foreach (var user in userFolders)
            {
                var userName = user.Substring(9);
                var journalsPath = user + @"\AppData\Local\Autodesk\Revit\Autodesk Revit 2016\Journals";
                try
                {
                    if (Directory.Exists(journalsPath))
                    {
                        var journalFile = GetLatestJournal(journalsPath);

                        if (journalFile == null)
                        {
                            throw new FileNotFoundException("No journals found in {0} folder", userName);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        private static string GetLatestJournal(string path)
        {
            var journals = Directory.EnumerateFiles(path, "journal.*.txt", SearchOption.TopDirectoryOnly);

            var latestJournal = journals.LastOrDefault();
            return latestJournal;
        }

        private static List<string> GetAllUserFolders()
        {
            var usersToIgnore = new HashSet<string> { "administrator", "ajc", "ajcadmin", "caduser", "all users", "default", "default user", "public" };
            var folders = Directory.EnumerateDirectories(@"C:\Users\").ToList();

            folders.RemoveAll(f => usersToIgnore.Contains(f.Substring(9).Trim().ToLower()));

            return folders;
        }
    }
}
