using System;
using System.Globalization;
using System.Windows.Data;

namespace DAN_XLIII_Kristina_Garcia_Francisco.Converter
{
    /// <summary>
    /// Convertes the id of the user to the position
    /// </summary>
    class ReportPositionConverter : IValueConverter
    {
        /// <summary>
        /// Converts the parameter value into the user position
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Service service = new Service();
            for (int i = 0; i < service.GetAllUsers().Count; i++)
            {
                if (service.GetAllUsers()[i].UserID == (int)value)
                {
                    return service.GetAllUsers()[i].Position;
                }
            }

            return value;
        }

        /// <summary>
        /// Converts back
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
