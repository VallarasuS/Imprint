using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Imprint.IO.Runtime
{
	public class JSBinder
	{
		#region Fields

		public const string IDENTIFIER_FUNCTION = "found";
		public const string SCRAPER_INFO = "notify";
		public const string EVALUATER_INFO = "respond";

		private List<IJSBinderCallback> callbacks;
		private object _syncRoot = new object();

		#endregion

		#region Constructor

		public JSBinder()
		{
			callbacks = new List<IJSBinderCallback>();
		}

		#endregion

		#region Members

		public void Found(string tagName, string className, string id, string xpath)
		{
			lock (_syncRoot)
			{
				foreach (var callback in callbacks)
				{
					try
					{
						if (callback == null) continue;

						callback.OnTagIdentified(tagName, className, id, xpath);
					}
					catch
					{
					}
				}
			}
		}

		public void Notify(string callerID, string content, string value, string image, string url, string header, string type)
		{
			lock (_syncRoot)
			{
				foreach (var callback in callbacks)
				{
					try
					{
						if (callback == null) continue;

						Func<string, string> trim = s => string.IsNullOrEmpty(s) ? string.Empty : Regex.Replace(s, @"\t|\n|\r", string.Empty).Trim();

						callback.OnTagValueIdentified(callerID, trim(content), trim(value), trim(image), trim(url), trim(header), (Element)Enum.Parse(typeof(Element), type));
					}
					catch
					{
					}
				}
			}
		}

		public void Respond(string callerID, string json)
		{
			lock (_syncRoot)
			{
				foreach (var callback in callbacks)
				{
					try
					{
						if (callback == null) continue;

						callback.OnResponse(callerID, json);
					}
					catch
					{
					}
				}
			}
		}

		public void RegisterCallback(IJSBinderCallback callback)
		{
			lock (_syncRoot)
			{
				if (callbacks.Contains(callback))
					return;

				callbacks.Add(callback);
			}
		}

		public void UnRegisterCallback(IJSBinderCallback callback)
		{
			lock (_syncRoot)
			{
				if (!callbacks.Contains(callback))
					return;

				callbacks.Remove(callback);
			}
		}

		#endregion
	}
}