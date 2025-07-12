using System;
using System.Globalization;
using System.Windows.Media.Imaging;
using Atemoya.Classes.Converters.Base;
using Atemoya.Classes.Helpers;
using static System.Windows.Application;

namespace Atemoya.Classes.Converters {

    public class TypeToImageConverter : Converter<TypeToImageConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (!(value is TabType source)) {
                return null;
            }

            switch (source) {
                case TabType.Tags:   return (BitmapImage)Current.FindResource("UI_IconH1");
                case TabType.Export: return (BitmapImage)Current.FindResource("UI_IconH2");
                default:
                    throw new ArgumentOutOfRangeException(nameof(source));
            }

        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }

}