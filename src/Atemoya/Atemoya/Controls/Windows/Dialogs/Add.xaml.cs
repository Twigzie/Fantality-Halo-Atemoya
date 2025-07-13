using Atemoya.Classes.Config;
using Atemoya.Classes.Entities;
using Atemoya.Classes.Helpers;
using Atemoya.Classes.Helpers.Extensions;
using Atemoya.Classes.Helpers.Security;
using Atemoya.Classes.Models;
using Atemoya.Classes.Models.Application;
using PropertyChanged;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using static Atemoya.Classes.Helpers.Extensions.BaseExtensions;

namespace Atemoya.Controls.Windows.Dialogs {

    [AddINotifyPropertyChangedInterface]
    public partial class Add : Window {

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

        public Add() {
            InitializeComponent();
        }

        private void OnAdd(object sender, RoutedEventArgs e) {
            try {

                if (string.IsNullOrEmpty(UI_txtName.Text)) {
                    MessageBox.Show("Please enter a Gamertag for this account.", "Add", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(UI_txtEmail.Text)) {
                    MessageBox.Show("Please enter an Email for this account.", "Add", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(UI_txtPassword.Text)) {
                    MessageBox.Show("Please enter a Password for this account.", "Add", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (UI_comboInput.SelectedIndex <= -1) {
                    MessageBox.Show("Please select an Input for this account.", "Add", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show($"Add this account?\r\n\n" +
                    $"Gamertag: {UI_txtName.Text}\r\n" +
                    $"Email: {UI_txtEmail.Text}\r\n" +
                    $"Password: {UI_txtPassword.Text}\r\n" +
                    $"Input: {UI_comboInput.Text}\r\n\n" +
                    $"Remember: After adding this account, Your email, password and gamertag will be encrypted. Your password is only visible now to ensure that you entered it correctly and will be hidden using a password box in the future.", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    return;

                var time = GetEpochTime();
                Locator.Instance.Gamertags.Tags.Add(new Gamertag() {
                    Created = time,
                    Updated = time,
                    Name = Encryption.EncryptString(UI_txtName.Text),
                    Email = Encryption.EncryptString(UI_txtName.Text),
                    Password = Encryption.EncryptString(UI_txtName.Text),
                    Input = (InputType)UI_comboInput.SelectedIndex,
                    IsNew = true
                });

                AppSettings.Instance.Save();

                DialogResult = true;

            }
            catch (Exception) {

                throw;
            }
        }
        private void OnCancel(object sender, RoutedEventArgs e) {
            OnWindowClose(sender, e);
        }
        private void OnWindowClose(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }

    }

}