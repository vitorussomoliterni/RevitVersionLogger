using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;

namespace RevitVersionLogger
{
    internal class RevitInfo
    {
        public string Year { get; set; }
        public string BuildNumber { get; set; }
        public string RevitVersion { get; set; }
        public string UserName { get; set; }

        public RevitInfo(string year)
        {
            Year = year;
            UserName = GetUserName();
            BuildNumber = GetBuildNumber(Year);
            RevitVersion = GetRevitVersion(BuildNumber);
        }

        private string GetBuildNumber(string year)
        {
            var path = string.Format(@"SOFTWARE\Autodesk\Revit\{0}\REVIT-05:0409", year);
            string buildNumber = null;

            using (var key = Registry.LocalMachine.OpenSubKey(path))
            {
                if (key != null)
                {
                    var value = key.GetValue("Version");
                    if (value != null)
                    {
                        var s = value.ToString();
                        var startIndex = s.IndexOf("(") + 1;
                        var length = s.LastIndexOf(")") - startIndex;
                        buildNumber = s.Substring(startIndex, length);
                    }
                }
            }

            return buildNumber;
        }

        private string GetUserName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();

            if (collection.Count == 0)
            {
                return "No user found";
            }

            var networkUsername = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];

            var startIndex = networkUsername.IndexOf("\\") + 1;
            var username = networkUsername.Substring(startIndex);

            return username;
        }

        private string GetRevitVersion(string buildNumber)
        {
            string version;

            switch (buildNumber)
            {
                case "16.0.428.0":
                    version = "First Customer Ship";
                    break;
                case "16.0.462.0":
                    version = "Service Pack 1";
                    break;
                case "16.0.490.0":
                    version = "Service Pack 2";
                    break;
                case "16.0.1063":
                    version = "Release 2 (R2)";
                    break;
                case "16.0.1092.0":
                    version = "Update 1 for R2";
                    break;
                case "16.0.1108.0":
                    version = "Update 2 for R2";
                    break;
                case "16.0.1118.0":
                    version = "Update 3 for R2";
                    break;
                case "16.0.1124.0":
                    version = "Update 4 for R2";
                    break;
                case "16.0.1144.0":
                    version = "Update 5 for R2";
                    break;
                case "16.0.1161.0":
                    version = "Update 6 for R2";
                    break;
                case "17.0.416.0":
                    version = "First Customer Ship";
                    break;
                case "17.0.476.0":
                    version = "Service Pack 1";
                    break;
                case "17.0.501.0":
                    version = "Service Pack 2";
                    break;
                case null:
                    version = "Revit " + Year + " installation not found";
                    break;
                default:
                    version = "No version found for given build number.";
                    break;
            }

            return version;
        }
    }
}