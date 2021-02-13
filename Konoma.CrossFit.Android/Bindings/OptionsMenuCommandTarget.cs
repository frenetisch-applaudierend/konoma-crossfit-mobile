using System;
using Android.App;
using Android.Views;

namespace Konoma.CrossFit
{
    public class OptionsMenuCommandTarget : ICommandTarget, IMenuItemCommandHandler
    {
        private Activity? _activity;
        private bool _canExecute;
        private readonly int _itemId;

        public OptionsMenuCommandTarget(Activity activity, int itemId)
        {
            _activity = activity;
            _itemId = itemId;
        }

        public void Dispose()
        {
            _activity = null;
        }

        public void SetCommandCanExecute(bool canExecute)
        {
            _canExecute = canExecute;
            _activity?.InvalidateOptionsMenu();
        }

        public Action? OnExecute { get; set; }

        void IMenuItemCommandHandler.PrepareOptionsMenu(IMenu menu)
        {
            if (menu.FindItem(_itemId) is { } menuItem)
                menuItem.SetEnabled(_canExecute);
        }

        bool IMenuItemCommandHandler.HandleItemSelected(IMenuItem menuItem)
        {
            if (menuItem.ItemId != _itemId || OnExecute is null)
                return false;

            OnExecute.Invoke();
            return true;
        }
    }

    public static class OptionsMenuCommandTargetExtensions
    {
        public static void ToOptionsMenu(this CommandBindingBuilder builder, Activity activity, int itemId)
            => builder.To(new OptionsMenuCommandTarget(activity, itemId));
    }
}
