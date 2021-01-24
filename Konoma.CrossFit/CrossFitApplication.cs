using System;

namespace Konoma.CrossFit
{
    public abstract class CrossFitApplication
    {
        protected internal CrossFitApplication() { }
    }

    public abstract class CrossFitApplication<TStartup> : CrossFitApplication
        where TStartup : IStartup
    {
    }
}
