using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RevitVersionLogger
{
    internal class RevitInfo
    {
        private List<string> UserFolders;
        public string MostUpdateJournal { get; set; }
        private List<string> JournalsList { get; set; }
        public string BuildNumber { get; set; }
        public string RevitVersion { get; set; }

        public RevitInfo(List<string> userFolders)
        {
            UserFolders = userFolders;
            JournalsList = GetJournalsList();
            MostUpdateJournal = GetMostUpdateJournal(JournalsList);
            BuildNumber = FileReader.GetRevitVersion(MostUpdateJournal);
            RevitVersion = ConvertBuildIntoRevitVersion(BuildNumber);
        }

        private string ConvertBuildIntoRevitVersion(string buildNumber)
        {
            throw new NotImplementedException();
        }

        private string GetMostUpdateJournal(List<string> journalsList)
        {
            var mostUpdateJournal = journalsList.FirstOrDefault();

            if (journalsList.Count == 1)
            {
                return mostUpdateJournal;
            }

            foreach (var f in journalsList)
            {
                var currentFileDate = File.GetLastWriteTime(f);
                var newestFileDate = File.GetLastWriteTime(mostUpdateJournal);

                if (currentFileDate > newestFileDate)
                {
                    mostUpdateJournal = f;
                }
            }

            return mostUpdateJournal;
        }

        private List<string> GetJournalsList()
        {
            var journalsList = new List<string>();

            foreach (var folder in UserFolders)
            {
                var journalsPath = string.Format(@"{0}\AppData\Local\Autodesk\Revit\Autodesk Revit 2016\Journals", folder);

                if (Directory.Exists(journalsPath))
                {
                    journalsList = Directory.EnumerateFiles(journalsPath, "journal.*.txt").ToList();
                }
            }

            return journalsList;
        }
    }
}