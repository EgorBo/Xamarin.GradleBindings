using System.Collections.Generic;
using System.Linq;

namespace GradleBindings
{
    public class DependencyFile
    {
        public string File { get; private set; }

        public bool IsDependency { get; private set; }

        public DependencyFile(string file, bool isDependency)
        {
            File = file;
            IsDependency = isDependency;
        }
    }
}