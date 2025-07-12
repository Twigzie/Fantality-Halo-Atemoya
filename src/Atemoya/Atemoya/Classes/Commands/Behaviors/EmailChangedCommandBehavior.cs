using Atemoya.Classes.Entities;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atemoya.Classes.Commands.Behaviors {

    public class EmailChangedCommandBehavior : Behavior<TextBox> {

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(EmailChangedCommandBehavior));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(Gamertag), typeof(EmailChangedCommandBehavior));

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
            AssociatedObject.TextChanged += OnTextChanged;
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, RoutedEventArgs e) {
            var args = new EmailChangedCommandArgs<Gamertag> {
                Email = AssociatedObject.Text,
                Source = CommandParameter
            };

            if (Command?.CanExecute(args) == true)
                Command.Execute(args);
        }

        public class EmailChangedCommandArgs<T> {
            public string Email {
                get; set;
            }
            public Gamertag Source {
                get; set;
            }
        }

    }

}