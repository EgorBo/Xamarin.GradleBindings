using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradleBindings.Interfaces
{
    public interface IBindingProjectGenerator
    {
        Task GenerateAsync(IEnumerable<DependencyFile> dependencies);
    }
}
