using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                var version = line.Substring(9, 13);

                return version;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
