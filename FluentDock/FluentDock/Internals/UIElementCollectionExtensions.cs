using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace FluentDock.Internals
{
    internal static class UIElementCollectionExtensions
    {
        private static Type _collectionType;

        public static Type UIElementCollectionType
        {
            get { return _collectionType ?? (_collectionType = typeof (UIElementCollection)); }
        }


        internal static void RemoveNoVerify(this UIElementCollection uiElementCollection, UIElement element)
        {
            UIElementCollectionType.InvokeMember("RemoveNoVerify",
                BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic,
                null,
                uiElementCollection,
                new object[] {element});
        }
    }
}
