using GradleBindings.Interfaces;

namespace EgorBo.GradleBindings_VisualStudio.Dialogs
{
    /// <summary>
    /// Interaction logic for ErrorDialog.xaml
    /// </summary>
    public partial class ErrorDialog : IErrorDialog
    {
        public ErrorDialog()
        {
            InitializeComponent();
        }

        public ErrorDialog(string helpTopic)
            : base(helpTopic)
        {
            InitializeComponent();
        }

        public void ShowError(string error)
        {
            LogTextBlock.Text = error;
            ShowModal();
        }
    }
}
