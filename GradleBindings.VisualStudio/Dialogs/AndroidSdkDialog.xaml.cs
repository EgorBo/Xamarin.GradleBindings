using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
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
            if (!string.IsNullOrWhiteSpace(PathTextBox.Text)
                && Directory.Exists(PathTextBox.Text))
            {
                DialogResult = true;
            }
        }
    }
}
