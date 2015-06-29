using GradleBindings.Interfaces;

namespace EgorBo.GradleBindings_VisualStudio
{
    internal class SettingsAdapter : ISettings
    {
        public string AndroidSdk
        {
            get { return Settings.Default.AndroidSdkHome; }
            set { Settings.Default.AndroidSdkHome = value; }
        }
    }
}