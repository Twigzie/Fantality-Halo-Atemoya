using Atemoya.Classes.Entities;
using Microsoft.Xaml.Behaviors;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atemoya.Classes.Commands.Behaviors {

    public class PasswordChangedCommandBehavior : Behavior<PasswordBox> {

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(PasswordChangedCommandBehavior));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(Gamertag), typeof(PasswordChangedCommandBehavior));

        public ICommand Command {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public Gamertag CommandParameter {
            get => (Gamertag)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.PasswordChanged += OnPasswordChanged;
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e) {
            var args = new PasswordChangedCommandArgs<Gamertag> {
                Password = AssociatedObject.SecurePassword,
                Source = CommandParameter
            };

            if (Command?.CanExecute(args) == true)
                Command.Execute(args);
        }

        public class PasswordChangedCommandArgs<T> {
            public SecureString Password {
                get; set;
            }
            public Gamertag Source {
                get; set;
            }
        }
    }

}