using SpecFlow.Reporting.Json;
using SpecFlow.Reporting.WebApp.Properties;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SpecFlow.Reporting.WebApp
{
    public class WebAppReporter : Reporter
    {
        protected JsonReporter JsonReporter
        {
            get
            {
                return new JsonReporter
                {
                    Report = this.Report
                };
            }
        }

        public WebAppReporterSettings Settings { get; private set; }

        public WebAppReporter()
        {
            Settings = new WebAppReporterSettings();
        }

        public override void WriteToStream(Stream stream)
        {
            throw new System.NotSupportedException("The WebAppReporter only support WriteToFolder()");
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
            File.WriteAllText(
                path: Path.Combine(folderPath, "index.html"),
                contents: ApplySettings(Resources.index_html)
            );

            // css/styles.min.css
            File.WriteAllText(
                path: Path.Combine(cssPath, "styles.min.css"),
                contents: Resources.styles_min_css
            );

            // js/scripts.min.js
            File.WriteAllText(
                path: Path.Combine(jsPath, "scripts.min.js"),
                contents: ApplySettings(Resources.scripts_min_js)
            );

            // js/scripts.min.map
            File.WriteAllText(
                path: Path.Combine(jsPath, "scripts.min.map"),
                contents: ApplySettings(Resources.scripts_min_map)
            );

            // js/reportdata.js
            File.WriteAllText(
                path: Path.Combine(jsPath, "report-data.js"),
                contents: string.Format("var reportData = {0};", JsonReporter.WriteToString())
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

            File.WriteAllBytes(
                path: Path.Combine(fontsPath, "glyphicons-halflings-regular.eot"),
                bytes: Resources.glyphicons_halflings_regular_eot
            );

            File.WriteAllBytes(
                path: Path.Combine(fontsPath, "glyphicons-halflings-regular.svg"),
                bytes: Resources.glyphicons_halflings_regular_svg
            );

            File.WriteAllBytes(
                path: Path.Combine(fontsPath, "glyphicons-halflings-regular.ttf"),
                bytes: Resources.glyphicons_halflings_regular_ttf
            );

            File.WriteAllBytes(
                path: Path.Combine(fontsPath, "glyphicons-halflings-regular.woff"),
                bytes: Resources.glyphicons_halflings_regular_woff
            );
        }

        private string ApplySettings(string contents)
        {
            contents = contents
                .Replace("__TITLE__", Settings.GetTitle())
                .Replace("__VERSION__", Settings.GetVersion())
                .Replace("__CULTURE__", Settings.GetCulture());
            

            // Inject custom css
            string css = "";
            if (!string.IsNullOrEmpty(Settings.CssFile))
            {
                css = File.ReadAllText(Settings.CssFile);
            }
            contents = contents.Replace("__CSS__", css);

            // Inject Dashboard text
            string dashboardText = "";
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

				var replacement = File.ReadAllText(Settings.StepDetailsTemplateFile) + "'+\""; ;
                replacement = Regex.Replace(replacement, "(\r|\n)", "\\n");
                replacement = replacement.Replace("'", "\'");

                contents = Regex.Replace(
                    input: contents,
                    pattern: pattern,
                    replacement: replacement
                );
            }

            return contents;
        }
    }
}