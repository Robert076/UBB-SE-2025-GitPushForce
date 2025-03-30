using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.View.Converters
{
    class AmountColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int amount)
            {
                // Return Red if negative, Green if positive
                if (amount < 0)
                {
                    return new SolidColorBrush(Microsoft.UI.Colors.Red);
                }
                else if (amount > 0)
                {
                    return new SolidColorBrush(Microsoft.UI.Colors.Green);
                }
                else
                {
                    return new SolidColorBrush(Microsoft.UI.Colors.Black); // Default color for zero
                }
            }
            return new SolidColorBrush(Microsoft.UI.Colors.Black); // Default color
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException(); // We don't need to support ConvertBack
        }
    }
}
