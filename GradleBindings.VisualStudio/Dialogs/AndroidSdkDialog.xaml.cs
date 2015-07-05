using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using GradleBindings;
using GradleBindings.Interfaces;

namespace EgorBo.GradleBindings_VisualStudio.Dialogs
{
    /// <summary>
    /// Interaction logic for AndroidSdkDialog.xaml
    /// </summary>
    public partial class AndroidSdkDialog : IAndroidSdkDialog
    {

        public AndroidSdkDialog()
        {
            InitializeComponent();
        }

        public AndroidSdkDialog(string helpTopic)
            : base(helpTopic)
        {
            InitializeComponent();
        }

        public async Task<string> AskAsync()
        {
            //try guess it
            var userDir = Environment.GetEnvironmentVariable("USERPROFILE");
            if (!string.IsNullOrEmpty(userDir))
            {
                var androidSdkHome = Path.Combine(userDir, @"AppData\Local\Android\sdk");
                string expectedDir;
                if (Gradle.HasLocalRepositories(androidSdkHome, out expectedDir))
                {
                    PathTextBox.Text = expectedDir;
                }
            }

            if (ShowModal() == true)
            {
                return PathTextBox.Text;
            }
            return null;
        }

        private void BrowseButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            var dialogResult = dlg.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                PathTextBox.Text = dlg.SelectedPath;
            }
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            string expectedDir;
            if (!Gradle.HasLocalRepositories(PathTextBox.Text, out expectedDir))
            {
                ErrorTextBlock.Text = string.Format("Error: {0} was not found!", expectedDir);
                ErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }
            OkButton.IsEnabled = false;
            DialogResult = true;
        }

        private void PathTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            OkButton.IsEnabled = !string.IsNullOrWhiteSpace(PathTextBox.Text);
            ErrorTextBlock.Visibility = Visibility.Hidden;
        }
    }
}
