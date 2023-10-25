using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Configuration;
using System.Reflection;
using System.Threading;

namespace TFS.Agent.Run
{

    public class Program
    {
        static string path = string.Empty;
        static GN.TFSAPI.Core.TFSAPI tfsAPI = new GN.TFSAPI.Core.TFSAPI();

        static void Main(string[] args)
        {
            string folderpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!Directory.Exists(Path.Combine(folderpath, "Reports")))
                Directory.CreateDirectory(Path.Combine(folderpath, "Reports"));

            path = Path.Combine(folderpath, "Reports");

            #region File Watcher
            ////Source Watcher
            //Console.WriteLine("Application has started and monitoring for Ranorex Workflow XMLs");
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //FileSystemWatcher watcher2 = new FileSystemWatcher();
            //string xmlsPath = ConfigurationManager.AppSettings["WorkFlowsXMLsPath"];
            //watcher2.Path = xmlsPath;
            //watcher2.EnableRaisingEvents = true;
            //watcher2.NotifyFilter = NotifyFilters.FileName;
            //watcher2.Filter = "*.xml";
            //watcher2.Created += new FileSystemEventHandler(OnChangedXMLS);

            // Report watcher
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //FileSystemWatcher watcher1 = new FileSystemWatcher();
            //string filePath = path;
            //watcher1.Path = filePath;
            //watcher1.EnableRaisingEvents = true;
            //watcher1.NotifyFilter = NotifyFilters.FileName;
            //watcher1.Filter = "*.xml";
            //watcher1.Created += new FileSystemEventHandler(OnChanged);

            //// wait - not to end
            //new System.Threading.AutoResetEvent(false).WaitOne();
            #endregion

            tfsAPI.TFSUserName = ConfigurationManager.AppSettings["TFSUserName"];
            tfsAPI.TFSPassword = ConfigurationManager.AppSettings["TFSPWD"];

            GetFileAndRunTFSAPI();

            //Thread thread1 = new Thread(GetFileAndRunTFSAPI);
            //thread1.Start();
        }

        private static void Watcher_Deleted()
        {
            Console.WriteLine("Report File has been deleted");
            Console.ForegroundColor = ConsoleColor.Green;

        }

        private static void UpdateTestResults(string fileName)
        {
            List<string> localFalseTestCases = new List<string>();
            List<string> localTrueTestCases = new List<string>();
            string _passedTrueTestCase = "";

            try
            {
                DataSet ds = new DataSet();

                Console.WriteLine("Agent started reading the file");

                ds.ReadXml(fileName);

                bool hasRows = ds.Tables.Cast<DataTable>()
                                   .Any(table => table.Rows.Count != 0);

                if (!hasRows)
                {
                    Console.WriteLine("Report has no rows");
                    //Console.ReadKey();
                    return;
                }

                var queryTestCasesinFalse = from row in ds.Tables[0].AsEnumerable()
                                            where (row.Field<string>("TestStatus").ToLower() == "FAIL".ToLower() || row.Field<string>("TestStatus").ToLower() == "FALSE".ToLower())
                                            select row;

                for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                {
                    localTrueTestCases.Add(ds.Tables[0].Rows[iRow]["TestCaseID"].ToString());
                }
                localTrueTestCases = localTrueTestCases.Distinct().ToList();
                
                foreach (var item in queryTestCasesinFalse.ToList())
                {
                    if (ds.Tables[0].Columns.Contains("TestSuitID"))
                    {
                        localFalseTestCases.Add(item.ItemArray[2].ToString());
                        localTrueTestCases.Remove(item.ItemArray[2].ToString());
                    }
                    else
                    {
                        localFalseTestCases.Add(item.ItemArray[1].ToString());
                        localTrueTestCases.Remove(item.ItemArray[1].ToString());
                    }
                    
                }
                localFalseTestCases = localFalseTestCases.Distinct().ToList();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < localFalseTestCases.Count; j++)
                    {
                        if (ds.Tables[0].Rows[i]["TestCaseID"].ToString() == localFalseTestCases[j].ToString())
                        {
                            tfsAPI.TestCaseStepResult = ds.Tables[0].Rows[i]["TestStatus"].ToString();
                            tfsAPI.TestPlanNumber = ds.Tables[0].Rows[i]["TestPlanID"].ToString();
                            if (ds.Tables[0].Columns.Contains("TestSuitID"))
                                tfsAPI.TestSuitNumber = ds.Tables[0].Rows[i]["TestSuitID"].ToString();
                            tfsAPI.TestCaseNummber = ds.Tables[0].Rows[i]["TestCaseID"].ToString();
                            tfsAPI.TestCaseConfiguration = ds.Tables[0].Rows[i]["TestConfiguration"].ToString();
                            tfsAPI.TestCaseActionTitle = ds.Tables[0].Rows[i]["TestStepID"].ToString();
                            if (ds.Tables[0].Columns.Contains("TestStepComment"))
                                tfsAPI.StepComment = Convert.ToString(ds.Tables[0].Rows[i]["TestStepComment"]);

                            string localAttchments = null;
                            if (ds.Tables[0].Columns.Contains("LogFile"))
                                localAttchments = Path.Combine(Path.GetDirectoryName(fileName),
                                    Convert.ToString(ds.Tables[0].Rows[i]["LogFile"]));
                            tfsAPI.TestAttachment = localAttchments;


                            Console.WriteLine("->" +
                                                     " Test Plan - " + tfsAPI.TestPlanNumber +
                                                     ",Test Suit - " + tfsAPI.TestSuitNumber +
                                                     ",Test Case - " + tfsAPI.TestCaseNummber +
                                                     ",Test Configuration - " + tfsAPI.TestCaseConfiguration +
                                                     ",Test Step No - " + tfsAPI.TestCaseActionTitle +
                                                     ",Test Result - " + tfsAPI.TestCaseStepResult
                                                     );

                            tfsAPI.UpdateTFSTestPlan();
                        }
                    }


                    for (int k = 0; k < localTrueTestCases.Count; k++)
                    {
                        if (ds.Tables[0].Rows[i]["TestCaseID"].ToString() == localTrueTestCases[k].ToString())
                        {
                            if (_passedTrueTestCase != ds.Tables[0].Rows[i]["TestCaseID"].ToString())
                            {
                                tfsAPI.TestCaseStepResult = "NA";
                                tfsAPI.TestPlanNumber = ds.Tables[0].Rows[i]["TestPlanID"].ToString();
                                if (ds.Tables[0].Columns.Contains("TestSuitID"))
                                    tfsAPI.TestSuitNumber = ds.Tables[0].Rows[i]["TestSuitID"].ToString();
                                tfsAPI.TestCaseNummber = ds.Tables[0].Rows[i]["TestCaseID"].ToString();
                                tfsAPI.TestCaseConfiguration = ds.Tables[0].Rows[i]["TestConfiguration"].ToString();
                                tfsAPI.TestCaseActionTitle = ds.Tables[0].Rows[i]["TestStepID"].ToString();
                                if (ds.Tables[0].Columns.Contains("TestStepComment"))
                                    tfsAPI.StepComment = Convert.ToString(ds.Tables[0].Rows[i]["TestStepComment"]);

                                Console.WriteLine("->" +
                                                         " Test Plan - " + tfsAPI.TestPlanNumber +
                                                         ",Test Suit - " + tfsAPI.TestSuitNumber +
                                                         ",Test Case - " + tfsAPI.TestCaseNummber +
                                                         ",Test Configuration - " + tfsAPI.TestCaseConfiguration +
                                                         ",Test Step No - " + tfsAPI.TestCaseActionTitle +
                                                         ",Test Result - " + tfsAPI.TestCaseStepResult
                                                         );

                                tfsAPI.UpdateTFSTestPlan();
                            }

                            _passedTrueTestCase = ds.Tables[0].Rows[i]["TestCaseID"].ToString();
                        }
                    }


                }
                localTrueTestCases = localFalseTestCases = null;
                //Console.ReadKey()
                ;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Console.ReadKey();
            }
        }


        private static void FileArchive(string fileName)
        {
            string startPath = fileName; // path + "\\" + "RxWorkflows.xml";

            if (!Directory.Exists(path + "\\Archive"))
                Directory.CreateDirectory(path + "\\Archive");

            File.Copy(startPath, path + "\\Archive\\" + Path.GetFileNameWithoutExtension(fileName) + "_" + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss") + ".xml", true);

            File.Delete(fileName);

            Console.WriteLine("Report File Archived Successfully !");
        }

        private static void MergeWorkFlowsXMLs()
        {
            string xmlsPath = ConfigurationManager.AppSettings["WorkFlowsXMLsPath"];
            RxResultUpdator.TestResults.GetReport(xmlsPath);
        }

        private static void GetFileAndRunTFSAPI()
        {
            string xmlsPath = ConfigurationManager.AppSettings["WorkFlowsXMLsPath"];

            DataSet dsMains = new DataSet("Report");
            string folderPath = xmlsPath;
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo d = new DirectoryInfo(folderPath);
                FileInfo[] Files = d.GetFiles("*.xml"); //Getting XML files

                foreach (FileInfo file in Files)
                {
                    UpdateTestResults(file.FullName);
                    //FileArchive(file.FullName);
                }

            }
            else
            {

            }
        }
    }
}