using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.View.Converters
{
    class MessageSuggestionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string text)
            {
                if (text.Contains("inoffensive", StringComparison.OrdinalIgnoreCase))
                {
                    return new SolidColorBrush(Colors.Green);
                }
                else if (text.Contains("offensive", StringComparison.OrdinalIgnoreCase))
                {
                    return new SolidColorBrush(Colors.Red);
                }
            }
            return new SolidColorBrush(Colors.Black); // Default color
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException(); // No need for ConvertBack
        }
    }
}
