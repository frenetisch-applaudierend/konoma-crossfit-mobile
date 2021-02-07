using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    public interface INavigation<TScene>
        where TScene : Scene
    {
        Task NavigateAsync(bool animated);
    }
}
