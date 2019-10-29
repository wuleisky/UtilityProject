using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UtilityResolution.Convert
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool hideControl = false;

            if (parameter != null)
            {
                string paramStr = parameter.ToString();

                string[] result = paramStr.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                bool reseve = false;
                if (result.Length >= 1)
                {
                    reseve = bool.Parse(result[0]);
                    if (reseve)
                    {
                        value = !(bool)value;
                    }
                }
                if (result.Length >= 2)
                {
                    hideControl = bool.Parse(result[1]);
                }

            }

            return (bool)value ? Visibility.Visible : hideControl ? Visibility.Hidden : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool reseve = false;
            if (parameter != null)
            {
                reseve = bool.Parse(parameter.ToString());
            }

            if (reseve)
            {
                return (Visibility)value != Visibility.Visible;
            }
            else
            {
                return (Visibility)value == Visibility.Visible;
            }
        }
    }
}
