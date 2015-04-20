using System.Windows.Input;

namespace FluentDock
{
    /// <summary>
    /// 
    /// </summary>
    public static class PaneCommands
    {
        private static RoutedUICommand _floatCommand;
        private static RoutedUICommand _dockCommand;
        private static RoutedUICommand _dockAsDocumentCommand;
        private static RoutedUICommand _autoHideCommand;
        private static RoutedUICommand _hideCommand;

        public static RoutedUICommand FloatCommand
        {
            get
            {
                if (_floatCommand == null)
                    _floatCommand = new RoutedUICommand("Float", "Float", typeof(PaneCommands));

                return _floatCommand;
            }
        }

        public static RoutedUICommand DockCommand
        {
            get
            {
                if (_dockCommand == null)
                    _dockCommand = new RoutedUICommand("Dock", "Dock", typeof(PaneCommands));

                return _dockCommand;
            }
        }

        public static RoutedUICommand DockAsDocumentCommand
        {
            get
            {
                if (_dockAsDocumentCommand == null)
                    _dockAsDocumentCommand = new RoutedUICommand("Dock as Tabbed Document", "DockAsDocument", typeof(PaneCommands));

                return _dockAsDocumentCommand;
            }
        }

        public static RoutedUICommand AutoHideCommand
        {
            get
            {
                if (_autoHideCommand == null)
                    _autoHideCommand = new RoutedUICommand("Auto Hide", "AutoHide", typeof(PaneCommands));

                return _autoHideCommand;
            }
        }

        public static RoutedUICommand HideCommand
        {
            get
            {
                if (_hideCommand == null)
                    _hideCommand = new RoutedUICommand("Hide", "Hide", typeof(PaneCommands));

                return _hideCommand;
            }
        }
    }
}