using Imprint.IO.Runtime;

namespace Imprint.IO.Interface
{
	public interface IFactory
	{
		IFactory Instance { get;}

		IMainWindowViewModel GetMainWindowViewModel();

		IConfigurationViewViewModel GetConfigurationViewViewModel(IMainWindowViewModel viewmodel);

		JSBinder GetBinder();

		QueryBuilder GetQueryBuilder();
	}
}