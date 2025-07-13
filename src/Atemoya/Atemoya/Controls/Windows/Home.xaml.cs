using Atemoya.Classes.Github;
using Atemoya.Classes.Helpers;
using PropertyChanged;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Atemoya.Controls.Windows {

    [AddINotifyPropertyChangedInterface]
    public partial class Home : Window {

        #region Interop

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            switch (msg) {
                case 0x0024:
                    Native.WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
                case 0x0046:

                    var pos = (Native.WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(Native.WINDOWPOS));
                    if ((pos.flags & (int)Native.SWP.NOMOVE) != 0) {
                        return IntPtr.Zero;
                    }

                    var wnd = (Window)HwndSource.FromHwnd(hwnd)?.RootVisual;
                    if (wnd == null) {
                        return IntPtr.Zero;
                    }

                    var changed = false;
                    if (pos.cx < MinWidth) {
                        pos.cx = (int)MinWidth;
                        changed = true;
                    }
                    if (pos.cy < MinHeight) {
                        pos.cy = (int)MinHeight;
                        changed = true;
                    }

                    if (!changed) {
                        return IntPtr.Zero;
                    }

                    Marshal.StructureToPtr(pos, lParam, true);
                    handled = true;
                    break;
            }
            return (IntPtr)0;
        }

        #endregion
        #region Overides

        protected override void OnSourceInitialized(EventArgs e) {

            base.OnSourceInitialized(e);

            var handle = (new WindowInteropHelper(this)).Handle;
            if (handle != IntPtr.Zero) {
                HwndSource.FromHwnd(handle)?.AddHook(WindowProc);
            }

        }

        #endregion

        public Home() {
            InitializeComponent();
        }

        private void OnMenuGithub(object sender, RoutedEventArgs e) {
            Process.Start("https://github.com/Twigzie/Fantality-Halo-Atemoya");
        }
        private void OnMenuAbout(object sender, RoutedEventArgs e) {
            MessageBox.Show("Coded with love by: TheDeadNorth\r\n\n" +
                "Reason: I'm lazy and can't be bothered to remember which accounts I have... If any... and the credentials I used...\r\n\n" +
                "Sketchness: Absolutely none.. information you provide is encrypted and can only be decrypted by you and your system. The source is hosted on Github (... > Github) if you still don't believe me, you can always compile it yourself (if you haven't already)\r\n\n" +
                $"Wisdom: {Utils.GetRandomJoke()}", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void OnMenuUpdates(object sender, RoutedEventArgs e) {
            UpdateCommand();
        }

        #region Updates

        public static async void UpdateCommand() {
            await UpdateCommandAsync();
        }
        private static async Task UpdateCommandAsync() {
            try {

                var update = await Updater.GetUpdates();
                if (update.IsAvailable) {
                    if (MessageBox.Show("Would you look at that, looks like theres been progress since you've last ran this application. Update now? (no fancy install, just opens the link in your browser)", "Update Available", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No) == MessageBoxResult.Yes)
                        Process.Start(update.Url);
                }
                else {
                    MessageBox.Show("Like my life, there have been no updates...", "Shame", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) {
                //TODO: Something other than this...
                Debug.WriteLine(ex);
            }
        }

        #endregion

        [SuppressPropertyChangedWarnings]
        private void OnWindowChanged(object sender, EventArgs e) {
            switch (WindowState) {
                case WindowState.Normal:
                    PartBtnRestore.Visibility = Visibility.Collapsed;
                    PartBtnMaximize.Visibility = Visibility.Visible;
                    break;
                case WindowState.Maximized:
                    PartBtnRestore.Visibility = Visibility.Visible;
                    PartBtnMaximize.Visibility = Visibility.Collapsed;
                    break;
                case WindowState.Minimized:
                default:
                    break;
            }
        }
        private void OnWindowMinimize(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }
        private void OnWindowRestore(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Normal;
        }
        private void OnWindowMaximize(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Maximized;
        }
        private void OnWindowClose(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }

    }

}