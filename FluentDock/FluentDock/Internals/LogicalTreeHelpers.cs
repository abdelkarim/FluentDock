using System.Reflection;
using System.Windows;

namespace FluentDock.Internals
{
    internal class LogicalTreeHelpers
    {
        internal static void RemoveLogicalChild(DependencyObject parent, object child)
        {
            var targetType = typeof (LogicalTreeHelper);
            targetType.InvokeMember("RemoveLogicalChild",
                BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod,
                null,
                null,
                new object[] {parent, child});
        }

        internal static void AddLogicalChild(FrameworkElement parentFE, object child)
        {
            var targetType = typeof(LogicalTreeHelper);
            targetType.InvokeMember("AddLogicalChild",
                BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod,
                null,
                null,
                new object[] { parentFE, null /* parentFCE */, child });
        }
    }
}
