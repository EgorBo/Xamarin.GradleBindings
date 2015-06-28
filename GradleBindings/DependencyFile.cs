namespace GradleBindings
{
    public class DependencyFile
    {
        public string Path { get; set; }

        public DependencyFileType Type { get; set; }

        public DependencyFile(string path, DependencyFileType type)
        {
            Path = path;
            Type = type;
        }
    }
}