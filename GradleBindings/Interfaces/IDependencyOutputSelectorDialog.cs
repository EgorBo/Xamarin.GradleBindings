using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradleBindings.Interfaces
{
    public interface IDependencyOutputSelectorDialog
    {
        Task<IEnumerable<DependencyFile>> FilterDependenciesAsync(IEnumerable<DependencyFile> files);
    }
}