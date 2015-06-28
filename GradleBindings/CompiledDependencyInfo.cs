using System.Collections.Generic;
using System.Linq;

namespace GradleBindings
{
    public class CompiledDependencyInfo
    {
        /// <summary>
        /// Name of the reference 
        /// e.g. 'com.afollestad:material-dialogs:0.7.6.0'
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The name of the reference adopted to be used as an Assembly name
        /// </summary>
        public string ShortName { get; private set; }

        public IEnumerable<DependencyFile> Files { get; private set; }

        public CompiledDependencyInfo(string name, string shortName, IEnumerable<DependencyFile> files)
        {
            Name = name;
            ShortName = shortName;
            Files = files.ToList();
        }
    }
}