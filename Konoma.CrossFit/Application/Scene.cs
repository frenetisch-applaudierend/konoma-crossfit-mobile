using System;

namespace Konoma.CrossFit
{
    public abstract class Scene : ViewModel
    {
        internal static IServiceProvider ServiceProvider { get; set; } = default!;

        internal virtual void SetArguments(object? arguments)
        {
            if (!(arguments is null))
                throw new ArgumentException("Scene does not support arguments");
        }

        internal static bool IsSceneType(Type? type)
        {
            while (type != null && type != typeof(object))
            {
                if (type == typeof(Scene))
                    return true;

                type = type.BaseType;
            }

            return false;
        }

        public static TScene Create<TScene>() where TScene : Scene =>
            ServiceProvider.GetRequiredService<TScene>();
    }

    public abstract class Scene<TArgs> : Scene
        where TArgs : TransferData
    {
        internal override void SetArguments(object? arguments)
        {
            if (!(arguments is TArgs args))
                throw new ArgumentException($"Scene must be initialized with arguments of type {typeof(TArgs)}");

            ApplyArguments(args);
        }

        protected abstract void ApplyArguments(TArgs args);
    }
}
