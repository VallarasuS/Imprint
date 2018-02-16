using System;
using System.Windows;
using System.Windows.Data;

namespace Imprint.IO.Components
{
	public class BooleanToVisibilityNegator : IValueConverter 
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool isVisible = (bool)value;

			return isVisible ? Visibility.Hidden : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			Visibility visibility = (Visibility)value;

			return visibility != Visibility.Visible;
		}
	}
}