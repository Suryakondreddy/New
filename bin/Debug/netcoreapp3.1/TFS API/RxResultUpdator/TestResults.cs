using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RxResultUpdator
{
    public static class TestResults
    {
        /// <summary>
        /// Test Plan number as in TFS
        /// </summary>
        public static string TestPlanNumber { private get; set; }

        /// <summary>
        /// Test Case Number number as in TFS
        /// </summary>
        public static string TestCaseNumber { private get; set; }
        /// <summary>
        /// Configuration of a Test case as in TFS
        /// </summary>
        public static string Configuration { private get; set; }

        /// <summary>
        /// Test Step number as in TFS, usually starts from '2'
        /// </summary>
        public static string StepNumber { private get; set; }
        /// <summary>
        /// Step - Result. 
        /// </summary>
        public static string Status { private get; set; }

        /// <summary>
        /// Assign if there are any comments to each step.
        /// </summary>
        public static string StepComment { private get; set; }

        public static string TestSuitNumber { private get; set; }

        public static string LogFile { private get; set; }

        /// <summary>
        /// UpdateReport method has to be invoked when each step runs in an execution.
        /// </summary>
        public static void UpdateReport()
        {
            if (!string.IsNullOrEmpty(TestCaseNumber))
                ReportGenerator.UpdateReport(TestPlanNumber, TestCaseNumber, Configuration, StepNumber, Status.ToUpper(), StepComment, TestSuitNumber, LogFile);
        }

        /// <summary>
        /// GenerateXML has to be invoked when a workflow execution completed.
        /// </summary>
        public static void GenerateXML()
        {
            ReportGenerator.SerializeObject();
        }

        /// <summary>
        /// GenerateXML has to be invoked when a workflow execution completed.
        /// </summary>
        public static string GenerateXML(string fileName)
        {
           return ReportGenerator.SerializeObject(fileName);
        }

        public static void GenerateXML(string folderName, string fileName)
        {
            ReportGenerator.SerializeObject(folderName, fileName);
        }

        /// <summary>
        /// GetReport method has to be invoked only when 'all' workflows execution completes from a script / TestSuit
        /// </summary>
        public static void GetReport()
        {
            ReportGenerator.GenerateReport();
        }

        /// <summary>
        /// GetReport method has to be invoked only when 'all' workflows execution completes from a script / TestSuit
        /// </summary>
        public static void GetReport(string xmlsPath)
        {
            ReportGenerator.GenerateReport(xmlsPath);
            DeleteWorkflowsXMLs(xmlsPath);
        }

        //public static void GenerateWorkflowXMLSFromTemp(string fileName)
        //{
        //    ReportGenerator.GenerateWorkfowXMLSFromTemp(fileName);
        //}

        public static void DeleteWorkflowsXMLs(string configPath)
        {
            string folderPath = configPath;
            DirectoryInfo di = null;
            System.Security.AccessControl.DirectorySecurity security = new System.Security.AccessControl.DirectorySecurity();

            if (!Directory.Exists(Path.Combine(folderPath)))
                di = Directory.CreateDirectory(Path.Combine(folderPath));
            else
            {
                string[] files = Directory.GetFiles(Path.Combine(folderPath));

                foreach (var file in files)
                {
                    FileArchive(file);
                    File.Delete(file);
                }
            }
        }


        private static void FileArchive(string path)
        {
            string startPath = path;
            string archivepath = Path.GetDirectoryName(path) + "\\Archive";
            if (!Directory.Exists(archivepath))
                Directory.CreateDirectory(archivepath);

            File.Copy(startPath, archivepath + "\\" + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss") + ".xml");

            Console.WriteLine("Report File has been archived Successfully!! ");
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        /// <summary>
        /// Copy error log files to TFS API Agent to pick up
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationFilePath"></param>
        public static void CopyLogFile(string sourceFilePath, string destinationFilePath)
        {
            File.Copy(sourceFilePath, Path.Combine(destinationFilePath, "XML", Path.GetFileName(sourceFilePath)), true);
        }
    }
}
