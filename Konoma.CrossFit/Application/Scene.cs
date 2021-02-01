using System;

namespace Konoma.CrossFit
{
    public abstract class Scene : ViewModel
    {
        internal static IServiceProvider ServiceProvider { get; set; } = default!;

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
}
