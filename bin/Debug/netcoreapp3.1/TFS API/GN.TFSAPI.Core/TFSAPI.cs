using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace GN.TFSAPI.Core
{
    public class TFSAPI
    {

        string _testPlan = "";
        string _testCase = "";
        string _testConfig = "";

        string testSuit = "";
        string testPlan = "";
        string testCaseID = "";
        string config = "";
        string testActionTitle = "";
        string stepActualResult = "";
        string tfsUserName = "";
        string tfsPassword = "";
        string stepComment = "";
        public static NetworkCredential credential = null;
        TfsTeamProjectCollection teamProjectCollection = null;
        ITestRun testRun = null;
        ITestPlanCollection plans = null;
        ITestManagementTeamProject testproject = null;
        ITestCaseResultCollection results = null;

        public string TFSUserName
        {
            get
            {
                return tfsUserName;
            }
            set { tfsUserName = value; }
        }

        public string TFSPassword
        {
            get
            {
                return tfsPassword;
            }
            set { tfsPassword = value; }
        }

        public string TestSuitNumber
        {
            get
            {
                return testSuit;
            }
            set
            {
                testSuit = value;
            }
        }

        public string TestPlanNumber
        {
            get
            {
                return testPlan;
            }
            set
            {
                testPlan = value;
            }
        }

        public string TestCaseNummber
        {
            get
            {
                return testCaseID;
            }
            set
            {
                testCaseID = value;
            }
        }

        public string TestCaseConfiguration
        {
            get
            {
                return config;
            }
            set
            {
                config = value;
            }
        }

        public string TestCaseActionTitle
        {
            get
            {
                return testActionTitle;
            }
            set
            {
                testActionTitle = value;
            }
        }

        public string TestCaseStepResult
        {
            get
            {
                return stepActualResult;
            }
            set
            {
                stepActualResult = value;
            }
        }

        public string BuildNumber
        {
            get;
            set;
        }

        public string StepComment
        {
            private get
            {
                return stepComment;
            }
            set
            {
                stepComment = value;
            }
        }

        public string TestAttachment { get; set; }

        bool flagStatus = true;
        bool outComeflag = true;
        ITestPlan p = null;
        ITestPointCollection points = null;
        public void UpdateTFSTestPlan()
        {
            Stopwatch watcher = new Stopwatch();
            watcher.Start();
            try
            {
                //Basic TFS API and authenticated TFS
                //   Uri collectionUri = new Uri("http://rdtfs:8080//tfs//GNR");
                Uri collectionUri = new Uri("https://tfs.gnhearing.com/tfs/GNR");
                if (credential == null)
                {
                    credential = new NetworkCredential(TFSUserName, tfsPassword);

                    teamProjectCollection = new TfsTeamProjectCollection(collectionUri, credential);
                    teamProjectCollection.EnsureAuthenticated();
                    WorkItemStore workItemStore = teamProjectCollection.GetService<WorkItemStore>();
                    var service = teamProjectCollection.GetService<ITestManagementService>();
                    testproject = service.GetTeamProject("GNR");
                    plans = testproject.TestPlans.Query("Select * From TestPlan");
                }

                if ((config != _testConfig))
                {
                    p = plans.Where(pp => pp.Id == int.Parse(testPlan)).First();

                    _testPlan = testPlan;
                    _testCase = testCaseID;
                    _testConfig = config;

                    testRun = p.CreateTestRun(true);
                    testRun.IsBvt = true;
                    if (!string.IsNullOrEmpty(TestSuitNumber))
                        points = p.QueryTestPoints("SELECT * FROM TestPoint WHERE SuiteId  = " + int.Parse(TestSuitNumber) + " and TestCaseId = " + int.Parse(testCaseID));
                    else
                        points = p.QueryTestPoints("SELECT * FROM TestPoint WHERE TestCaseId = " + int.Parse(testCaseID));
                }
                else if ((testCaseID != _testCase))
                {
                    p = plans.Where(pp => pp.Id == int.Parse(testPlan)).First();

                    _testPlan = testPlan;
                    _testCase = testCaseID;
                    _testConfig = config;

                    testRun = p.CreateTestRun(true);

                    testRun.IsBvt = true;

                    if (!string.IsNullOrEmpty(TestSuitNumber))
                        points = p.QueryTestPoints("SELECT * FROM TestPoint WHERE SuiteId  = " + int.Parse(TestSuitNumber) + " and TestCaseId = " + int.Parse(testCaseID));
                    else
                        points = p.QueryTestPoints("SELECT * FROM TestPoint WHERE TestCaseId = " + int.Parse(testCaseID));
                }


                foreach (ITestPoint p1 in points)
                {
                    string tfsConfig = RemoveWhiteSpaces(p1.ConfigurationName);

                    if (tfsConfig == RemoveWhiteSpaces(config))
                    {
                        testRun.AddTestPoint(p1, p.Owner);
                        break;
                    }
                }

                testRun.Title = testCaseID + " : " + config;
                testRun.Type = TestRunType.Web;
                testRun.Save();
                testRun.Refresh();
                ITestStepResult stepResult = null;


                results = testRun.QueryResults();
                ITestIterationResult iterationResult = null;
                if (TestCaseStepResult != "NA")
                {

                    foreach (ITestCaseResult result in results)

                    {

                        if (result.TestCaseId == int.Parse(testCaseID))

                        {

                            iterationResult = result.CreateIteration(1);



                            foreach (var action in result.GetTestCase().Actions)

                            {

                                if (action is ISharedStepReference)

                                {

                                    // Handle shared step references by expanding them

                                    var sharedStepReference = action as ISharedStepReference;

                                    var sharedStep = sharedStepReference.FindSharedStep();



                                    foreach (var step in sharedStep.Actions)

                                    {

                                        // Process the steps within the shared step

                                        if (step.Id == (Convert.ToInt32(testActionTitle) + 1))

                                        {

                                            if (stepActualResult.ToUpper() == "FAILED" || stepActualResult.ToUpper() == "FAIL" || stepActualResult.ToUpper() == "FALSE")

                                            {

                                                Console.WriteLine("Step " + testActionTitle + " Committed");

                                                stepResult = iterationResult.CreateStepResult(step.Id);

                                                stepResult.Outcome = TestOutcome.Failed;

                                                flagStatus = false;

                                                outComeflag = false;

                                                iterationResult.Outcome = TestOutcome.Failed;

                                                iterationResult.Actions.Add(stepResult);

                                                break;

                                            }



                                            if (stepActualResult.ToUpper() == "PASS" || stepActualResult.ToUpper() == "PASSED" || stepActualResult.ToUpper() == "TRUE")

                                            {

                                                Console.WriteLine("Step " + testActionTitle + " Committed");

                                                stepResult = iterationResult.CreateStepResult(step.Id);

                                                stepResult.Outcome = TestOutcome.Passed;

                                                flagStatus = true;

                                                iterationResult.Actions.Add(stepResult);

                                                iterationResult.Outcome = TestOutcome.Passed;

                                                break;

                                            }

                                        }

                                    }

                                }

                                else if (action is ITestStep)

                                {

                                    // Handle regular test steps

                                    var testStep = action as ITestStep;

                                    if (testStep.Id == (Convert.ToInt32(testActionTitle) + 1))

                                    {

                                        if (stepActualResult.ToUpper() == "FAILED" || stepActualResult.ToUpper() == "FAIL" || stepActualResult.ToUpper() == "FALSE")

                                        {

                                            Console.WriteLine("Step " + testActionTitle + " Committed");

                                            stepResult = iterationResult.CreateStepResult(testStep.Id);

                                            stepResult.Outcome = TestOutcome.Failed;

                                            flagStatus = false;

                                            outComeflag = false;

                                            iterationResult.Outcome = TestOutcome.Failed;

                                            iterationResult.Actions.Add(stepResult);

                                            break;

                                        }



                                        if (stepActualResult.ToUpper() == "PASS" || stepActualResult.ToUpper() == "PASSED" || stepActualResult.ToUpper() == "TRUE")

                                        {

                                            Console.WriteLine("Step " + testActionTitle + " Committed");

                                            stepResult = iterationResult.CreateStepResult(testStep.Id);

                                            stepResult.Outcome = TestOutcome.Passed;

                                            flagStatus = true;

                                            iterationResult.Actions.Add(stepResult);

                                            iterationResult.Outcome = TestOutcome.Passed;

                                            break;

                                        }

                                    }

                                }

                            }



                            if (TestAttachment != null)

                            {

                                if (File.Exists(TestAttachment))

                                {

                                    ITestAttachment testAttachment = result.CreateAttachment(TestAttachment);

                                    result.Attachments.Add(testAttachment);

                                }

                            }


                            try
                            {

                            if (flagStatus)

                                stepResult.Outcome = TestOutcome.Passed;

                            else

                                    stepResult.Outcome = TestOutcome.Failed;
                            }

                            catch(Exception)

                            {

                            }
                        }



                        if (outComeflag)

                            iterationResult.Outcome = TestOutcome.Passed;

                        else

                            iterationResult.Outcome = TestOutcome.Failed;



                        result.Iterations.Add(iterationResult);

                        result.DateCompleted = DateTime.Now;

                        result.Outcome = iterationResult.Outcome;

                        result.Comment = StepComment;

                        result.State = TestResultState.Completed;
                    }

                    
                }

                

                else
                {
                    foreach (ITestCaseResult result in results)
                    {
                        if (result.TestCaseId == int.Parse(testCaseID))
                        {
                            foreach (ITestStep testStep in result.GetTestCase().Actions)
                            {
                                Console.Write(testStep.Id.ToString() + " " + "Updated");
                                result.Outcome = TestOutcome.Passed;
                                result.State = TestResultState.Completed;
                            }
                        }
                    }
                }

                results.Save(true);
            }
            
           
            catch(Exception Ex)
            {
               Console.WriteLine(Ex.Message);
               Console.ReadKey();
            }
            watcher.Stop();

            Console.WriteLine(" : Test Step Executed in " + watcher.ElapsedMilliseconds / 1000 + " Sec(s)");

        }


        private string RemoveWhiteSpaces(string str)
        {
            return Regex.Replace(str, @"\s", "");
        }

        private string StripHTML(string htmlString)
        {
            string pattern = @"<(.|\n)*?>";

            return Regex.Replace(htmlString, pattern, string.Empty);
        }

        public void CompleteTFSTestRun()
        {
            if (testRun != null)
            {
                testRun.Refresh();
                if (outComeflag)
                    testRun.State = TestRunState.Completed;
                else
                    testRun.State = TestRunState.NeedsInvestigation;

                testRun.Save();
            }
        }

        public static IList<ITestPoint> CreateTestPoints(ITestManagementTeamProject project,
                                                          ITestPlan testPlan,
                                                          IList<ITestCase> testCases,
                                                          IList<IdAndName> testConfigs)
        {
            // Create a static suite within the plan and add all the test cases.
            IStaticTestSuite testSuite = CreateTestSuite(project);
            testPlan.RootSuite.Entries.Add(testSuite);
            testPlan.Save();

            testSuite.Entries.AddCases(testCases);
            testPlan.Save();

            testSuite.SetEntryConfigurations(testSuite.Entries, testConfigs);
            testPlan.Save();

            ITestPointCollection tpc = testPlan.QueryTestPoints("SELECT * FROM TestPoint WHERE SuiteId = " + testSuite.Id);
            return new List<ITestPoint>(tpc);
        }

        private static IStaticTestSuite CreateTestSuite(ITestManagementTeamProject project)
        {
            // Create a static test suite.
            IStaticTestSuite testSuite = project.TestSuites.CreateStatic();
            testSuite.Title = "1282571"; //"Static Suite";
            return testSuite;
        }
    }
}