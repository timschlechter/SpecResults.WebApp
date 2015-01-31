using System;
using System.Globalization;

namespace SpecResults.WebApp
{
	public class WebAppReporterSettings
	{
		public string Title { get; set; }
		public string StepDetailsTemplateFile { get; set; }
		public string CssFile { get; set; }
		public string DashboardTextFile { get; set; }
		public CultureInfo Culture { get; set; }

		internal string GetTitle()
		{
			return String.IsNullOrEmpty(Title) ? "{WebAppReporter.Settings.Title}" : Title;
		}

		internal string GetVersion()
		{
			return Guid.NewGuid().ToString();
		}

		internal string GetCulture()
		{
			return Culture != null ? Culture.Name : "en-US";
		}
	}
}