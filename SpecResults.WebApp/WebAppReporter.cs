using System;
using System.IO;
using System.Text.RegularExpressions;
using SpecResults.Json;
using SpecResults.WebApp.Properties;

namespace SpecResults.WebApp
{
	public class WebAppReporter : Reporter
	{
		public WebAppReporter()
		{
			Settings = new WebAppReporterSettings();
		}

		protected JsonReporter JsonReporter
		{
			get
			{
				return new JsonReporter
				{
					Report = Report
				};
			}
		}

		public WebAppReporterSettings Settings { get; private set; }

		public override void WriteToStream(Stream stream)
		{
			throw new NotSupportedException("The WebAppReporter only support WriteToFolder()");
		}

		public void WriteToFolder(string folderPath, bool clearDirectory = false)
		{
			var jsPath = Path.Combine(folderPath, "js");
			var cssPath = Path.Combine(folderPath, "css");

			if (clearDirectory && Directory.Exists(folderPath))
			{
				foreach (var dir in Directory.GetDirectories(folderPath))
				{
					Directory.Delete(dir, true);
				}

				foreach (var file in Directory.GetFiles(folderPath))
				{
					File.Delete(file);
				}
			}

			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			if (!Directory.Exists(jsPath))
			{
				Directory.CreateDirectory(jsPath);
			}
			if (!Directory.Exists(cssPath))
			{
				Directory.CreateDirectory(cssPath);
			}

			// index.html
			File.WriteAllText(Path.Combine(folderPath, "index.html"), ApplySettings(Resources.index_html)
				);

			// css/styles.min.css
			File.WriteAllText(Path.Combine(cssPath, "styles.min.css"), Resources.styles_min_css
				);

			// js/scripts.min.js
			File.WriteAllText(Path.Combine(jsPath, "scripts.min.js"), ApplySettings(Resources.scripts_min_js)
				);

			// js/scripts.min.map
			File.WriteAllText(Path.Combine(jsPath, "scripts.min.map"), ApplySettings(Resources.scripts_min_map)
				);

			// js/reportdata.js
			File.WriteAllText(Path.Combine(jsPath, "report-data.js"),
				string.Format("var reportData = {0};", JsonReporter.WriteToString())
				);

			WriteFontFiles(folderPath);
		}

		private static void WriteFontFiles(string folderPath)
		{
			var fontsPath = Path.Combine(folderPath, "fonts");
			if (!Directory.Exists(fontsPath))
			{
				Directory.CreateDirectory(fontsPath);
			}

			File.WriteAllBytes(Path.Combine(fontsPath, "glyphicons-halflings-regular.eot"),
				Resources.glyphicons_halflings_regular_eot
				);

			File.WriteAllBytes(Path.Combine(fontsPath, "glyphicons-halflings-regular.svg"),
				Resources.glyphicons_halflings_regular_svg
				);

			File.WriteAllBytes(Path.Combine(fontsPath, "glyphicons-halflings-regular.ttf"),
				Resources.glyphicons_halflings_regular_ttf
				);

			File.WriteAllBytes(Path.Combine(fontsPath, "glyphicons-halflings-regular.woff"),
				Resources.glyphicons_halflings_regular_woff
				);
		}

		private string ApplySettings(string contents)
		{
			contents = contents
				.Replace("__TITLE__", Settings.GetTitle())
				.Replace("__VERSION__", Settings.GetVersion())
				.Replace("__CULTURE__", Settings.GetCulture());


			// Inject custom css
			var css = "";
			if (!string.IsNullOrEmpty(Settings.CssFile))
			{
				css = File.ReadAllText(Settings.CssFile);
			}
			contents = contents.Replace("__CSS__", css);

			// Inject Dashboard text
			var dashboardText = "";
			if (!string.IsNullOrEmpty(Settings.DashboardTextFile))
			{
				dashboardText = File.ReadAllText(Settings.DashboardTextFile);
				dashboardText = Markdown.ToHtml(dashboardText);
				dashboardText = Regex.Replace(dashboardText, "(\r|\n)", "\\n");
			}
			contents = contents.Replace("__DASHBOARD_TEXT__", dashboardText);

			// Inject custom step-details template
			if (!string.IsNullOrEmpty(Settings.StepDetailsTemplateFile))
			{
				var pattern = "(?:<!-- step-details-marker: begin -->)(.*?)(?:<!-- step-details-marker: end -->)";

				var replacement = File.ReadAllText(Settings.StepDetailsTemplateFile) + "'+\"";
				;
				replacement = Regex.Replace(replacement, "(\r|\n)", "\\n");
				replacement = replacement.Replace("'", "\'");

				contents = Regex.Replace(contents, pattern, replacement
					);
			}

			return contents;
		}
	}
}