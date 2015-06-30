using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using GradleBindings.Interfaces;

namespace EgorBo.GradleBindings_VisualStudio.Dialogs
{
    /// <summary>
    /// Interaction logic for DependencyInputDialog.xaml
    /// </summary>
    public partial class DependencyInputDialog : IDependencyInputDialog
    {
        private Func<DependencyInputDialogResult, Task> _taskExecuter = null;

        public DependencyInputDialog()
        {
            InitializeComponent();
        }

        public DependencyInputDialog(string helpTopic)
            : base(helpTopic)
        {
            InitializeComponent();
        }

        public async Task<bool> ShowAsync(string defualtRepositories, Func<DependencyInputDialogResult, Task> taskExecuter)
        {
            _taskExecuter = taskExecuter;
            RepositoriesTextBox.Text = defualtRepositories;
            return ShowModal() == true;
        }

        private async void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DependencyIdTextBox.Text) &&
                !string.IsNullOrWhiteSpace(ProjectNameTextBox.Text))
            {
                BusyIndicator.IsBusy = true;
                await _taskExecuter(new DependencyInputDialogResult(DependencyIdTextBox.Text, ProjectNameTextBox.Text, RepositoriesTextBox.Text));
                BusyIndicator.IsBusy = false;
                DialogResult = true;
            }
        }

        private void DependencyIdTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //let's try to help user and generate a name for the assembly from Dependency String
            //com.afollestad:material-dialogs:0.7.6.0 --> Binding_MaterialDialogs
            var text = DependencyIdTextBox.Text.Trim(' ', '\t');

            if (text.StartsWith("compile '"))
            {
                text = text.Remove(0, "compile '".Length);
            }
            text = text.Trim('\'', '\"', ' ');
            if (text != DependencyIdTextBox.Text)
            {
                DependencyIdTextBox.Text = text;
                return;
            }
            
            var parts = text.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 3)
            {
                var rsb = new StringBuilder();
                var name = parts[1];
                bool previousWasnotLetter = true;
                for (int i = 0; i < name.Length; i++)
                {
                    if (char.IsLetter(name[i]))
                    {
                        rsb.Append(previousWasnotLetter ? char.ToUpper(name[i]) : name[i]);
                        previousWasnotLetter = false;
                        continue;
                    }
                    if (char.IsDigit(name[i]) && i > 0)
                    {
                        rsb.Append(name[i]);
                    }
                    previousWasnotLetter = true;
                }

                if (rsb.Length > 1)
                {
                    ProjectNameTextBox.Text = "Binding_" + rsb.ToString();
                }
            }
            UpdateSubmitVisibility();
        }

        private void ProjectNameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSubmitVisibility();
        }

        private void UpdateSubmitVisibility()
        {
            OkButton.IsEnabled = !string.IsNullOrWhiteSpace(DependencyIdTextBox.Text) && !string.IsNullOrWhiteSpace(ProjectNameTextBox.Text);
        }

        private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(((Hyperlink)sender).NavigateUri.ToString());
        }

        private void Expander_OnExpanded(object sender, RoutedEventArgs e)
        {
            this.Height = 410; //Auto doesn't work for DialogWindow
        }

        private void Expander_OnCollapsed(object sender, RoutedEventArgs e)
        {
            this.Height = 240; //Auto doesn't work for DialogWindow
        }
    }
}
