using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HarvestDotNet.TestApp
{
  /// <summary>
  /// Interaction logic for ShellView.xaml
  /// </summary>
  public partial class ShellView : UserControl
  {
    public ShellView()
    {
      InitializeComponent();
    }
  }

  public class TypeToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (null == value)
        return "null";
      return value.GetType().Name;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return DependencyProperty.UnsetValue;
    }
  }

  public class BooleanInverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is bool)
        return (!(bool) value);
      return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is bool)
        return (!(bool)value);
      return DependencyProperty.UnsetValue;
    }
  }
}
