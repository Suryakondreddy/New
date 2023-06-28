using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System.Diagnostics;
using System.Threading;
using OfficeOpenXml;
using System.IO;
using OpenQA.Selenium.Support.UI;
using com.sun.xml.@internal.bind.v2.model.core;
using System.Linq;
using jdk.nashorn.@internal.ir;
using WindowsInput.Native;
using WindowsInput;
using AventStack.ExtentReports;
using System.Xml;
using OpenQA.Selenium.Appium;
using AventStack.ExtentReports.Model;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;

namespace AppiumWinApp
{
    internal class FunctionLibrary
    {
        string[] strArray;
        string[] strArrayVal;
        private static ExtentTest test;
        public static String textDir = Directory.GetCurrentDirectory();



        /** Click operations by using Automation id's and names **/
        public void clickOnAutomationId(WindowsDriver<WindowsElement> session, string name, string id)
        {
            var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
            var div = wait.Until(ExpectedConditions.ElementIsVisible(By.Name(name)));
            session.FindElementByAccessibilityId(id).Click();
        }

        /** Click operations by using Automation id's **/
        public void clickOnElementWithIdonly(WindowsDriver<WindowsElement> session, string id)
        {
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(80));
            var txtLocation = session.FindElementByAccessibilityId(id);
            waitForMe.Until(session => txtLocation.Enabled);
            session.FindElementByAccessibilityId(id).Click();
        }

        /* Click by using Name locator */

        public void clickOnAutomationName(WindowsDriver<WindowsElement> session, string name)
        {
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
            var txtLocation = session.FindElementByName(name);
            waitForMe.Until(session => txtLocation.Displayed);
            session.FindElementByName(name).Click();
        }


        /** **/
        public WindowsDriver<WindowsElement> funtionRecurse(WindowsDriver<WindowsElement> session, string name)
        {

            try
            {
                InputSimulator sim = new InputSimulator();
                WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(70));
                var txtLocation = session.FindElementByAccessibilityId(name);
                waitForMe.Until(session => txtLocation.Displayed);
                Thread.Sleep(10000);

                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            }
            catch (Exception e)
            {
                funtionRecurse(session, name);

            }

            Thread.Sleep(8000);
            return session;
        }

        /** Wait for the element name is enable **/
        public WindowsDriver<WindowsElement> functionWaitForName(WindowsDriver<WindowsElement> session, string name)
        {

            do
            {

                try
                {
                    WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
                    var txtLocation = session.FindElementByName(name);
                    waitForMe.Until(session => txtLocation.Enabled);
                    session.FindElementByName(name).Click();
                    return session;
                }

                catch (Exception e)
                {
                    functionWaitForName(session, name);
                }
                break;

            } while (!(session.FindElementByName(name).Enabled));

            return session;
        }


        /** Wait for the Id is enable **/
        public WindowsDriver<WindowsElement> functionWaitForId(WindowsDriver<WindowsElement> session, string name)
        {
            do
            {
                try
                {
                    Thread.Sleep(8000);
                    WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
                    var txtLocation = session.FindElementByAccessibilityId(name);
                    waitForMe.Until(session => txtLocation.Enabled);
                    session.FindElementByAccessibilityId(name).Click();
                    return session;
                }

                catch (Exception e)
                {
                    functionWaitForId(session, name);
                }
                break;

            } while (!(session.FindElementByAccessibilityId(name).Enabled));

            return session;
        }
        /** Wait for the duplicate Id is enable **/
        public WindowsDriver<WindowsElement> functionWaitForIdduplicate(WindowsDriver<WindowsElement> session, string name)
        {

            try
            {

                do
                {
                    session.SwitchTo().Window(session.WindowHandles.First());
                    Thread.Sleep(8000);
                } while (!(session.FindElementByAccessibilityId(name).Enabled));
            }

            catch (Exception e)
            {
                functionWaitForIdduplicate(session, name);
            }

            return session;
        }

        /** Wait for the element name is enable **/
        public WindowsDriver<WindowsElement> waitForElement(WindowsDriver<WindowsElement> session, string name)
        {

            do
            {
                try
                {
                    WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
                    Thread.Sleep(4000);
                    var txtLocation = session.FindElementByName(name);
                    waitForMe.Until(session => txtLocation.Enabled);
                    session.FindElementByName(name).Click();
                    return session;
                }

                catch (Exception e)
                {
                    waitForElement(session, name);
                }
                break;

            } while (!(session.FindElementByName(name).Enabled));

            return session;
        }

        /** Wait for the Clickable element is enable **/
        public WindowsDriver<WindowsElement> waitForElementToBeClickable(WindowsDriver<WindowsElement> session, string name)
        {
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(400));
            var sts = waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.Name(name)));
            session.FindElementByName(name).Click();
            return session;
        }
        /** Wait for the Clickable Id is enable **/
        public WindowsDriver<WindowsElement> waitForIdToBeClickable(WindowsDriver<WindowsElement> session, string name)
        {
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(400));
            var sts = waitForMe.Until(ExpectedConditions.ElementToBeClickable(MobileBy.AccessibilityId(name)));
            session.FindElementByAccessibilityId(name).Click();
            return session;
        }

        /** Wait until the existable element is display **/
        public WindowsDriver<WindowsElement> waitUntilElementExists(WindowsDriver<WindowsElement> session, string name, int id)
        {
            switch (id)
            {
                case 1:

                    do
                    {

                        try
                        {
                            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(90));
                            Thread.Sleep(8000);
                            var txtLocation = session.FindElementByAccessibilityId(name);
                            waitForMe.Until(session => txtLocation.Displayed);
                            return session;
                        }

                        catch (Exception e)
                        {
                            waitUntilElementExists(session, name, id);
                        }
                        break;

                    } while (!(session.FindElementByAccessibilityId(name).Displayed));

                    break;
                case 0:

                    do
                    {

                        try
                        {
                            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
                            Thread.Sleep(8000);
                            var txtLocation = session.FindElementByName(name);
                            waitForMe.Until(session => txtLocation.Displayed);
                            return session;
                        }

                        catch (Exception e)
                        {
                            waitUntilElementExists(session, name, id);
                        }
                        break;

                    } while (!(session.FindElementByName(name).Displayed));

                    break;
            }

            return session;
        }

        /** To click on the Close texts in Testwork flow **/
        public static WindowsDriver<WindowsElement> clickOnCloseTestFlow(WindowsDriver<WindowsElement> session, string windowName)
        {

            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("platformName", "Windows");
            desktopCapabilities.SetCapability("app", "Root");
            desktopCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
            WindowsElement applicationWindow = null;
            Thread.Sleep(6000);
            var openWindows = session.FindElementsByClassName("Window");


            if (windowName == "Program Selection")
            {
                openWindows = session.FindElementsByName("Program Selection");
                Thread.Sleep(2000);
            }
            else
            {
                openWindows = session.FindElementsByClassName("Window");
                Thread.Sleep(2000);
            }


            foreach (var window in openWindows)
            {
                string txt = window.GetAttribute("Name");
                Thread.Sleep(4000);

                if (window.GetAttribute("Name").StartsWith(windowName))
                {
                    applicationWindow = window;
                    Thread.Sleep(2000);

                    break;
                }
            }

            Thread.Sleep(4000);

            // Attaching to existing Application Window

            string topLevelWindowHandle = null;

            try
            {
                topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            }

            catch (Exception e)
            {
                topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            }

            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");

            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "WindowsPC");
            capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities);


            return session;

        }

        /**To kill the opeartions **/
        public void processKill(string name)
        {
            Process[] processCollection = Process.GetProcesses();

            foreach (Process proc in processCollection)
            {
                Console.WriteLine(proc);
                if (proc.ProcessName == name)
                {
                    proc.Kill();

                }
            }
        }



        /** To verify the files If file is there tehn select last elements in log file and verify the Capture & Restore time **/
        public void fileVerify(string path, ExtentTest test, string operationVar)
        {
            if (File.Exists(path))
            {
                string last = File.ReadLines(path).Last();
                Console.WriteLine(last);
                String str = last;
                var result = str.LastIndexOf(':');
                int lastDotIndex = str.LastIndexOf(":", System.StringComparison.Ordinal);
                string firstPart = str.Remove(lastDotIndex);
                string secondPart = str.Substring(lastDotIndex + 1, str.Length - firstPart.Length - 1);
                int timeTaken = 0;



                if (result != null)
                {
                    try
                    {
                        timeTaken = Convert.ToInt32(secondPart.Replace(" ", "").Replace("|", ""));
                    }
                    catch (Exception ex)
                    {

                        string[] lines = File.ReadAllLines(path);

                        foreach (string line in lines)
                        {
                            Console.WriteLine(line);

                            if (line.Contains(operationVar))
                            {
                                str = line;
                                result = str.LastIndexOf(':');
                                lastDotIndex = str.LastIndexOf(":", System.StringComparison.Ordinal);
                                firstPart = str.Remove(lastDotIndex);
                                secondPart = str.Substring(lastDotIndex + 1, str.Length - firstPart.Length - 1);
                                timeTaken = 0;
                                timeTaken = Convert.ToInt32(secondPart.Replace(" ", "").Replace("|", ""));

                            }
                        }
                    }


                    if (timeTaken < 41000)
                    {
                        Console.WriteLine(operationVar + " Operation performed in desired time");
                        test.Pass(operationVar + " Desired Time : " + timeTaken);
                    }
                    else
                    {
                        Console.WriteLine("Failed");
                        test.Fail(operationVar + " Desired Time : " + timeTaken);
                    }
                }
            }

            else
            {
                Console.WriteLine("file not found");
            }

        }


        /** To get the Device information into the Excel sheet **/

        public void getDeviceInfo(WindowsDriver<WindowsElement> session)
        {
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);
            var labels = session.FindElementsByClassName("Text");
            strArray = new string[labels.Count];
            int i = 0;

            foreach (var label in labels)
            {
                Console.Write(label.Text);

                strArray[i] = label.Text;
                i = i + 1;
            }

            var labelValue = session.FindElementsByClassName("TextBox");
            i = 0;
            strArrayVal = new string[labelValue.Count];

            foreach (var label in labelValue)
            {
                Console.Write(label.Text.ToString());
                strArrayVal[i] = label.Text.ToString();
                i = i + 1;

            }


            /*Excel */


            String textDir = Directory.GetCurrentDirectory();

            var path = textDir + "\\" + strArrayVal[0] + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {

                //Set some properties of the Excel document

                excelPackage.Workbook.Properties.Author = "Test S&R Tool";
                excelPackage.Workbook.Properties.Title = "Device Info Parameters";
                excelPackage.Workbook.Properties.Subject = "Write in Excel";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                //Create the WorkSheet

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                for (i = 0; i < strArray.Length; i++)
                {
                    worksheet.Cells[i + 1, 1].Value = strArray[i]; // Column= 1, Row =3

                }
                for (i = 0; i < strArrayVal.Length; i++)
                {
                    // Column= 1, Row =3

                    worksheet.Cells[i + 4, 2].Value = strArrayVal[i]; //*adding data


                }


                //Save your file

                FileInfo fi = new FileInfo(path);
                excelPackage.SaveAs(fi);
            }
        } // Func getDevicInfo

        /**** Function to read values from CSV file ****/


        /** To get the device firmware into the Excel sheet **/
        public void getDeviceInfoForFirmware(WindowsDriver<WindowsElement> session)
        {
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);
            var labels = session.FindElementsByClassName("Text");
            strArray = new string[labels.Count];
            int i = 0;

            foreach (var label in labels)
            {
                Console.Write(label.Text);

                strArray[i] = label.Text;
                i = i + 1;
            }

            var labelValue = session.FindElementsByClassName("TextBox");
            i = 0;
            strArrayVal = new string[labelValue.Count];

            foreach (var label in labelValue)
            {
                Console.Write(label.Text.ToString());
                strArrayVal[i] = label.Text.ToString();
                i = i + 1;

            }


            /*Excel */


            String textDir = Directory.GetCurrentDirectory();

            var path = textDir + "\\" + strArrayVal[0] + "1" + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {

                //Set some properties of the Excel document

                excelPackage.Workbook.Properties.Author = "Test S&R Tool";
                excelPackage.Workbook.Properties.Title = "Device Info Parameters";
                excelPackage.Workbook.Properties.Subject = "Write in Excel";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                //Create the WorkSheet

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                for (i = 0; i < strArray.Length; i++)
                {
                    worksheet.Cells[i + 1, 1].Value = strArray[i]; // Column= 1, Row =3

                }
                for (i = 0; i < strArrayVal.Length; i++)
                {
                    // Column= 1, Row =3

                    worksheet.Cells[i + 4, 2].Value = strArrayVal[i]; //*adding data


                }


                //Save your file

                FileInfo fi = new FileInfo(path);
                excelPackage.SaveAs(fi);
            }
        } // Func getDevicInfo


        /** To read the CSV files **/
        public static String[] readCSVFile()
        {

            using (var reader = new StreamReader(textDir + "\\TestData.csv"))
            {

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();

                String[] csvVal = new string[100];
                int i = 0;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    listB.Add(values[1]);

                    csvVal[i] = values[0];
                    csvVal[i + 1] = values[1];

                    i = i + 2;
                }
                return csvVal;
            }

        }// Read CSV File function


        /** To Compare the Dump files with attached attribute values **/
        public void dumpCompare1(string device, ExtentTest test)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Device C.xml");

            XmlDocument doc1 = new XmlDocument();
            doc1.Load("C:\\Device D.xml");

            /* XML file 1 Attributes */
            string[] DfsInitLayoutItem = new string[5];
            string[] FittingSoftwareInfoSpace = new string[5];
            string[] OsPreset = new string[5];
            string[] PresetTable = new string[5];
            string[] GlobalPreset = new string[5];
            string[] CombinedPreset = new string[5];
            string[] GattDatabase = new string[5];
            string[] PersistentDataSpace = new string[5];

            /* XML file 2 Attributes */
            string[] DfsInitLayoutItem1 = new string[5];
            string[] FittingSoftwareInfoSpace1 = new string[5];
            string[] OsPreset1 = new string[5];
            string[] PresetTable1 = new string[5];
            string[] GlobalPreset1 = new string[5];
            string[] CombinedPreset1 = new string[5];
            string[] GattDatabase1 = new string[5];
            string[] PersistentDataSpace1 = new string[5];


            string[] arrayVal = new string[] { "Type", "StartAddress", "Size", "IsSystem", "Data" };
            string displayName = null;
            string type = null;
            string startaddress = null;
            string size = null;
            string isSystem = null;
            string data = null;

            var txt = doc.DocumentElement.ChildNodes;


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                displayName = node.Attributes["DisplayName"].InnerText; //or loop through its children as well


                switch (displayName)
                {
                    case "DfsInitLayoutItem":

                        DfsInitLayoutItem[0] = node.Attributes["Type"].InnerText;
                        DfsInitLayoutItem[1] = node.Attributes["StartAddress"].InnerText;
                        DfsInitLayoutItem[2] = node.Attributes["Size"].InnerText;
                        DfsInitLayoutItem[3] = node.Attributes["IsSystem"].InnerText;
                        DfsInitLayoutItem[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "FittingSoftwareInfoSpace":

                        FittingSoftwareInfoSpace[0] = node.Attributes["Type"].InnerText;
                        FittingSoftwareInfoSpace[1] = node.Attributes["StartAddress"].InnerText;
                        FittingSoftwareInfoSpace[2] = node.Attributes["Size"].InnerText;
                        FittingSoftwareInfoSpace[3] = node.Attributes["IsSystem"].InnerText;
                        FittingSoftwareInfoSpace[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "OsPreset":

                        OsPreset[0] = node.Attributes["Type"].InnerText;
                        OsPreset[1] = node.Attributes["StartAddress"].InnerText;
                        OsPreset[2] = node.Attributes["Size"].InnerText;
                        OsPreset[3] = node.Attributes["IsSystem"].InnerText;
                        OsPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "PresetTable":

                        PresetTable[0] = node.Attributes["Type"].InnerText;
                        PresetTable[1] = node.Attributes["StartAddress"].InnerText;
                        PresetTable[2] = node.Attributes["Size"].InnerText;
                        PresetTable[3] = node.Attributes["IsSystem"].InnerText;
                        PresetTable[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "GlobalPreset":

                        GlobalPreset[0] = node.Attributes["Type"].InnerText;
                        GlobalPreset[1] = node.Attributes["StartAddress"].InnerText;
                        GlobalPreset[2] = node.Attributes["Size"].InnerText;
                        GlobalPreset[3] = node.Attributes["IsSystem"].InnerText;
                        GlobalPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "CombinedPreset":

                        CombinedPreset[0] = node.Attributes["Type"].InnerText;
                        CombinedPreset[1] = node.Attributes["StartAddress"].InnerText;
                        CombinedPreset[2] = node.Attributes["Size"].InnerText;
                        CombinedPreset[3] = node.Attributes["IsSystem"].InnerText;
                        CombinedPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "GattDatabase":

                        GattDatabase[0] = node.Attributes["Type"].InnerText;
                        GattDatabase[1] = node.Attributes["StartAddress"].InnerText;
                        GattDatabase[2] = node.Attributes["Size"].InnerText;
                        GattDatabase[3] = node.Attributes["IsSystem"].InnerText;
                        GattDatabase[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "PersistentDataSpace":

                        PersistentDataSpace[0] = node.Attributes["Type"].InnerText;
                        PersistentDataSpace[1] = node.Attributes["StartAddress"].InnerText;
                        PersistentDataSpace[2] = node.Attributes["Size"].InnerText;
                        PersistentDataSpace[3] = node.Attributes["IsSystem"].InnerText;
                        PersistentDataSpace[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                }

            }

            /****File 2 Reading values *****/

            foreach (XmlNode node in doc1.DocumentElement.ChildNodes)
            {
                displayName = node.Attributes["DisplayName"].InnerText; //or loop through its children as well

                switch (displayName)
                {
                    case "DfsInitLayoutItem":

                        DfsInitLayoutItem1[0] = node.Attributes["Type"].InnerText;
                        DfsInitLayoutItem1[1] = node.Attributes["StartAddress"].InnerText;
                        DfsInitLayoutItem1[2] = node.Attributes["Size"].InnerText;
                        DfsInitLayoutItem1[3] = node.Attributes["IsSystem"].InnerText;
                        DfsInitLayoutItem1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "FittingSoftwareInfoSpace":

                        FittingSoftwareInfoSpace1[0] = node.Attributes["Type"].InnerText;
                        FittingSoftwareInfoSpace1[1] = node.Attributes["StartAddress"].InnerText;
                        FittingSoftwareInfoSpace1[2] = node.Attributes["Size"].InnerText;
                        FittingSoftwareInfoSpace1[3] = node.Attributes["IsSystem"].InnerText;
                        FittingSoftwareInfoSpace1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "OsPreset":

                        OsPreset1[0] = node.Attributes["Type"].InnerText;
                        OsPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        OsPreset1[2] = node.Attributes["Size"].InnerText;
                        OsPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        OsPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "PresetTable":

                        PresetTable1[0] = node.Attributes["Type"].InnerText;
                        PresetTable1[1] = node.Attributes["StartAddress"].InnerText;
                        PresetTable1[2] = node.Attributes["Size"].InnerText;
                        PresetTable1[3] = node.Attributes["IsSystem"].InnerText;
                        PresetTable1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "GlobalPreset":

                        GlobalPreset1[0] = node.Attributes["Type"].InnerText;
                        GlobalPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        GlobalPreset1[2] = node.Attributes["Size"].InnerText;
                        GlobalPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        GlobalPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "CombinedPreset":

                        CombinedPreset1[0] = node.Attributes["Type"].InnerText;
                        CombinedPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        CombinedPreset1[2] = node.Attributes["Size"].InnerText;
                        CombinedPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        CombinedPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "GattDatabase":

                        GattDatabase1[0] = node.Attributes["Type"].InnerText;
                        GattDatabase1[1] = node.Attributes["StartAddress"].InnerText;
                        GattDatabase1[2] = node.Attributes["Size"].InnerText;
                        GattDatabase1[3] = node.Attributes["IsSystem"].InnerText;
                        GattDatabase1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "PersistentDataSpace":

                        PersistentDataSpace1[0] = node.Attributes["Type"].InnerText;
                        PersistentDataSpace1[1] = node.Attributes["StartAddress"].InnerText;
                        PersistentDataSpace1[2] = node.Attributes["Size"].InnerText;
                        PersistentDataSpace1[3] = node.Attributes["IsSystem"].InnerText;
                        PersistentDataSpace1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                }

            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    try
                    {


                        switch (i)
                        {
                            case 0:
                                {
                                    if (DfsInitLayoutItem[j].Equals(DfsInitLayoutItem1[j]))
                                    {
                                        Console.WriteLine("Pass");
                                        test.Log(Status.Pass, "Compared Value of DFSInitLayoutItem :** " + "'" + arrayVal[j].ToUpper() + "'" + "**: is" + DfsInitLayoutItem[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of DFSInitLayoutItem Expected is: " + arrayVal[j] + ": is" + DfsInitLayoutItem[j] + "AND Actual Value is :" + DfsInitLayoutItem1[j]);
                                    }

                                    break;
                                }

                            case 1:

                                {
                                    if (FittingSoftwareInfoSpace[j].Equals(FittingSoftwareInfoSpace1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of FittingSoftwareInfoSpace :** " + arrayVal[j].ToUpper() + "** : is" + FittingSoftwareInfoSpace[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of FittingSoftwareInfoSpace Expected is: " + "'" + arrayVal[j] + "'" + " : is " + FittingSoftwareInfoSpace[j] + "AND Actual Value is :" + FittingSoftwareInfoSpace1[j]);
                                    }
                                    break;
                                }

                            case 2:

                                {
                                    if (OsPreset[j].Equals(OsPreset1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of OsPreset :** " + arrayVal[j].ToUpper() + "** : is" + OsPreset[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of OsPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + OsPreset[j] + "AND Actual Value is :" + OsPreset1[j]);
                                    }
                                    break;
                                }

                            case 3:

                                {
                                    if (PresetTable[j].Equals(PresetTable1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of PresetTable :** " + arrayVal[j].ToUpper() + "** : is" + PresetTable[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of PresetTable Expected is: " + "'" + arrayVal[j] + "'" + " : is " + PresetTable[j] + "AND Actual Value is :" + PresetTable1[j]);
                                    }
                                    break;
                                }

                            case 4:

                                {
                                    if (GlobalPreset[j].Equals(GlobalPreset1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of GlobalPreset :** " + arrayVal[j].ToUpper() + "** : is" + GlobalPreset[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of GlobalPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + GlobalPreset[j] + "AND Actual Value is :" + GlobalPreset1[j]);
                                    }
                                    break;
                                }

                            case 5:

                                {
                                    if (CombinedPreset[j].Equals(CombinedPreset1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of CombinedPreset :** " + arrayVal[j].ToUpper() + "** : is" + CombinedPreset[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of CombinedPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + CombinedPreset[j] + "AND Actual Value is :" + CombinedPreset1[j]);
                                    }
                                    break;
                                }

                            case 6:

                                {
                                    if (GattDatabase[j].Equals(GattDatabase1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of GattDatabase :** " + arrayVal[j].ToUpper() + "** : is" + GattDatabase[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of GattDatabase Expected is: " + "'" + arrayVal[j] + "'" + " : is " + GattDatabase[j] + "AND Actual Value is :" + GattDatabase1[j]);
                                    }
                                    break;
                                }

                            case 7:

                                {
                                    if (PersistentDataSpace[j].Equals(PersistentDataSpace1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of PersistentDataSpace :** " + arrayVal[j].ToUpper() + "** : is" + PersistentDataSpace[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of PersistentDataSpace Expected is: " + "'" + arrayVal[j] + "'" + " : is " + PersistentDataSpace[j] + "AND Actual Value is :" + PersistentDataSpace1[j]);
                                    }
                                    break;
                                }


                        }
                    }
                    catch (Exception e) { }

                }
            }




            for (int i = 0; i < FittingSoftwareInfoSpace.Length; i++)
            {
                Console.WriteLine("FittingSoftwareInfoSpace " + arrayVal[i] + " : is :" + FittingSoftwareInfoSpace[i]);
            }


            foreach (var item in FittingSoftwareInfoSpace)
            {
                string txt1 = item;

            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < DfsInitLayoutItem.Length; i++)
            {
                Console.WriteLine("DfsInitLayoutItem " + arrayVal[i] + " : is :" + DfsInitLayoutItem[i]);
            }


            foreach (var item in DfsInitLayoutItem)
            {
                string txt1 = item;

            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < OsPreset.Length; i++)
            {
                Console.WriteLine("OsPreset " + arrayVal[i] + " : is :" + OsPreset[i]);
            }


            foreach (var item in OsPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < PresetTable.Length; i++)
            {
                Console.WriteLine("PresetTable " + arrayVal[i] + " : is :" + PresetTable[i]);
            }


            foreach (var item in PresetTable)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < GlobalPreset.Length; i++)
            {
                Console.WriteLine("GlobalPreset " + arrayVal[i] + " : is :" + GlobalPreset[i]);
            }


            foreach (var item in GlobalPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < CombinedPreset.Length; i++)
            {
                Console.WriteLine("CombinedPreset " + arrayVal[i] + " : is :" + CombinedPreset[i]);
            }


            foreach (var item in CombinedPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < GattDatabase.Length; i++)
            {
                Console.WriteLine("GattDatabase " + arrayVal[i] + " : is :" + GattDatabase[i]);
            }


            foreach (var item in GattDatabase)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < PersistentDataSpace.Length; i++)
            {
                Console.WriteLine("PersistentDataSpace " + arrayVal[i] + " : is :" + PersistentDataSpace[i]);
            }


            foreach (var item in PersistentDataSpace)
            {
                string txt1 = item;

            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


        }

        /* Verifying device dumps and report errors incase any differences*/

        public void dumpCompare(string device, ExtentTest test)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\" + device + ".xml");

            XmlDocument doc1 = new XmlDocument();
            doc1.Load("C:\\Device A.xml");

            /* XML file 1 Attributes */
            string[] DfsInitLayoutItem = new string[5];
            string[] FittingSoftwareInfoSpace = new string[5];
            string[] OsPreset = new string[5];
            string[] PresetTable = new string[5];
            string[] GlobalPreset = new string[5];
            string[] CombinedPreset = new string[5];
            string[] GattDatabase = new string[5];
            string[] PersistentDataSpace = new string[5];

            /* XML file 2 Attributes */
            string[] DfsInitLayoutItem1 = new string[5];
            string[] FittingSoftwareInfoSpace1 = new string[5];
            string[] OsPreset1 = new string[5];
            string[] PresetTable1 = new string[5];
            string[] GlobalPreset1 = new string[5];
            string[] CombinedPreset1 = new string[5];
            string[] GattDatabase1 = new string[5];
            string[] PersistentDataSpace1 = new string[5];


            string[] arrayVal = new string[] { "Type", "StartAddress", "Size", "IsSystem", "Data" };
            string displayName = null;
            string type = null;
            string startaddress = null;
            string size = null;
            string isSystem = null;
            string data = null;

            var txt = doc.DocumentElement.ChildNodes;


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                displayName = node.Attributes["DisplayName"].InnerText; //or loop through its children as well           

                switch (displayName)
                {
                    case "DfsInitLayoutItem":

                        DfsInitLayoutItem[0] = node.Attributes["Type"].InnerText;
                        DfsInitLayoutItem[1] = node.Attributes["StartAddress"].InnerText;
                        DfsInitLayoutItem[2] = node.Attributes["Size"].InnerText;
                        DfsInitLayoutItem[3] = node.Attributes["IsSystem"].InnerText;
                        DfsInitLayoutItem[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "FittingSoftwareInfoSpace":

                        FittingSoftwareInfoSpace[0] = node.Attributes["Type"].InnerText;
                        FittingSoftwareInfoSpace[1] = node.Attributes["StartAddress"].InnerText;
                        FittingSoftwareInfoSpace[2] = node.Attributes["Size"].InnerText;
                        FittingSoftwareInfoSpace[3] = node.Attributes["IsSystem"].InnerText;
                        FittingSoftwareInfoSpace[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "OsPreset":

                        OsPreset[0] = node.Attributes["Type"].InnerText;
                        OsPreset[1] = node.Attributes["StartAddress"].InnerText;
                        OsPreset[2] = node.Attributes["Size"].InnerText;
                        OsPreset[3] = node.Attributes["IsSystem"].InnerText;
                        OsPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "PresetTable":

                        PresetTable[0] = node.Attributes["Type"].InnerText;
                        PresetTable[1] = node.Attributes["StartAddress"].InnerText;
                        PresetTable[2] = node.Attributes["Size"].InnerText;
                        PresetTable[3] = node.Attributes["IsSystem"].InnerText;
                        PresetTable[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "GlobalPreset":

                        GlobalPreset[0] = node.Attributes["Type"].InnerText;
                        GlobalPreset[1] = node.Attributes["StartAddress"].InnerText;
                        GlobalPreset[2] = node.Attributes["Size"].InnerText;
                        GlobalPreset[3] = node.Attributes["IsSystem"].InnerText;
                        GlobalPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "CombinedPreset":

                        CombinedPreset[0] = node.Attributes["Type"].InnerText;
                        CombinedPreset[1] = node.Attributes["StartAddress"].InnerText;
                        CombinedPreset[2] = node.Attributes["Size"].InnerText;
                        CombinedPreset[3] = node.Attributes["IsSystem"].InnerText;
                        CombinedPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "GattDatabase":

                        GattDatabase[0] = node.Attributes["Type"].InnerText;
                        GattDatabase[1] = node.Attributes["StartAddress"].InnerText;
                        GattDatabase[2] = node.Attributes["Size"].InnerText;
                        GattDatabase[3] = node.Attributes["IsSystem"].InnerText;
                        GattDatabase[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "PersistentDataSpace":

                        PersistentDataSpace[0] = node.Attributes["Type"].InnerText;
                        PersistentDataSpace[1] = node.Attributes["StartAddress"].InnerText;
                        PersistentDataSpace[2] = node.Attributes["Size"].InnerText;
                        PersistentDataSpace[3] = node.Attributes["IsSystem"].InnerText;
                        PersistentDataSpace[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                }

            }

            /****File 2 Reading values *****/

            foreach (XmlNode node in doc1.DocumentElement.ChildNodes)
            {
                displayName = node.Attributes["DisplayName"].InnerText; //or loop through its children as well

                switch (displayName)
                {
                    case "DfsInitLayoutItem":

                        DfsInitLayoutItem1[0] = node.Attributes["Type"].InnerText;
                        DfsInitLayoutItem1[1] = node.Attributes["StartAddress"].InnerText;
                        DfsInitLayoutItem1[2] = node.Attributes["Size"].InnerText;
                        DfsInitLayoutItem1[3] = node.Attributes["IsSystem"].InnerText;
                        DfsInitLayoutItem1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "FittingSoftwareInfoSpace":

                        FittingSoftwareInfoSpace1[0] = node.Attributes["Type"].InnerText;
                        FittingSoftwareInfoSpace1[1] = node.Attributes["StartAddress"].InnerText;
                        FittingSoftwareInfoSpace1[2] = node.Attributes["Size"].InnerText;
                        FittingSoftwareInfoSpace1[3] = node.Attributes["IsSystem"].InnerText;
                        FittingSoftwareInfoSpace1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "OsPreset":

                        OsPreset1[0] = node.Attributes["Type"].InnerText;
                        OsPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        OsPreset1[2] = node.Attributes["Size"].InnerText;
                        OsPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        OsPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "PresetTable":

                        PresetTable1[0] = node.Attributes["Type"].InnerText;
                        PresetTable1[1] = node.Attributes["StartAddress"].InnerText;
                        PresetTable1[2] = node.Attributes["Size"].InnerText;
                        PresetTable1[3] = node.Attributes["IsSystem"].InnerText;
                        PresetTable1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "GlobalPreset":

                        GlobalPreset1[0] = node.Attributes["Type"].InnerText;
                        GlobalPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        GlobalPreset1[2] = node.Attributes["Size"].InnerText;
                        GlobalPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        GlobalPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "CombinedPreset":

                        CombinedPreset1[0] = node.Attributes["Type"].InnerText;
                        CombinedPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        CombinedPreset1[2] = node.Attributes["Size"].InnerText;
                        CombinedPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        CombinedPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "GattDatabase":

                        GattDatabase1[0] = node.Attributes["Type"].InnerText;
                        GattDatabase1[1] = node.Attributes["StartAddress"].InnerText;
                        GattDatabase1[2] = node.Attributes["Size"].InnerText;
                        GattDatabase1[3] = node.Attributes["IsSystem"].InnerText;
                        GattDatabase1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "PersistentDataSpace":

                        PersistentDataSpace1[0] = node.Attributes["Type"].InnerText;
                        PersistentDataSpace1[1] = node.Attributes["StartAddress"].InnerText;
                        PersistentDataSpace1[2] = node.Attributes["Size"].InnerText;
                        PersistentDataSpace1[3] = node.Attributes["IsSystem"].InnerText;
                        PersistentDataSpace1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                }

            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    try
                    {


                        switch (i)
                        {
                            case 0:
                                {
                                    if (DfsInitLayoutItem[j].Equals(DfsInitLayoutItem1[j]))
                                    {
                                        Console.WriteLine("Pass");
                                        test.Log(Status.Pass, "Compared Value of DFSInitLayoutItem :** " + "'" + arrayVal[j].ToUpper() + "'" + "**: is" + DfsInitLayoutItem[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of DFSInitLayoutItem Expected is: " + arrayVal[j] + ": is" + DfsInitLayoutItem[j] + "AND Actual Value is :" + DfsInitLayoutItem1[j]);
                                    }

                                    break;
                                }

                            case 1:

                                {
                                    if (FittingSoftwareInfoSpace[j].Equals(FittingSoftwareInfoSpace1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of FittingSoftwareInfoSpace :** " + arrayVal[j].ToUpper() + "** : is" + FittingSoftwareInfoSpace[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of FittingSoftwareInfoSpace Expected is: " + "'" + arrayVal[j] + "'" + " : is " + FittingSoftwareInfoSpace[j] + "AND Actual Value is :" + FittingSoftwareInfoSpace1[j]);
                                    }
                                    break;
                                }

                            case 2:

                                {
                                    if (OsPreset[j].Equals(OsPreset1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of OsPreset :** " + arrayVal[j].ToUpper() + "** : is" + OsPreset[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of OsPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + OsPreset[j] + "AND Actual Value is :" + OsPreset1[j]);
                                    }
                                    break;
                                }

                            case 3:

                                {
                                    if (PresetTable[j].Equals(PresetTable1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of PresetTable :** " + arrayVal[j].ToUpper() + "** : is" + PresetTable[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of PresetTable Expected is: " + "'" + arrayVal[j] + "'" + " : is " + PresetTable[j] + "AND Actual Value is :" + PresetTable1[j]);
                                    }
                                    break;
                                }

                            case 4:

                                {
                                    if (GlobalPreset[j].Equals(GlobalPreset1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of GlobalPreset :** " + arrayVal[j].ToUpper() + "** : is" + GlobalPreset[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of GlobalPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + GlobalPreset[j] + "AND Actual Value is :" + GlobalPreset1[j]);
                                    }
                                    break;
                                }

                            case 5:

                                {
                                    if (CombinedPreset[j].Equals(CombinedPreset1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of CombinedPreset :** " + arrayVal[j].ToUpper() + "** : is" + CombinedPreset[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of CombinedPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + CombinedPreset[j] + "AND Actual Value is :" + CombinedPreset1[j]);
                                    }
                                    break;
                                }

                            case 6:

                                {
                                    if (GattDatabase[j].Equals(GattDatabase1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of GattDatabase :** " + arrayVal[j].ToUpper() + "** : is" + GattDatabase[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of GattDatabase Expected is: " + "'" + arrayVal[j] + "'" + " : is " + GattDatabase[j] + "AND Actual Value is :" + GattDatabase1[j]);
                                    }
                                    break;
                                }

                            case 7:

                                {
                                    if (PersistentDataSpace[j].Equals(PersistentDataSpace1[j]))
                                    {
                                        test.Log(Status.Pass, "Compared Value of PersistentDataSpace :** " + arrayVal[j].ToUpper() + "** : is" + PersistentDataSpace[j]);
                                    }
                                    else
                                    {
                                        test.Log(Status.Fail, "Compared Value of PersistentDataSpace Expected is: " + "'" + arrayVal[j] + "'" + " : is " + PersistentDataSpace[j] + "AND Actual Value is :" + PersistentDataSpace1[j]);
                                    }
                                    break;
                                }


                        }

                    }

                    catch (Exception e) { }
                }
            }


            for (int i = 0; i < FittingSoftwareInfoSpace.Length; i++)
            {
                Console.WriteLine("FittingSoftwareInfoSpace " + arrayVal[i] + " : is :" + FittingSoftwareInfoSpace[i]);
            }


            foreach (var item in FittingSoftwareInfoSpace)
            {
                string txt1 = item;

            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < DfsInitLayoutItem.Length; i++)
            {
                Console.WriteLine("DfsInitLayoutItem " + arrayVal[i] + " : is :" + DfsInitLayoutItem[i]);
            }


            foreach (var item in DfsInitLayoutItem)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < OsPreset.Length; i++)
            {
                Console.WriteLine("OsPreset " + arrayVal[i] + " : is :" + OsPreset[i]);
            }


            foreach (var item in OsPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < PresetTable.Length; i++)
            {
                Console.WriteLine("PresetTable " + arrayVal[i] + " : is :" + PresetTable[i]);
            }


            foreach (var item in PresetTable)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < GlobalPreset.Length; i++)
            {
                Console.WriteLine("GlobalPreset " + arrayVal[i] + " : is :" + GlobalPreset[i]);
            }


            foreach (var item in GlobalPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < CombinedPreset.Length; i++)
            {
                Console.WriteLine("CombinedPreset " + arrayVal[i] + " : is :" + CombinedPreset[i]);
            }


            foreach (var item in CombinedPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < GattDatabase.Length; i++)
            {
                Console.WriteLine("GattDatabase " + arrayVal[i] + " : is :" + GattDatabase[i]);
            }


            foreach (var item in GattDatabase)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < PersistentDataSpace.Length; i++)
            {
                Console.WriteLine("PersistentDataSpace " + arrayVal[i] + " : is :" + PersistentDataSpace[i]);
            }


            foreach (var item in PersistentDataSpace)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);

        }/* End of dumpCompare*/
   
    }
}
