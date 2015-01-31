using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;

namespace SpecResults.WebApp.Showcase
{
	public static class Extensions
	{
		#region IWebDriver

		public static void TakeScreenshot(this IWebDriver driver, string outputFile)
		{
			var outputFolder = Path.GetDirectoryName(outputFile);
			if (!Directory.Exists(outputFolder))
			{
				Directory.CreateDirectory(outputFolder);
			}

			var takesScreenshot = driver as ITakesScreenshot;

			if (takesScreenshot != null)
			{
				var screenshot = takesScreenshot.GetScreenshot();

				var screenshotFilePath = Path.Combine(outputFile);

				screenshot.SaveAsFile(screenshotFilePath, ImageFormat.Png);
			}
		}

		#endregion IWebDriver
	}
}