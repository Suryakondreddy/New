
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Xml.Serialization;

/// <summary>
/// ReportGenerator class pushes the data to a list of GNOSReport class
/// </summary>
internal class ReportGenerator
{

    /// <summary>
    ///  A global object to generate XML by serialise the listReport object.
    ///  GNOSReport is a class to serialised into XML elements.
    /// </summary>
    public static List<TFSTestResultsSet> listReport = new List<TFSTestResultsSet>();
    private static void ReportData(TFSTestResultsSet report)
    {
        listReport.Add(report);
    }

    /// <summary>
    /// Before running the script, invoke this method to delete previous files and data.
    /// </summary>
    public static void DeleteTemp()
    {
        string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        DirectoryInfo di = null;
        System.Security.AccessControl.DirectorySecurity security = new System.Security.AccessControl.DirectorySecurity();

        if (!Directory.Exists(Path.Combine(folderPath, "XML")))
            di = Directory.CreateDirectory(Path.Combine(folderPath, "XML"));
        else
        {
            string[] files = Directory.GetFiles(Path.Combine(folderPath, "XML"));

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }


    public static void UpdateReport(string TestPlan, string TestCaseID, string Config, string StepNo, string status,
        string stepComment, string testSuitNo, string LogFile)
    {
        TFSTestResultsSet result = new TFSTestResultsSet();
        result.TestPlanID = TestPlan;
        result.TestSuitID = testSuitNo;
        result.TestCaseID = TestCaseID;
        result.TestConfiguration = Config;
        result.TestStepID = StepNo;
        result.TestStatus = status;
        result.TestStepComment = stepComment;
        result.TestLogAttachment = LogFile;
        ReportData(result);
    }

    internal static void SerializeObject()
    {
        string val = DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss");
        try
        {
            string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DirectoryInfo di = null;
            System.Security.AccessControl.DirectorySecurity security = new System.Security.AccessControl.DirectorySecurity();

            if (!Directory.Exists(Path.Combine(folderPath, "XML")))
                di = Directory.CreateDirectory(Path.Combine(folderPath, "XML"));

            string filename = Path.Combine(folderPath, "XML", val + ".xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<TFSTestResultsSet>));
            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, listReport);
            writer.Close();
            System.Threading.Thread.Sleep(2000);
            SortXML(filename).WriteXml(Path.Combine(folderPath, "XML", DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss") + ".xml"));
            System.Threading.Thread.Sleep(2000);
            File.Delete(filename);
        }
        catch (Exception Ex)
        {
            Console.WriteLine(Ex.Message);
        }
    }

    internal static string SerializeObject(string fileName)
    {
        string val = DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss");
        string pathFile = Path.GetDirectoryName(fileName) + "\\" + val;
        try
        {
            string folderPath = Path.GetDirectoryName(pathFile);
            DirectoryInfo di = null;
            System.Security.AccessControl.DirectorySecurity security = new System.Security.AccessControl.DirectorySecurity();

            if (!Directory.Exists(Path.Combine(folderPath, "XML\\TEMP")))
                di = Directory.CreateDirectory(Path.Combine(folderPath, "XML\\TEMP"));

            string filename1 = (folderPath + "\\XML\\TEMP\\" + val + ".xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<TFSTestResultsSet>));
            TextWriter writer = new StreamWriter(filename1);
            serializer.Serialize(writer, listReport);
            writer.Close();
            string newFileName = Path.Combine(folderPath, "XML", Path.GetFileNameWithoutExtension(fileName) + " " + listReport[0].TestCaseID + "_" + val + ".xml");
            SortXML(filename1).WriteXml(newFileName);
            File.Delete(filename1);
            listReport = new List<TFSTestResultsSet>();
            return newFileName;
        }
        catch (Exception Ex)
        {
            Console.WriteLine(Ex.Message);
            return string.Empty;
        }
    }

    internal static void SerializeObject(string folderName, string fileName)
    {
        string val = DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss");
        string pathFile = folderName + "\\" + val;
        try
        {
            string folderPath = Path.GetDirectoryName(pathFile);
            DirectoryInfo di = null;
            System.Security.AccessControl.DirectorySecurity security = new System.Security.AccessControl.DirectorySecurity();

            if (!Directory.Exists(Path.Combine(folderPath, "XML\\TEMP")))
                di = Directory.CreateDirectory(Path.Combine(folderPath, "XML\\TEMP"));

            string filename1 = (folderPath + "\\XML\\TEMP\\" + val + ".xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<TFSTestResultsSet>));
            TextWriter writer = new StreamWriter(filename1);
            serializer.Serialize(writer, listReport);
            writer.Close();
            //System.Threading.Thread.Sleep(2000);
            SortXML(filename1).WriteXml(Path.Combine(folderPath, "XML", fileName + "_" + val + ".xml"));
            //System.Threading.Thread.Sleep(2000);
            File.Delete(filename1);
            listReport = new List<TFSTestResultsSet>();
        }
        catch (Exception Ex)
        {
            Console.WriteLine(Ex.Message);
        }
    }

    internal static void GenerateReport()
    {
        dsMains = new DataSet("Report");
        string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (Directory.Exists(Path.Combine(folderPath, "XML")))
        {
            DirectoryInfo d = new DirectoryInfo(Path.Combine(folderPath, "XML"));
            FileInfo[] Files = d.GetFiles("*.xml"); //Getting Text files
            foreach (FileInfo file in Files)
            {
                DataSet ds = new DataSet("ResultSet");
                ds.ReadXml(Path.Combine(folderPath, "XML", file.Name));
                dsMains.Merge(ds);
                ds = null;
            }

            MergeAllReportsIntoSingle(dsMains);

        }
    }

    /// <summary>
    /// Provide Workflows XML Path
    /// </summary>
    /// <param name="xmlPath"></param>
    internal static void GenerateReport(string xmlPath)
    {
        dsMains = new DataSet("Report");
        string folderPath = xmlPath;
        if (Directory.Exists(folderPath))
        {
            DirectoryInfo d = new DirectoryInfo(folderPath);
            FileInfo[] Files = d.GetFiles("*.xml"); //Getting XML files
            foreach (FileInfo file in Files)
            {
                DataSet ds = new DataSet("ResultSet");
                ds.ReadXml(Path.Combine(folderPath, file.Name));
                dsMains.Merge(ds);
                ds = null;
            }

            MergeAllReportsIntoSingle(dsMains);

        }
    }

    static DataSet dsMains = new DataSet("Report");

    internal static void MergeAllReportsIntoSingle(DataSet ds)
    {
        try
        {
            string folderpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!Directory.Exists(Path.Combine(folderpath, "Reports")))
                Directory.CreateDirectory(Path.Combine(folderpath, "Reports"));

            dsMains.WriteXml(Path.Combine(folderpath, "Reports", "RxWorkflows.xml"));
            //dsMains = null;

        }
        catch (Exception Ex)
        {
            Console.WriteLine(Ex.Message);
        }
    }

    private static DataTable SortXML(string fileName)
    {
        DataSet ds = new DataSet("Report");
        ds.ReadXml(fileName);

        DataTable dt1 = ds.Tables[0];
        DataTable dt2 = dt1.Clone();

        dt2.Columns["TestCaseID"].DataType = Type.GetType("System.Int32");

        foreach (DataRow dr in dt1.Rows)
        {
            dt2.ImportRow(dr);
        }
        dt2.AcceptChanges();
        DataView dv = dt2.DefaultView;
        dv.Sort = "TestStatus";

        DataTable dt = dv.ToTable();

        return dt;
    }
}
