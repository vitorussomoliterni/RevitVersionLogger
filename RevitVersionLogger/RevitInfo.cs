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
            string version;

            switch (buildNumber)
            {
                case "20150220_1215":
                    version = "First Customer Ship,16.0.428.0";
                    break;
                case "20150506_1715":
                    version = "Service Pack 1,16.0.462.0";
                    break;
                case "20150714_1515":
                    version = "Service Pack 2,16.0.490.0";
                    break;
                case "20151007_0715":
                    version = "Release 2 (R2),16.0.1063";
                    break;
                case "20151209_0715":
                    version = "Update 1 for R2,16.0.1092.0";
                    break;
                case "20160126_1600":
                    version = "Update 2 for R2,16.0.1108.0";
                    break;
                case "20160217_1800":
                    version = "Update 3 for R2,16.0.1118.0";
                    break;
                case "20160314_0715":
                    version = "Update 4 for R2,16.0.1124.0";
                    break;
                case "20160525_1230":
                    version = "Update 5 for R2,16.0.1144.0";
                    break;
                case "20160720_0715":
                    version = "Update 6 for R2,16.0.1161.0";
                    break;
                default:
                    version = "No version found for given build number.";
                    break;
            }

            return version;
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