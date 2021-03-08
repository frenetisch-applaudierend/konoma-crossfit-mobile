using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    public interface INavigation
    {
        Task NavigateAsync(bool animated);
    }

    // ReSharper disable once UnusedTypeParameter
    public interface INavigation<TScene>
        where TScene : Scene
    {
        Task NavigateAsync(bool animated);
    }

    // ReSharper disable once TypeParameterCanBeVariant
    // ReSharper disable once UnusedTypeParameter
    public interface INavigation<TScene, TArgs>
        where TScene : Scene<TArgs>
        where TArgs : TransferData
    {
        Task NavigateAsync(TArgs arguments, bool animated);
    }
}
