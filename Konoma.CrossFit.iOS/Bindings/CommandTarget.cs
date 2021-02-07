using System;

namespace Konoma.CrossFit
{
    public interface ICommandTarget : IDisposable
    {
        void SetCommandCanExecute(bool canExecute);

        Action? OnExecute { get; set; }
    }
}
