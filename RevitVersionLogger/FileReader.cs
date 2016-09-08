using System;
using System.IO;

namespace RevitVersionLogger
{
    public static class FileReader
    {
        public static string GetRevitVersion(string path)
        {
            StreamReader file = new StreamReader(path);
            string line;

            try
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("Build"))
                    {
                        break;
                    }
                }

                if (string.IsNullOrEmpty(line))
                {
                    return null;
                }

                var version = line.Substring(9, 13); // Gets the Revit version installed

                return version;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                file.Close();
            }
        }
    }
}
