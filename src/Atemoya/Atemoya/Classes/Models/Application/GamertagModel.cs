using Atemoya.Classes.Commands;
using Atemoya.Classes.Commands.Behaviors;
using Atemoya.Classes.Config;
using Atemoya.Classes.Entities;
using Atemoya.Classes.Helpers;
using Atemoya.Classes.Helpers.Security;
using Atemoya.Classes.Models.Base;
using Atemoya.Controls.Windows.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace Atemoya.Classes.Models.Application {

    public class GamertagModel : Model {

        #region ISingleton

        static GamertagModel() {
            Instance = new GamertagModel() {
                Type = TabType.Tags
            };
            Instance.Initialize();
        }
        public static GamertagModel Instance {
            get;
            set;
        }

        #endregion

        public string CountPrefix {
            get {
                if (AppSettings.Instance.Gamertags.Count <= 0)
                    return $"(0)";
                return $"({AppSettings.Instance.Gamertags.Count})";
            }
        }
        public ObservableCollection<Gamertag> Tags {
            get {
                return AppSettings.Instance.Gamertags;
            }
            set {
                AppSettings.Instance.Gamertags = value;
            }
        }

        public ICommand CopyEmailCommand => new RelayGenericParamCommand<Gamertag>((gamertag) => {
            try {
                Clipboard.SetText(gamertag.IsEditing ? gamertag.Email : Encryption.DecryptString(gamertag.Email));
                MessageBox.Show("Warning, Email has been copied to your clipboard! This is just a friendly message to let you know that sensitive data has been copied.", "Atemoya", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex) {
                throw ex;
            }
        });
        public ICommand CopyPasswordCommand => new RelayGenericParamCommand<Gamertag>((gamertag) => {
            try {

                var unmanagedString = IntPtr.Zero;
                try {
                    unmanagedString = Marshal.SecureStringToBSTR(Encryption.DecryptToSecureString(gamertag.Password));
                    Clipboard.SetText(Marshal.PtrToStringBSTR(unmanagedString));
                }
                finally {
                    if (unmanagedString != IntPtr.Zero)
                        Marshal.ZeroFreeBSTR(unmanagedString);
                }

                MessageBox.Show("Warning, Password has been copied to your clipboard! This is just a friendly message to let you know that sensitive data has been copied.", "Atemoya", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex) {
                throw ex;
            }
        });

        public ICommand AddGamertagCommand => new RelayCommand(() => {
            try {

                if (new Add().ShowDialog() == true) {
                    MessageBox.Show("true");
                }
                else {
                    MessageBox.Show("false");
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        });
        public ICommand RefreshGamertagsCommand => new RelayCommand(() => {
            try {
                MessageBox.Show("TODO: Refresh Inputs", "Atemoya", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) {
                throw ex;
            }
        });
        public ICommand ViewMouseGamertagsCommand => new RelayCommand(() => {
            try {

                if (Tags?.Count >= 1) {
                    foreach (var tag in Tags)
                        tag.IsHidden = tag.Input != Helpers.InputType.Mouse;
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        });
        public ICommand ViewControllerGamertagsCommand => new RelayCommand(() => {
            try {

                if (Tags?.Count >= 1) {
                    foreach (var tag in Tags)
                        tag.IsHidden = tag.Input != Helpers.InputType.Controller;
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        });
        public ICommand ViewAllGamertagsCommand => new RelayCommand(() => {
            try {

                if (Tags?.Count >= 1) {
                    foreach (var tag in Tags)
                        tag.IsHidden = false;
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        });

        public ICommand SaveGamertagCommand => new RelayGenericParamCommand<Gamertag>((gamertag) => {
            try {
                if (MessageBox.Show($"Are you sure you want to save the selected Profile?\r\n\nGamertag: {gamertag.Name}", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes) {
                    gamertag.Email    = Encryption.EncryptString(gamertag.NewEmail);
                    gamertag.Password = Encryption.EncryptFromSecureString(gamertag.NewPassword);
                    //AppSettings.Instance.Save();
                }
                gamertag.Email = Encryption.EncryptString(gamertag.Email);
                gamertag.IsEditing = false;
            }
            catch (Exception ex) {
                throw ex;
            }
        });
        public ICommand EditGamertagCommand => new RelayGenericParamCommand<Gamertag>((gamertag) => {
            try {
                gamertag.Email = Encryption.DecryptString(gamertag.Email);
                gamertag.IsEditing = true;
            }
            catch (Exception ex) {
                throw ex;
            }
        });
        public ICommand RemoveGamertagCommand => new RelayGenericParamCommand<Gamertag>((gamertag) => {
            try {

                if (MessageBox.Show($"Are you sure you want to remove the selected Profile?\r\n\nGamertag: {gamertag.Name}", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    return;

            }
            catch (Exception ex) {
                throw ex;
            }
        });

        public ICommand ChangeEmailCommand => new RelayGenericParamCommand<EmailChangedCommandBehavior.EmailChangedCommandArgs<Gamertag>>((args) => {
            args.Source.NewEmail = args.Email;
        });
        public ICommand ChangePasswordCommand => new RelayGenericParamCommand<PasswordChangedCommandBehavior.PasswordChangedCommandArgs<Gamertag>>((args) => {
            args.Source.NewPassword = args.Password;
        });

        public void Initialize() {
            try {

                if (AppSettings.Instance.Gamertags.Count >= 1) {

                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }

}