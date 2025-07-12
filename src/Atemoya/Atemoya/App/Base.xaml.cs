using Atemoya.Classes.Config;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Atemoya.App {

    public partial class Base : Application {

        private void OnStartup(object sender, StartupEventArgs e) {
            try {
                AppSettings.Instance.Load();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Atemoya", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                Environment.Exit(0);
            }
        }

        private void OnException(object sender, DispatcherUnhandledExceptionEventArgs e) {
            try {
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Atemoya", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                Environment.Exit(0);
            }
        }
    }
}