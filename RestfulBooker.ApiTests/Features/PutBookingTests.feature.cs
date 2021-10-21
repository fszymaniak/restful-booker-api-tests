﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.8.0.0
//      SpecFlow Generator Version:3.8.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace RestfulBooker.ApiTests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.8.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Put Bookings endpoint tests")]
    [NUnit.Framework.CategoryAttribute("TestDataCleanup")]
    public partial class PutBookingsEndpointTestsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = new string[] {
                "TestDataCleanup"};
        
#line 1 "PutBookingTests.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Put Bookings endpoint tests", null, ProgrammingLanguage.CSharp, new string[] {
                        "TestDataCleanup"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Put Booking returns updated Bookings when request with new data is sent")]
        [NUnit.Framework.CategoryAttribute("GetInitialBookingIds")]
        public virtual void PutBookingReturnsUpdatedBookingsWhenRequestWithNewDataIsSent()
        {
            string[] tagsOfScenario = new string[] {
                    "GetInitialBookingIds"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Put Booking returns updated Bookings when request with new data is sent", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 5
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "FirstName",
                            "LastName",
                            "TotalPrice",
                            "DepositPaid",
                            "BookingDates",
                            "AdditionalNeeds"});
                table10.AddRow(new string[] {
                            "Jack",
                            "Mamoa",
                            "1000",
                            "true",
                            "2020-08-23 / 2020-08-30",
                            "Breakfasts"});
                table10.AddRow(new string[] {
                            "Kate",
                            "Winslet",
                            "1500",
                            "false",
                            "2020-09-23 / 2020-09-30",
                            "Breakfasts"});
#line 6
testRunner.Given("bookings exist", ((string)(null)), table10, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                            "FirstName",
                            "LastName",
                            "TotalPrice",
                            "DepositPaid",
                            "BookingDates",
                            "AdditionalNeeds"});
                table11.AddRow(new string[] {
                            "Derric",
                            "Green",
                            "2500",
                            "false",
                            "2021-10-20 / 2021-10-28",
                            "Parking"});
                table11.AddRow(new string[] {
                            "Kia",
                            "Madson",
                            "1750",
                            "true",
                            "2021-07-17 / 2021-07-29",
                            "Dinner"});
#line 10
testRunner.When("PUT Bookings request with following data is sent", ((string)(null)), table11, "When ");
#line hidden
#line 14
testRunner.And("GET Bookings Ids request is sent", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 15
testRunner.Then("expected bookings should exist", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Put Booking returns Bad Request status code when incompleted Booking Model is sen" +
            "t")]
        [NUnit.Framework.TestCaseAttribute("FirstName", null)]
        [NUnit.Framework.TestCaseAttribute("LastName", null)]
        [NUnit.Framework.TestCaseAttribute("BookingDates", null)]
        [NUnit.Framework.TestCaseAttribute("TotalPrice", null)]
        [NUnit.Framework.TestCaseAttribute("DepositPaid", null)]
        public virtual void PutBookingReturnsBadRequestStatusCodeWhenIncompletedBookingModelIsSent(string excludedRow, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("excludedRow", excludedRow);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Put Booking returns Bad Request status code when incompleted Booking Model is sen" +
                    "t", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 17
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                            "FirstName",
                            "LastName",
                            "TotalPrice",
                            "DepositPaid",
                            "BookingDates",
                            "AdditionalNeeds"});
                table12.AddRow(new string[] {
                            "Jack",
                            "Mamoa",
                            "1000",
                            "true",
                            "2020-08-23 / 2020-08-30",
                            "Breakfasts"});
                table12.AddRow(new string[] {
                            "Kate",
                            "Winslet",
                            "1500",
                            "false",
                            "2020-09-23 / 2020-09-30",
                            "Breakfasts"});
#line 18
testRunner.Given("bookings exist", ((string)(null)), table12, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                            "FirstName",
                            "LastName",
                            "TotalPrice",
                            "DepositPaid",
                            "BookingDates",
                            "AdditionalNeeds"});
                table13.AddRow(new string[] {
                            "Jack",
                            "Mamoa",
                            "1000",
                            "true",
                            "2020-08-23 / 2020-08-30",
                            "Breakfasts"});
#line 22
testRunner.When(string.Format("PUT Bookings request with invalid data without {0} is sent", excludedRow), ((string)(null)), table13, "When ");
#line hidden
#line 25
testRunner.Then("expected bookings should return expected status code 400", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 26
testRunner.And("GET Booking by Id request is sent", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 27
testRunner.And("bookings should not be updated", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
