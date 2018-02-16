using Microsoft.Win32;
using System.Windows;

namespace Imprint.IO.Interface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var view = new MainWindow();
			var viewmodel = Factory.Instance.GetMainWindowViewModel() as MainWindowViewModel;
			viewmodel.WebBrowser = view.GetBrowser();
			MainWindow.DataContext = viewmodel;
			this.MainWindow = view;

			this.MainWindow.Show();
		}
    }
}