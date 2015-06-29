using System.Threading.Tasks;

namespace GradleBindings.Interfaces
{
    public interface IAndroidSdkDialog
    {
        Task<string> AskAsync();
    }
}