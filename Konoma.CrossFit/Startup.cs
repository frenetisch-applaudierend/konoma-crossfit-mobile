using System.Threading.Tasks;
using Konoma.CrossFit.Util;

namespace Konoma.CrossFit
{
    // ReSharper disable once TypeParameterCanBeVariant

    public interface IStartup<TMainNavigation>
    {
        Task StartApplicationAsync(TMainNavigation navigation);
    }
}
