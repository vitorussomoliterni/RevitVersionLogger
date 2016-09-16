using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RevitVersionLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            var infoList = new List<RevitInfo>();

            try
            {
                var revit2016Info = new RevitInfo("2016");
                infoList.Add(revit2016Info);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                var revit2017Info = new RevitInfo("2017");
                infoList.Add(revit2017Info);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Logger(infoList);
        }

        private static void Logger(List<RevitInfo> infoList)
        {
            try
            {
                var path = @"\\alljac-nas04\Transfer\Transfer\moliterni\logs\log.csv";
                foreach (var i in infoList)
                {
                    var line = string.Format("{0},{1},{2},Revit {3},{4},{5}", DateTime.Now.ToShortDateString(), i.UserName, Environment.MachineName, i.Year, i.RevitVersion, i.BuildNumber);
                    //using (StreamWriter w = File.AppendText(path))
                    //{
                    //    w.WriteLine(line);
                    //    Console.WriteLine(line);
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
