using System.Windows;
using System.Windows.Controls;

namespace Atemoya.Classes.Helpers.Controls {

    public static class PasswordHelper {

        public static readonly DependencyProperty SecurePasswordProperty = DependencyProperty.RegisterAttached("SecurePassword", typeof(string), typeof(PasswordHelper), new PropertyMetadata(null, OnSecurePasswordPropertyChanged));

        public static string GetSecurePassword(DependencyObject obj) => (string)obj.GetValue(SecurePasswordProperty);

        public static void SetSecurePassword(DependencyObject obj, string value) => obj.SetValue(SecurePasswordProperty, value);

        private static void OnSecurePasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is PasswordBox passwordBox)
                passwordBox.Password = e.NewValue as string;
        }

    }

}