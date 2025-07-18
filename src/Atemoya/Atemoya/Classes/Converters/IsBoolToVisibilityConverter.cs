﻿using System;
using System.Globalization;
using System.Windows;
using Atemoya.Classes.Converters.Base;

namespace Atemoya.Classes.Converters {

    public class IsBoolToVisibilityConverter : Converter<IsBoolToVisibilityConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            if (!(value is bool)) {
                return Visibility.Collapsed;
            }

            return (bool)value ? Visibility.Visible : Visibility.Collapsed;

        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }

}