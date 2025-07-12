using Atemoya.Classes.Converters.Base;
using Atemoya.Classes.Helpers.Security;
using System;
using System.Globalization;

namespace Atemoya.Classes.Converters {

    public class EncryptedStringToStringConverter : Converter<EncryptedStringToStringConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            try {

                if (!(value is string))
                    return "";
                return Encryption.DecryptString((string)value);

            }
            catch {
                return "";
            }

        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }

}