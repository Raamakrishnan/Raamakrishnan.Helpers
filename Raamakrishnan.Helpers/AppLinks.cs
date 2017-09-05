using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Raamakrishnan.Helpers
{
    public class AppLinks
    {
		/// <summary>
		/// Launch the URI scheme based AppUri if there is an app to handle, else open the browser
		/// </summary>
		/// <param name="AppUri">Based on the URI scheme of the app</param>
		/// <param name="WebUri">Web URI</param>
		/// <returns>Returns a LaunchLinkResult object</returns>
		public async static Task<LaunchLinkResult> LaunchLinkAsync(Uri AppUri, Uri WebUri)
		{
			LaunchLinkResult result = new LaunchLinkResult() { IsLinkLaunched = false, LinkLaunchedBy = LinkLauncher.Unknown };
			switch (await Launcher.QueryUriSupportAsync(AppUri, LaunchQuerySupportType.Uri))
			{
				case LaunchQuerySupportStatus.Available:
					result.LinkLaunchedBy = LinkLauncher.App;
					result.IsLinkLaunched = await Launcher.LaunchUriAsync(AppUri);
					break;
				default:
					result.LinkLaunchedBy = LinkLauncher.WebBrowser;
					result.IsLinkLaunched = await Launcher.LaunchUriAsync(WebUri);
					break;
			}
			return result;
		}

		/// <summary>
		/// Launch the URI scheme based AppUri if there is an app to handle, else open the browser
		/// </summary>
		/// <param name="AppUri">Based on the URI scheme of the app</param>
		/// <param name="WebUri">Web URI</param>
		/// <param name="Options">LaunchOptions that needs to be passed</param>
		/// <returns>Returns a LaunchLinkResult object</returns>
		public async static Task<LaunchLinkResult> LaunchLinkAsync(Uri AppUri, Uri WebUri, LauncherOptions Options)
		{
			LaunchLinkResult result = new LaunchLinkResult() { IsLinkLaunched = false, LinkLaunchedBy = LinkLauncher.Unknown };
			switch (await Launcher.QueryUriSupportAsync(AppUri, LaunchQuerySupportType.Uri))
			{
				case LaunchQuerySupportStatus.Available:
					result.LinkLaunchedBy = LinkLauncher.App;
					result.IsLinkLaunched = await Launcher.LaunchUriAsync(AppUri, Options);
					break;
				default:
					result.LinkLaunchedBy = LinkLauncher.WebBrowser;
					result.IsLinkLaunched = await Launcher.LaunchUriAsync(WebUri, Options);
					break;
			}
			return result;
		}
	}

	public class LaunchLinkResult
	{
		public LinkLauncher LinkLaunchedBy;
		public Boolean IsLinkLaunched;
	}

	/// <summary>
	/// Tells what handled the link, the App or the WebBrowser. Returns Unknown if any error occurs.
	/// </summary>
	public enum LinkLauncher
	{
		App,
		WebBrowser,
		Unknown
	}
}
