using System;
using System.Globalization;
using Atemoya.Classes.Converters.Base;

namespace Atemoya.Classes.Converters {

    public class CountToBoolConverter : Converter<CountToBoolConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (!(value is int)) {
                return false;
            }

            return (int)value >= 1;

        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }

}