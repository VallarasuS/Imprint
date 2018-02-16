using System.ComponentModel;

namespace Imprint.IO.Runtime
{
	public class ViewModelBase : IViewModelBase
	{
		#region Fields

		private PropertyChangedEventHandler _propertyChanged;
		private string _name;

		#endregion

		#region ** IViewModelBase

		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				_propertyChanged += value;
			}
			remove
			{
				_propertyChanged -= value;
			}
		}

		public string Name
		{
			get { return _name; }
			set {
				_name = value;

				OnPropertyChanged("Name");
			}
		}

		protected virtual void OnPropertyChanged(string name)
		{
			var handler = _propertyChanged;

			if (handler == null) return;

			handler(this, new PropertyChangedEventArgs(name));
		}

		public virtual void Dispose()
		{
		}

		#endregion
	}
}