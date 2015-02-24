using System;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace SpecResults.WebApp.Showcase
{
	[Binding]
	public class StepDefinitions : ReportingStepDefinitions
	{
		private static IWebDriver WebDriver { get; set; }

		[BeforeTestRun]
		public static void BeforeTestRun()
		{
			var webApp = new WebAppReporter();
			webApp.Settings.Title = "WebAppReporter Showcase";
			webApp.Settings.StepDetailsTemplateFile = GetAbsolutePath(@"templates\step-details.tpl.html");
			webApp.Settings.CssFile = GetAbsolutePath(@"templates\styles.css");
			webApp.Settings.DashboardTextFile = GetAbsolutePath(@"templates\dashboard-text.md");

			Reporters.Add(webApp);

			var screenshotFolder = GetAbsolutePath("screenshots");
			var appFolder = GetAbsolutePath("app");

			if (Directory.Exists(screenshotFolder))
			{
				Directory.Delete(screenshotFolder, true);
			}

			Reporters.FinishedStep += (sender, args) =>
			{
				var path = Path.Combine("screenshots", Guid.NewGuid().ToString() + ".png");
				WebDriver.TakeScreenshot(path);
				args.Step.UserData = new
				{
					Screenshot = path
				};
			};

			Reporters.FinishedReport += (sender, args) =>
			{
				var reporter = args.Reporter as WebAppReporter;
				if (reporter != null)
				{
					reporter.WriteToFolder(appFolder, true);

					Directory.Move(screenshotFolder, Path.Combine(appFolder, "screenshots"));
				}
			};

			WebDriver = new FirefoxDriver();
		}

		private static string GetAbsolutePath(string path)
		{
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
		}

		[AfterTestRun]
		public static void AfterTestRun()
		{
			WebDriver.Close();
			WebDriver.Dispose();
			WebDriver = null;
		}

		[Given(@"I'm on ""(.*)""")]
		public void GivenIMOn(string url)
		{
			WebDriver.Navigate().GoToUrl(url);
		}

		[When(@"I enter searchtext ""(.*)"" in ""(.*)""")]
		public void WhenIEnterSearchtextIn(string searchText, string searchInputId)
		{
			WebDriver.FindElement(By.Id(searchInputId)).SendKeys(searchText);
		}

		[When(@"I click the search button ""(.*)""")]
		public void WhenIClickTheSearchButton(string buttonId)
		{
			WebDriver.FindElement(By.Id(buttonId)).Click();
		}

		[When(@"I click the result with title ""(.*)""")]
		public void WhenIClickTheResultWithTitle(string title)
		{
			WebDriver.FindElements(By.TagName("a"))
				.First(a => a.Text == title)
				.Click();
		}

		[Then(@"I can read the instructions on how to install the package")]
		public void ThenICanReadTheInstructionsOnHowToInstallThePackage()
		{
		}
	}
}