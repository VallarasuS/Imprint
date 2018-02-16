using Imprint.IO.Components;
using Imprint.IO.Runtime;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace Imprint.IO.Interface
{
	public partial class MainWindowViewModel : ViewModelBase
	{
		public void Initialize()
		{
			// Scrap - Commands
			EnableTrackingTextCommand = new DelegateCommand(_ => EnableTrackingTextCommandExecute());
			EnableTrackingImageCommand = new DelegateCommand(_ => EnableTrackingImageCommandExecute());
			EnableTrackingURLCommand = new DelegateCommand(_ => EnableTrackingURLCommandExecute());
            EnableTrackingNextURLCommand = new DelegateCommand(_ => EnableTrackingNextURLCommandExecute());
            EnableTrackingListCommand = new DelegateCommand(_ => EnableTrackingListCommandExecute());
			EnableTrackingTableCommand = new DelegateCommand(_ => EnableTrackingTableCommandExecute());

			// Action - Commands
			ScrollCommand = new DelegateCommand(_ => ScrollCommandExecute());

			DisableTrackingCommand = new DelegateCommand(_ => DisableTrackingCommandExecute());
			FectchConfiguredDataCommand = new DelegateCommand(_ => FectchConfiguredDataCommandExecute());

			OnPropertyChanged(string.Empty);
		}

		private void ScrollCommandExecute()
		{
			var builder = _factory.GetQueryBuilder();

			var xpath = builder.GetXpath();
			var jquery = builder.GetJQuery();
			var enableTrack = builder.HighlightURL();
			var scroll = builder.GetScroll();

			WebBrowser.ExecuteScript(jquery + " " + xpath + "  " + scroll);
		}

		private void EnableTrackingTableCommandExecute()
		{
			DisableTrackingCommandExecute();

			MessageBox.Show("Not implemented");
		}

		private void EnableTrackingListCommandExecute()
		{
			DisableTrackingCommandExecute();

			ElementToTrack = Element.List;

			var builder = _factory.GetQueryBuilder();

			var xpath = builder.GetXpath();
			var jquery = builder.GetJQuery();
			var find = builder.IdentifyTagUnderMouse(Constants.IDENTIFIER_FUNCTION, Constants.BINDER_NAME, JSBinder.IDENTIFIER_FUNCTION, "LI");
			var subscribe = builder.EventSubscribe(Constants.IDENTIFIER_FUNCTION, Events.MouseUp);
			var enableTrack = builder.HighlightList();

			WebBrowser.EvaluateScript(jquery + " " + xpath + "  " + subscribe + "  " + find + " " + enableTrack);
		}

        private void EnableTrackingNextURLCommandExecute()
        {
            DisableTrackingCommandExecute();

            ElementToTrack = Element.Next;

            var builder = _factory.GetQueryBuilder();

            var xpath = builder.GetXpath();
            var jquery = builder.GetJQuery();
            var find = builder.IdentifyTagUnderMouse(Constants.IDENTIFIER_FUNCTION, Constants.BINDER_NAME, JSBinder.IDENTIFIER_FUNCTION, "A");
            var subscribe = builder.EventSubscribe(Constants.IDENTIFIER_FUNCTION, Events.MouseUp);
            var enableTrack = builder.HighlightURL();

            WebBrowser.EvaluateScript(jquery + " " + xpath + "  " + subscribe + "  " + find + " " + enableTrack);
        }

        private void EnableTrackingURLCommandExecute()
		{
			DisableTrackingCommandExecute();

			ElementToTrack = Element.Url;

			var builder = _factory.GetQueryBuilder();

			var xpath = builder.GetXpath();
			var jquery = builder.GetJQuery();
			var find = builder.IdentifyTagUnderMouse(Constants.IDENTIFIER_FUNCTION, Constants.BINDER_NAME, JSBinder.IDENTIFIER_FUNCTION, "A");
			var subscribe = builder.EventSubscribe(Constants.IDENTIFIER_FUNCTION, Events.MouseUp);
			var enableTrack = builder.HighlightURL();

			WebBrowser.EvaluateScript(jquery + " " + xpath + "  " + subscribe + "  " + find + " " + enableTrack);
		}

		public void EnableTrackingTextCommandExecute()
		{
			DisableTrackingCommandExecute();

			ElementToTrack = Element.Text;

			var builder =  _factory.GetQueryBuilder();

			var xpath = builder.GetXpath();
			var jquery = builder.GetJQuery ();
			var find = builder.IdentifyTagUnderMouse(Constants.IDENTIFIER_FUNCTION, Constants.BINDER_NAME, JSBinder.IDENTIFIER_FUNCTION, "TEXT");
			var subscribe = builder.EventSubscribe(Constants.IDENTIFIER_FUNCTION, Events.MouseUp);
			var enableTrack = builder.EnableHighlight();

			WebBrowser.EvaluateScript(jquery + " " + xpath + "  " + subscribe + "  " + find + " " + enableTrack);
		}

		public void EnableTrackingImageCommandExecute()
		{
			DisableTrackingCommandExecute();

			ElementToTrack = Element.Image;

			var builder = _factory.GetQueryBuilder();

			var xpath = builder.GetXpath();
			var jquery = builder.GetJQuery();
			var find = builder.IdentifyTagUnderMouse(Constants.IDENTIFIER_FUNCTION, Constants.BINDER_NAME, JSBinder.IDENTIFIER_FUNCTION, "IMG");
			var subscribe = builder.EventSubscribe(Constants.IDENTIFIER_FUNCTION, Events.MouseUp);
			var enableTrack = builder.HighlightImage();

			WebBrowser.EvaluateScript(jquery + " " + xpath + "  " + subscribe + "  " + find + " " + enableTrack);
		}

		//public void EnableTrackingCommandExecute()
		//{
		//	var builder = _factory.GetQueryBuilder();

		//	var xpath = builder.GetXpath();
		//	var jquery = builder.GetJQuery();
		//	var find = builder.IdentifyTagUnderMouse(Constants.IDENTIFIER_FUNCTION, Constants.BINDER_NAME, JSBinder.IDENTIFIER_FUNCTION);
		//	var subscribe = builder.EventSubscribe(Constants.IDENTIFIER_FUNCTION, Events.MouseUp);
		//	var enableTrack = builder.EnableHighlight();

		//	WebBrowser.EvaluateScript(jquery + " " + xpath + "  " + subscribe + "  " + find + " " + enableTrack);
		//}

		public void DisableTrackingCommandExecute()
		{
			ElementToTrack = Element.None;

			var builder = _factory.GetQueryBuilder();
			var disable = builder.DisableHighlight();
			var unsub = builder.EventUnSubscribe(Events.MouseUp);

			WebBrowser.ExecuteScript(disable + " " + unsub);
		}

		public void FectchConfiguredDataCommandExecute()
		{
			ConfigurationViewViewModel.ResetEvent.Reset();

			foreach (var config in ConfigurationViewViewModel.Elements)
			{
				if (!string.IsNullOrEmpty(config.ClassName))
				{
					var builder = _factory.GetQueryBuilder();
					var jq = builder.GetJQuery();
					var extract = builder.ExtractData("UUID", config.ClassName, Selector.Class, Constants.BINDER_NAME, JSBinder.SCRAPER_INFO, config.Header,config.ElementType);

					if (config.ElementType == Element.List)
						extract = builder.ExtractListData("UUID", config.ClassName, Selector.Class, Constants.BINDER_NAME, JSBinder.SCRAPER_INFO, config.Header, config.ElementType);

					WebBrowser.EvaluateScript(jq + " " + extract);
				}
				else if(!string.IsNullOrEmpty(config.ID))
				{

				}
				else if (!string.IsNullOrEmpty(config.Xpath))
				{

				}
			}

			if (ConfigurationViewViewModel.ResetEvent.WaitOne())
			{
				SaveFileDialog saveDialog = new SaveFileDialog();

				saveDialog.Filter = "Comma Separated Values (.csv)|*.csv|XML File (.xml)|*.xml|JavaScript Object Notation File(.json)|*.json|Microsoft Excel File (.xls)|*.xls";

				var result = saveDialog.ShowDialog();

				if (result.HasValue && result.Value)
				{
					if (saveDialog.FilterIndex == 1) // CSV
					{
						var writer = CSVWriter.Create(saveDialog.FileName, ConfigurationViewViewModel.ResultTable);
						writer.Write();
					}
					else if(saveDialog.FilterIndex == 2) //XML
					{
						var writer = XMLWriter.Create(saveDialog.FileName, ConfigurationViewViewModel.ResultTable);
						writer.Write();
					}
				}
			}
		}
	}
}