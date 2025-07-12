using System;
using System.Globalization;
using System.Windows.Media.Imaging;
using Atemoya.Classes.Converters.Base;
using Atemoya.Classes.Helpers;
using static System.Windows.Application;

namespace Atemoya.Classes.Converters {

    public class InputToImageConverter : Converter<InputToImageConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                if (value is InputType type) {
                    switch (type) {
                        case InputType.Mouse:
                            return (BitmapImage)Current.FindResource("UI_IconMouse");
                        case InputType.Controller:
                            return (BitmapImage)Current.FindResource("UI_IconController");
                    }
                }
                return (BitmapImage)Current.FindResource("map_unknown");

            } catch {
                return (BitmapImage)Current.FindResource("map_unknown");
            }
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }
}
