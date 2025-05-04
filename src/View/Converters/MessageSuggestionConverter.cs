using System;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace Src.View.Converters
{
    public partial class MessageSuggestionConverter : IValueConverter
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
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
