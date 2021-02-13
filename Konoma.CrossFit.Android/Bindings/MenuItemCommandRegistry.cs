using System.Collections.Generic;
using System.Diagnostics;
using Android.Views;

namespace Konoma.CrossFit
{
    public class MenuItemCommandRegistry
    {
        private readonly HashSet<IMenuItemCommandHandler> _handlers = new HashSet<IMenuItemCommandHandler>();

        public void RegisterHandler(IMenuItemCommandHandler handler)
        {
            Debug.Assert(!_handlers.Contains(handler), "Tried to add the same handler multiple times");

            _handlers.Add(handler);
        }

        public void ClearHandlers()
        {
            _handlers.Clear();
        }

        public void PrepareOptionsMenu(IMenu menu)
        {
            foreach (var handler in _handlers)
                handler.PrepareOptionsMenu(menu);
        }

        public bool HandleItemSelected(IMenuItem menuItem)
        {
            foreach (var handler in _handlers)
            {
                if (handler.HandleItemSelected(menuItem))
                    return true;
            }

            return false;
        }
    }

    public interface IMenuItemCommandHandler
    {
        void PrepareOptionsMenu(IMenu menu);

        bool HandleItemSelected(IMenuItem menuItem);
    }
}
