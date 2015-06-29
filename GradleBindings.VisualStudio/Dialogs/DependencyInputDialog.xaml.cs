using System.Threading.Tasks;
using System.Windows;
using GradleBindings.Interfaces;

namespace EgorBo.GradleBindings_VisualStudio.Dialogs
{
    /// <summary>
    /// Interaction logic for DependencyInputDialog.xaml
    /// </summary>
    public partial class DependencyInputDialog : IDependencyInputDialog
    {
        public DependencyInputDialog()
        {
            InitializeComponent();
        }

        public DependencyInputDialog(string helpTopic)
            : base(helpTopic)
        {
            InitializeComponent();
        }

        public async Task<DependencyInputDialogResult> ShowAsync()
        {
            if (ShowModal() == true)
            {
                return new DependencyInputDialogResult(DependencyIdTextBox.Text, ProjectNameTextBox.Text, null);
            }
            return null;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DependencyIdTextBox.Text) &&
                !string.IsNullOrWhiteSpace(ProjectNameTextBox.Text))
            {
                DialogResult = true;
            }
        }
    }
}
