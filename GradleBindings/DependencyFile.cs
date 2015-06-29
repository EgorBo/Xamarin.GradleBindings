using System.Collections.Generic;
using System.Linq;

namespace GradleBindings
{
    public class DependencyFile
    {
        public string File { get; private set; }

        public bool IsTransitive { get; private set; }

        public DependencyFile(string file, bool isTransitive)
        {
            File = file;
            IsTransitive = isTransitive;
        }
    }
}