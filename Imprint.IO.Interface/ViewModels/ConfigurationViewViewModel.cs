using Imprint.IO.Runtime;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Threading;

namespace Imprint.IO.Interface
{
	internal class ConfigurationViewViewModel : ViewModelBase, IJSBinderCallback, IConfigurationViewViewModel
	{
		#region Fields

		private ManualResetEvent _resetEvent = new ManualResetEvent(true);

		public ManualResetEvent ResetEvent
		{
			get { return _resetEvent; }
		}

		private Dispatcher _dispatcher;

		private IFactory _factory;

		private ObservableCollection<HTMLElement> _elements;

		private IMainWindowViewModel _viewmodel;

		private DataTable _dataTable;

		public DataTable ResultTable
		{
			get { return _dataTable; }
			set { _dataTable = value; }
		}

		private string _CurrentHeader;

		private int _iterator = -1;

		private int _columnIndex = 0;

		#endregion

		public ConfigurationViewViewModel(IFactory factory, IMainWindowViewModel viewmodel)
		{
			_factory = factory;

			_viewmodel = viewmodel;

			_dispatcher = Dispatcher.CurrentDispatcher;

			_elements = new ObservableCollection<HTMLElement>();

			_factory.GetBinder().RegisterCallback(this);
		}

		public ObservableCollection<HTMLElement> Elements
		{
			get { return _elements; }
		}

		public void OnTagIdentified(string tagName, string className, string id, string xpath)
		{
			className = string.Join(".", className.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

			_dataTable = null;
			_iterator = -1;

			_dispatcher.BeginInvoke(new Action(() => {

				_elements.Add(new HTMLElement() { TagName = tagName, ClassName = className, ID = id, Xpath = xpath, Header = string.Format("Column_{0}", _elements.Count + 1), ElementType = _viewmodel.ElementToTrack });

				_viewmodel.ElementToTrack = Element.None;

			}));

		}

		public void OnTagValueIdentified(string callerID, string content, string value, string image, string url, string header, Element type)
		{
			if (_dataTable == null)
			{
				_dataTable = new DataTable("Results");

				Elements.ToList().ForEach(e => _dataTable.Columns.Add(new DataColumn(e.Header, typeof(string)))); 
			}

			if ("-EOF-".Equals(content))
			{
				if (_dataTable.Columns.Count == _columnIndex)
					_resetEvent.Set();

				return;
			}

			DataRow row;

			if (!header.Equals(_CurrentHeader))
			{
				_CurrentHeader = header;
				_iterator = -1;
				_columnIndex++;
			}

			if (_dataTable.Columns.IndexOf(_dataTable.Columns[header]) == 0 || _iterator > _dataTable.Rows.Count)
				row = _dataTable.NewRow();
			else
			{
				_iterator++;

				row = _dataTable.Rows[_iterator];
			}

			switch (type)
			{
 				case Element.Text:
					row[header] = Regex.Replace(content, @"\t|\n|\r", string.Empty).Trim();
					break;
				case Element.Image:
					row[header] = image;
					break;
				case Element.Url:
					row[header] = url;
					break;
				case Element.List:
					row[header] = content;
					break;
			}

			_dataTable.Rows.Add(row);
		}

		public void OnResponse(string callerID, string json)
		{

		}
	}
}