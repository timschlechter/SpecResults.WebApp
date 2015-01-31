> __Attention:__ this project was formerly known as SpecFlow.Reporting.WebApp.  This is
> not an offical SpecFlow package and the name clashed with some of the
> official SpecFlow packages/namespace. Therefor it was renamed to
> SpecResults.WebApp
>
> All previous published versions of SpecFlow.Reporting.WebApp are still available on
> NuGet.org, alltough they aren't listed anymore.

# WebApp reporter [![Build status](https://ci.appveyor.com/api/projects/status/929xe2ohucewlkkj/branch/master)](https://ci.appveyor.com/project/TimSchlechter/specflow-reporting-webapp)

NuGet: [SpecResults.WebApp](https://www.nuget.org/packages/SpecResults.WebApp/)

## Usage
Make your existing [StepDefinitions class](https://github.com/techtalk/SpecFlow/wiki/Step-Definitions) inherit from [SpecResults.ReportingStepDefinitions](https://github.com/specflowreporting/SpecResults/blob/master/SpecResults/ReportingStepDefinitions.cs)

Initialize and add the reporter in [BeforeTestRun] and register on one of the [events](https://github.com/specflowreporting/SpecResults/blob/master/SpecResults/Reporters.Events.cs) to get notified when something gets reported:

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