using System.Windows;
using System.Windows.Media;

namespace FluentDock.Internals
{
    internal static class VisualTreeHelpers
    {
        internal static T ParentOfType<T>(this DependencyObject target) where T : FrameworkElement
        {
            if (target == null)
                return null;

            var parent = VisualTreeHelper.GetParent(target);
            return parent as T ?? ParentOfType<T>(parent);
        }
    }
}
