using System.Threading.Tasks;

namespace GradleBindings.Interfaces
{
    public interface IDependencyInputDialog
    {
        Task<DependencyInputDialogResult> ShowAsync(string repositores);
    }

    public class DependencyInputDialogResult
    {
        public string DependencyId { get; set; }

        public string AssemblyName { get; set; }

        public string DependencyRepository { get; set; }

        public DependencyInputDialogResult(string dependencyId, string assemblyName, string dependencyRepository)
        {
            DependencyId = dependencyId;
            AssemblyName = assemblyName;
            DependencyRepository = dependencyRepository;
        }
    }
}