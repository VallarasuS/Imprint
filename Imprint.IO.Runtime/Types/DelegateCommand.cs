using System;
using System.Windows.Input;

namespace Imprint.IO.Runtime
{
	public class DelegateCommand : ICommand
	{
		#region Fields

		private Predicate<object> _canExecute;
		private Action<object> _execute;

		#endregion

		#region Constructor

		public DelegateCommand(Predicate<object> canExecute, Action<object> execute)
		{
			_canExecute = canExecute;
			_execute = execute;
		}

		public DelegateCommand(Action<object> execute)
			: this(null, execute)
		{
		}

		#endregion

		#region

		public bool CanExecute(object parameter)
		{
			if (_canExecute == null) return true;

			try
			{
				return _canExecute(parameter);
			}
			catch
			{
				return true;
			}
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			if (_execute == null) return;

			try
			{
				_execute(parameter);
			}
			catch
			{ }
		}

		#endregion
	}
}