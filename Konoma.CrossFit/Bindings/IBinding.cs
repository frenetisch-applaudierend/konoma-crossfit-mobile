using System;

namespace Konoma.CrossFit
{
    public interface IBinding : IDisposable
    {
        void SetupAfterRegistration();

        Action<IBinding>? OnDisposed { get; set; }
    }
}
