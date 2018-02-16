using Imprint.IO.Runtime;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Imprint.IO.Components
{
	public class MetroWindow : Window
	{
		#region Fields

		private ICommand _closeCommand;
		private ICommand _minimizeCommand;
		private ICommand _restoreCommand;

		#endregion

		#region Constructor

		static MetroWindow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroWindow), new FrameworkPropertyMetadata(typeof(MetroWindow)));
		}

		public MetroWindow()
		{
		}

		#endregion

		#region Members

		public ICommand CloseCommand
		{
			get
			{
				if (_closeCommand == null)
					_closeCommand = new DelegateCommand(_ => { 
						Close();
						Process.GetCurrentProcess().Kill();
					});

				return _closeCommand;
			}
		}

		public ICommand MinimizeCommand
		{
			get
			{
				if (_minimizeCommand == null)
					_minimizeCommand = new DelegateCommand(_ => WindowState = System.Windows.WindowState.Minimized);

				return _minimizeCommand;
			}
		}

		public ICommand RestoreCommand
		{
			get
			{
				if (_restoreCommand == null)
					_restoreCommand = new DelegateCommand(_ =>
					{
						if (WindowState == System.Windows.WindowState.Normal)
							WindowState = System.Windows.WindowState.Maximized;
						else
							WindowState = System.Windows.WindowState.Normal;
					});

				return _restoreCommand;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.OriginalSource == this || e.Source == this) return;

			if (e.LeftButton == MouseButtonState.Pressed && !e.Source.GetType().Name.Equals("WebBrowser"))
			{
				DragMove();
			}

			base.OnMouseMove(e);
		}

		#endregion
	}
}