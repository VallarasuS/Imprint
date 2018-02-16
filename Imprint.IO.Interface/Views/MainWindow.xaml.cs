using Imprint.IO.Components;
using System.Windows;

namespace Imprint.IO.Interface
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		public MainWindow()
		{
			InitializeComponent();

			browser.RegisterJsObject(Constants.BINDER_NAME, Factory.Instance.GetBinder()); 
		}

		void browser_Loaded(object sender, RoutedEventArgs e)
		{
		}

		void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			

			//browser.AttachHook(Factory.Instance.GetBinder(), Constants.BINDER_NAME);
		}

		public WebBrowser GetBrowser()
		{
			return browser;
		}
	}
}