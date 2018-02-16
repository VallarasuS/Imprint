
using Imprint.IO.Runtime;
namespace Imprint.IO.Interface
{
	public class Factory : IFactory
	{
		#region Fields

		private static IFactory _factory = new Factory();
		private static JSBinder _binder = new JSBinder();
		private static QueryBuilder _queryBuilder = new QueryBuilder();
		
		#endregion

		#region Constructor

		private Factory()
		{
		}

		#endregion

		#region ** IFactory

		public static IFactory Instance
		{
			get
			{
				return _factory;
			}
		}

		IFactory IFactory.Instance
		{
			get { return _factory; }
		}

		IMainWindowViewModel IFactory.GetMainWindowViewModel()
		{
			return new MainWindowViewModel(this);
		}

		IConfigurationViewViewModel IFactory.GetConfigurationViewViewModel(IMainWindowViewModel viewmodel)
		{
			return new ConfigurationViewViewModel(this, viewmodel);
		}

		JSBinder IFactory.GetBinder()
		{
			return _binder;
		}

		QueryBuilder IFactory.GetQueryBuilder()
		{
			return _queryBuilder;
		}

		#endregion
	}
}