using System;
using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    public interface INavigation<TScene>
        where TScene : Scene
    {
        Task GoAsync();
    }

    public static class Navigation
    {
        public static INavigation<TScene> For<TScene>(Action navigator) where TScene : Scene =>
            For<TScene>(
                () =>
                {
                    navigator();
                    return Task.CompletedTask;
                });

        public static INavigation<TScene> For<TScene>(Func<Task> navigator) where TScene : Scene =>
            new DelegateNavigation<TScene>(navigator);

        class DelegateNavigation<TScene> : INavigation<TScene> where TScene : Scene
        {
            public DelegateNavigation(Func<Task> navigator)
            {
                _navigator = navigator;
            }

            private readonly Func<Task> _navigator;

            public Task GoAsync() => _navigator();
        }
    }
}
