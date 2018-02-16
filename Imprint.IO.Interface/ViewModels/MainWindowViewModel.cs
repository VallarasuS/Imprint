using Imprint.IO.Components;
using Imprint.IO.Runtime;
using System.Windows.Input;

namespace Imprint.IO.Interface
{
	public partial class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
	{
		#region Fields

		private IFactory _factory;
		private WebBrowser _webBrowser;
		private Element _elementToTrack = Element.None;

		#endregion

		public MainWindowViewModel(IFactory factory)
		{
			_factory = factory;

			Initialize();

			ConfigurationViewViewModel = _factory.GetConfigurationViewViewModel(this);
		}

		public Element ElementToTrack
		{
			get { return _elementToTrack; }
			set { _elementToTrack = value; }
		}

		public ICommand EnableTrackingImageCommand { get; set; }

		public ICommand EnableTrackingTextCommand { get; set; }

		public ICommand EnableTrackingURLCommand { get; set; }

        public ICommand EnableTrackingNextURLCommand { get; set; }

        public ICommand EnableTrackingListCommand { get; set; }

		public ICommand EnableTrackingTableCommand { get; set; }

		public ICommand DisableTrackingCommand { get; set; }

		public ICommand FectchConfiguredDataCommand { get; set; }

		public ICommand ScrollCommand { get; set; }

		public IConfigurationViewViewModel ConfigurationViewViewModel
		{
			get;
			set;
		}

		public WebBrowser WebBrowser
		{
			get { return _webBrowser; }
			set 
			{
				_webBrowser = value;

				if (_webBrowser == null) return;
			}
		}
	}
}