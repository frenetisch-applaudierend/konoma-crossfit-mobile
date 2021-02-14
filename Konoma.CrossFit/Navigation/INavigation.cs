using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    // ReSharper disable once UnusedTypeParameter
    public interface INavigation<TScene>
        where TScene : Scene
    {
        Task NavigateAsync(bool animated);
    }
}
