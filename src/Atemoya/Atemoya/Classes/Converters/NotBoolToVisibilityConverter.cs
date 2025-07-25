﻿using System;
using System.Globalization;
using System.Windows;
using Atemoya.Classes.Converters.Base;

namespace Atemoya.Classes.Converters {

    public class NotBoolToVisibilityConverter : Converter<NotBoolToVisibilityConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (!(value is bool)) {
                return Visibility.Collapsed;
            }

            return (bool)value ? Visibility.Collapsed : Visibility.Visible;

        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }

}