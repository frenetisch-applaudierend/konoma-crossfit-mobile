using System.Collections.Generic;
using System.Diagnostics;

namespace Konoma.CrossFit
{
    public class BindingRegistry
    {
        private readonly HashSet<IBinding> _bindings = new HashSet<IBinding>();

        public void RegisterBinding(IBinding binding)
        {
            Debug.Assert(!_bindings.Contains(binding), "Tried to add the same binding multiple times");

            binding.OnDisposed = HandleBindingDisposed;
            binding.SetupAfterRegistration();

            _bindings.Add(binding);
        }

        public void ClearAndDisposeBindings()
        {
            foreach (var binding in _bindings)
            {
                binding.OnDisposed = null;
                binding.Dispose();
            }

            _bindings.Clear();
        }

        private void HandleBindingDisposed(IBinding binding)
        {
            var wasRemoved = _bindings.Remove(binding);

            Debug.Assert(wasRemoved, "Tried to remove binding that was not part of this binder");
        }
    }
}
