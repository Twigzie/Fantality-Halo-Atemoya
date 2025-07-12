using Atemoya.Classes.Converters.Base;
using System;
using System.Globalization;

namespace Atemoya.Classes.Converters {

    public class IsEditingConverter : Converter<IsEditingConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            return (bool)value == false;

        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }

}