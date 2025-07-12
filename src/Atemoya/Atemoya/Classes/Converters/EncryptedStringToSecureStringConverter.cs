using Atemoya.Classes.Converters.Base;
using Atemoya.Classes.Helpers.Security;
using System;
using System.Globalization;
using System.Security;

namespace Atemoya.Classes.Converters {

    public class EncryptedStringToSecureStringConverter : Converter<EncryptedStringToSecureStringConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            try {

                if (!(value is string))
                    return new SecureString();

                var result = new SecureString();
                foreach (char c in Encryption.DecryptString((string)value))
                    result.AppendChar(c);
                result.MakeReadOnly();

                return result;

            }
            catch {
                return new SecureString();
            }

        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }

}