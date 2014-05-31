# WebApp reporter [![Build status](https://ci.appveyor.com/api/projects/status/929xe2ohucewlkkj)](https://ci.appveyor.com/project/TimSchlechter/specflow-reporting-webapp)

NuGet: [SpecFlow.Reporting.WebApp](https://www.nuget.org/packages/SpecFlow.Reporting.WebApp/)

## Usage
Make your existing [StepDefinitions class](https://github.com/techtalk/SpecFlow/wiki/Step-Definitions) inherit from [SpecFlow.Reporting.ReportingStepDefinitions](https://github.com/specflowreporting/SpecFlow.Reporting/blob/master/SpecFlow.Reporting/ReportingStepDefinitions.cs)

Initialize and add the reporter in [BeforeTestRun] and register on one of the [events](https://github.com/specflowreporting/SpecFlow.Reporting/blob/master/SpecFlow.Reporting/Reporters.Events.cs) to get notified when something gets reported:

<pre>
[Binding]
public class StepDefinitions : ReportingStepDefinitions
{
	[BeforeTestRun]
	public static void BeforeTestRun()
	{
		var webApp = new WebAppReporter();
		webApp.Settings.Title = "WebAppReporter Showcase";
		
		Reporters.Add(webApp);

		Reporters.FinishedReport += (sender, args) =>
		{
			var reporter = args.Reporter as WebAppReporter;
			if (reporter != null)
			{
				reporter.WriteToFolder("app", true);
			}
		};
	}
}	
</pre>