using CefSharp;
using CefSharp.Wpf;
using Imprint.IO.Runtime;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Imprint.IO.Components
{
	public class WebBrowser : ContentControl
	{
		#region Fields

		private ChromiumWebBrowser _browser;

		private MenuHandler _menuHandler;
		private ICommand _loadCommand;

		private string _name;
		private object _binder;

		#endregion

		#region Constructor

		static WebBrowser()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(WebBrowser), new FrameworkPropertyMetadata(typeof(WebBrowser)));
		}

		public WebBrowser()
		{
			Loaded += (_, __) => Initialize();
		}

		#endregion

		#region Properties

		public ChromiumWebBrowser BrowserInternal
		{
			get { return _browser; }
		}

		public ICommand LoadCommand
		{
			get
			{
				if (_loadCommand == null)
					_loadCommand = new DelegateCommand(p =>
					{
						_browser.Load(URL);
					});

				return _loadCommand;
			}
		}

		public ICommand BackCommand
		{
			get
			{
				if (_browser == null) return null;

				return _browser.BackCommand;
			}
		}

		public ICommand ForwardCommand
		{
			get
			{
				if (_browser == null) return null;

				return _browser.ForwardCommand;
			}
		}

		public ICommand ReloadCommand
		{
			get
			{
				if (_browser == null) return null;

				return _browser.ReloadCommand;
			}
		}

		#endregion

		#region Dependency Properties

		public static readonly DependencyProperty IsContextMenuEnabledProperty = DependencyProperty.Register("IsContextMenuEnabled", typeof(bool), typeof(WebBrowser), new PropertyMetadata(false, OnIsContextMenuEnabledPropertyChanged));

		public static readonly DependencyProperty URLProperty = DependencyProperty.Register("URL", typeof(string), typeof(WebBrowser), new PropertyMetadata(string.Empty, OnURLPropertyChanged));

		public string URL
		{
			get { return (string)GetValue(URLProperty); }
			set { SetValue(URLProperty, value); }
		}

		public bool IsContextMenuEnabled
		{
			get { return (bool)GetValue(IsContextMenuEnabledProperty); }
			set { SetValue(IsContextMenuEnabledProperty, value); }
		}

		public static void OnURLPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			// ((WebBrowser)d)._browser.Load((string)e.NewValue); 
		}

		public static void OnIsContextMenuEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((WebBrowser)d)._menuHandler.OnIsContextMenuEnabledChanged((bool)e.NewValue);
		}

		#endregion

		#region Overriden Members

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_browser = GetTemplateChild("PART_BROWSER") as ChromiumWebBrowser;

			_browser.RegisterJsObject(_name, _binder);

			_browser.BrowserSettings.Javascript = CefState.Enabled;
		}

		#endregion

		#region Members

		public void RegisterJsObject(string name, object binder)
		{
			_name = name;
			_binder = binder;
		}

		private void Initialize()
		{
			_menuHandler = new MenuHandler(this);

			_browser.LoadingStateChanged += (_, e) => Dispatcher.BeginInvoke(new Action(() => { if (!string.IsNullOrEmpty(_browser.Address)) URL = _browser.Address; }));
		}

		#endregion

		#region ** private class handlers

		private class MenuHandler : IContextMenuHandler
		{
			private ChromiumWebBrowser _browser;
			private WebBrowser _webBrowser;
			private bool _enabled = false;

			public MenuHandler(WebBrowser webBrowser)
			{
				_browser = webBrowser.BrowserInternal;
				_webBrowser = webBrowser;

				if (_browser.IsInitialized)
					_browser.MenuHandler = this;
				else
					_browser.FrameLoadEnd += (_, __) => _browser.MenuHandler = this;
			}

			public void OnIsContextMenuEnabledChanged(bool value)
			{
				_enabled = value;
			}

			public void OnBeforeContextMenu(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model)
			{
			}

			public bool OnContextMenuCommand(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.CefMenuCommand commandId, CefSharp.CefEventFlags eventFlags)
			{
				// return false to execute command.
				return !_enabled;
			}

			public void OnContextMenuDismissed(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame)
			{
			}

			public bool RunContextMenu(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model, CefSharp.IRunContextMenuCallback callback)
			{
				// return false to display context menu.
				return !_enabled;
			}
		}

		#endregion
	}
}