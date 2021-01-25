using System;
using Microsoft.Extensions.DependencyInjection;

namespace Konoma.CrossFit
{
    public interface ISceneRegistration
    {
        void RegisterScene<TScene>() where TScene : Scene;

        void RegisterScene(Type sceneType);
    }

    internal class SceneRegistration : ISceneRegistration
    {
        public SceneRegistration(IServiceCollection services)
        {
            _services = services;
        }

        private readonly IServiceCollection _services;

        public void RegisterScene<TScene>() where TScene : Scene
        {
            _services.AddTransient<TScene>();
        }

        public void RegisterScene(Type sceneType)
        {
            if (!Scene.IsSceneType(sceneType))
                throw new ArgumentException($"Must be a subclass of Scene: {sceneType}", nameof(sceneType));

            _services.AddTransient(sceneType);
        }
    }
}
