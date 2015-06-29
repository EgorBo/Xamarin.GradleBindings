using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradleBindings.Interfaces
{
    public interface IBindingProjectGenerator
    {
        Task GenerateAsync(string sourceProjectName, string bindingProjectName, IEnumerable<DependencyFile> dependencies);
    }
}
