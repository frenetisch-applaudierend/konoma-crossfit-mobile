using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    public interface IStartup
    {
        void Configure(IStartupCompletion completion);
    }

    public interface IStartupCompletion
    {
        void StartScene<TScene>() where TScene : Scene;
    }

    public class Startup<TScene> : IStartup
        where TScene : Scene
    {
        public void Configure(IStartupCompletion completion)
        {
            completion.StartScene<TScene>();
        }
    }
}
