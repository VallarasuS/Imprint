using Imprint.IO.Components;
using Imprint.IO.Runtime;

namespace Imprint.IO.Interface
{
	public interface IMainWindowViewModel
	{
		WebBrowser WebBrowser { get; set; }

		IConfigurationViewViewModel ConfigurationViewViewModel { get; set; }

		Element ElementToTrack { get; set; }
	}
}