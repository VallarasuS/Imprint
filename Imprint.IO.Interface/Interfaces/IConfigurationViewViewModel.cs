using Imprint.IO.Runtime;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;

namespace Imprint.IO.Interface
{
	public interface IConfigurationViewViewModel
	{
		ObservableCollection<HTMLElement> Elements { get; }

		DataTable ResultTable { get; set; }

		ManualResetEvent ResetEvent { get; }
	}
}