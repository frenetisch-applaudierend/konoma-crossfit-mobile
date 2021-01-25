using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    // ReSharper disable once TypeParameterCanBeVariant

    public interface IStartup<TMainNavigation>
    {
        Task StartApplicationAsync(TMainNavigation navigation);
    }
}
