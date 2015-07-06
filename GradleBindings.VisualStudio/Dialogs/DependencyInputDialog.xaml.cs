using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using EgorBo.GradleBindings_VisualStudio.Helpers;
using GradleBindings;
using GradleBindings.Extensions;
using GradleBindings.Interfaces;

namespace EgorBo.GradleBindings_VisualStudio.Dialogs
{
    /// <summary>
    /// Interaction logic for DependencyInputDialog.xaml
    /// </summary>
    public partial class DependencyInputDialog : IDependencyInputDialog
    {
        private Func<DependencyInputDialogResult, Task> _taskExecuter = null;
        private List<RecommendedDpendencyInfo> _allSuggestions;

        public DependencyInputDialog()
        {
            InitializeComponent();
            Initialize();
            DataContext = this;
        }

        private async void Initialize()
        {
            _allSuggestions = (await new RecommendedDpendenciesService().GetAsync()).ToList();
        }

        public DependencyInputDialog(string helpTopic)
            : base(helpTopic)
        {
            InitializeComponent();
            Initialize();
            DataContext = this;
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
                OkButton.IsEnabled = false;
                DialogResult = true;
            }
        }

        private void DependencyIdTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //let's try to help user and generate a name for the assembly from Dependency String
            //com.afollestad:material-dialogs:0.7.6.0 --> Binding_MaterialDialogs

            var readableName = Gradle.GetReadableDependencyName(DependencyIdTextBox.Text);
            if (!string.IsNullOrEmpty(readableName))
            {
                ProjectNameTextBox.Text = "Binding_" + readableName;
            }

            UpdateSubmitVisibility();

            if (_allSuggestions != null)
            {
                var query = DependencyIdTextBox.Text.ToLower();
                var suggestions = _allSuggestions.Where(s =>
                    s.DependencyId.ToLower().Contains(query) ||
                    s.Name.ToLower().Contains(query)).ToList();
                SuggestionsBox.ItemsSource = suggestions;
                SuggestionsBox.SelectedIndex = -1;
                SuggestionsBox.Visibility = suggestions.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                SuggestionsBox.Visibility = Visibility.Collapsed;
            }
        }

        private void ProjectNameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SuggestionsBox.Visibility = Visibility.Visible;
            UpdateSubmitVisibility();
        }

        private void UpdateSubmitVisibility()
        {
            OkButton.IsEnabled = !string.IsNullOrWhiteSpace(DependencyIdTextBox.Text) && !string.IsNullOrWhiteSpace(ProjectNameTextBox.Text);
        }

        private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(((Hyperlink)sender).NavigateUri.ToString());
        }

        private void Expander_OnExpanded(object sender, RoutedEventArgs e)
        {
            this.Height = 410; //Auto doesn't work for DialogWindow
        }

        private void Expander_OnCollapsed(object sender, RoutedEventArgs e)
        {
            this.Height = 240; //Auto doesn't work for DialogWindow
        }

        private void OnSuggestionKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && SuggestionsBox.SelectedIndex <= 0)
            {
                SuggestionsBox.SelectedItem = null;
                DependencyIdTextBox.Focus();
            }

            if ((e.Key == Key.Enter || e.Key == Key.Tab) && SuggestionsBox.SelectedIndex >= 0)
            {
                var item = SuggestionsBox.SelectedItem as RecommendedDpendencyInfo;
                HandleSuggestionSelected(item);
            }
        }

        private void OnDependencyTextKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && DependencyIdTextBox.IsFocused && SetSuggestionsBoxFocus())
            {
                e.Handled = true;
            }
        }

        private bool SetSuggestionsBoxFocus()
        {
            if (!SuggestionsBox.ItemsSource.IsNullOrEmpty())
            {
                SuggestionsBox.SelectedIndex = 0;
                var itemContainerGenerator = SuggestionsBox.ItemContainerGenerator;
                if (itemContainerGenerator != null && itemContainerGenerator.Items.Any())
                {
                    var containerFromIndex = (ListBoxItem)itemContainerGenerator.ContainerFromIndex(0);
                    if (containerFromIndex != null)
                    {
                        containerFromIndex.Focus();
                        return true;
                    }
                }
            }
            return false;
        }

        private void DependencyInputDialog_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                SuggestionsBox.Visibility = Visibility.Collapsed;
            }
        }

        private void SuggestionsBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = e.OriginalSource as TextBlock;
            if (textBlock != null && textBlock.Name == "Url")
            {
                Process.Start(textBlock.Text);
                e.Handled = true;
                return;
            }

            var lbi = ItemsControl.ContainerFromElement(SuggestionsBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (lbi != null)
            {
                HandleSuggestionSelected(lbi.DataContext as RecommendedDpendencyInfo);
            }
        }

        private void HandleSuggestionSelected(RecommendedDpendencyInfo item)
        {
            if (item == null)
                return;

            DependencyIdTextBox.Text = item.DependencyId;
            SuggestionsBox.Visibility = Visibility.Collapsed;
            DependencyIdTextBox.SelectionStart = item.DependencyId.Length;
            DependencyIdTextBox.Focus();
        }
    }
}
