using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using GradleBindings;
using GradleBindings.Interfaces;

namespace EgorBo.GradleBindings_VisualStudio.Dialogs
{
    /// <summary>
    /// Interaction logic for DependencyOutputSelectorDialog.xaml
    /// </summary>
    public partial class DependencyOutputSelectorDialog : IDependencyOutputSelectorDialog
    {
        public DependencyOutputSelectorDialog()
        {
            InitializeComponent();
        }

        public DependencyOutputSelectorDialog(string helpTopic)
            : base(helpTopic)
        {
            InitializeComponent();
        }

        public async Task<IEnumerable<DependencyFile>> FilterDependenciesAsync(IEnumerable<DependencyFile> files)
        {
            var dependencyInfoViewModels = files.Select(f => new DependencyInfoViewModel(!f.IsTransitive, Path.GetFileName(f.File), f.File, f.IsTransitive)).ToList();
            DataGrid.ItemsSource = dependencyInfoViewModels;
            if (ShowModal() == true)
            {
                return dependencyInfoViewModels.Where(d => d.Included).Select(d => new DependencyFile(d.Path, d.IsDependency));
            }
            return null;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }

    public class DependencyInfoViewModel : INotifyPropertyChanged
    {
        private bool _included;
        private string _name;
        private string _path;
        private bool _isDependency;

        public bool Included
        {
            get { return _included; }
            set { SetProperty(ref _included, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }

        public bool IsDependency
        {
            get { return _isDependency; }
            set { SetProperty(ref _isDependency, value); }
        }

        public DependencyInfoViewModel(bool included, string name, string path, bool isDependency)
        {
            Included = included;
            Name = name;
            Path = path;
            IsDependency = isDependency;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T backingField, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (Equals(backingField, newValue))
                return;
            backingField = newValue;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
