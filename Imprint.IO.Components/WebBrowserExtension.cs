using Imprint.IO.Runtime;

namespace Imprint.IO.Components
{
	public static class WebBrowserExtension
	{
		public static void AttachHook(this WebBrowser browser, JSBinder binder, string name)
		{
			if(browser !=null && browser.BrowserInternal == null)
				browser.Loaded += (_, __) => browser.BrowserInternal.RegisterJsObject(name, binder);
			else
				browser.BrowserInternal.RegisterJsObject(name, binder);
		}

		public static void ExecuteScript(this WebBrowser browser, string script)
		{
			var chromium = browser.BrowserInternal.GetBrowser();

			chromium.MainFrame.ExecuteJavaScriptAsync(script); 
		}

		public static void EvaluateScript(this WebBrowser browser, string script)
		{
			var chromium = browser.BrowserInternal.GetBrowser();

			chromium.MainFrame.EvaluateScriptAsync(script).ContinueWith(t => { });
		}
	}
}