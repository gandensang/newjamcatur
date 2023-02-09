using System;
using System.Globalization;

namespace JamCaturOffline.Converters
{
    public class HourCheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var response = false;
            try
            {

                TimeSpan getTime = (TimeSpan)value;
                if (getTime > TimeSpan.FromMinutes(60))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

