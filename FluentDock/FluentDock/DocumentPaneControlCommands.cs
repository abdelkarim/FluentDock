using System;
using System.Windows.Input;

namespace FluentDock
{
    public class DocumentPaneControlCommands
    {
        #region "Fields"

        private static Type _OwnerType = typeof(DocumentPaneControlCommands);
        private static ICommand _newTabCommand;
        private static ICommand _pinTabCommand;
        private static ICommand _unpinCommand;
        private static ICommand _closeTabCommand;
        private static ICommand _CloseAllTabsCommand;
        private static ICommand _CloseAllExceptSelectedCommand;
        private static ICommand _CloseRightTabsCommands;

        #endregion

        #region "Properties"

        public static ICommand NewTabCommand
        {
            get
            {
                if (_newTabCommand == null)
                    _newTabCommand = new RoutedUICommand("New tab",
                        "NewTab",
                        _OwnerType);
                return _newTabCommand;
            }
        }

        public static ICommand PinTabCommand
        {
            get
            {
                if (_pinTabCommand == null)
                    _pinTabCommand = new RoutedUICommand("Pin tab",
                        "PinTab",
                        _OwnerType);
                return _pinTabCommand;
            }
        }

        public static ICommand UnpinCommand
        {
            get
            {
                if (_unpinCommand == null)
                    _unpinCommand = new RoutedUICommand("Unpin tab",
                        "UnpinTab",
                        _OwnerType);

                return _unpinCommand;
            }
        }

        public static ICommand CloseTabCommand
        {
            get
            {
                if (_closeTabCommand == null)
                    _closeTabCommand = new RoutedUICommand("Close Tab",
                        "CloseTab",
                        _OwnerType);
                return _closeTabCommand;
            }
        }

        public static ICommand CloseAllTabsCommand
        {
            get
            {
                if (_CloseAllTabsCommand == null)
                    _CloseAllTabsCommand = new RoutedUICommand("Close All Tabs",
                        "CloseAllTabs",
                        _OwnerType);
                return _CloseAllTabsCommand;
            }
        }

        public static ICommand CloseAllExceptSelectedCommand
        {
            get
            {
                if (_CloseAllExceptSelectedCommand == null)
                    _CloseAllExceptSelectedCommand = new RoutedUICommand("Close All Except this",
                        "CloseAllExceptSelected",
                        _OwnerType);
                return _CloseAllExceptSelectedCommand;
            }
        }

        public static ICommand CloseRightTabsCommand
        {
            get
            {
                if (_CloseRightTabsCommands == null)
                {
                    _CloseRightTabsCommands = new RoutedUICommand("Close tabs to the right",
                        "CloseRightTabs",
                        _OwnerType);
                }
                return _CloseRightTabsCommands;
            }
        }

        #endregion
    }
}
