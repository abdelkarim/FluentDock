using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FluentDock.Internals;
using FluentDock.Primitives;

namespace FluentDock
{
    public class DocumentPaneItem : PaneItem
    {
        #region "Fields"

        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="DocumentPaneItem"/> class.
        /// </summary>
        static DocumentPaneItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DocumentPaneItem), new FrameworkPropertyMetadata(typeof(DocumentPaneItem)));
            CommandManager.RegisterClassCommandBinding(typeof(DocumentPaneItem), new CommandBinding(DocumentPaneControlCommands.PinTabCommand, OnPinExecuted, OnPinCanExecute));
            CommandManager.RegisterClassCommandBinding(typeof(DocumentPaneItem), new CommandBinding(DocumentPaneControlCommands.UnpinCommand, OnUnpinExecuted, OnUnpinCanExecute));
        }

        #endregion

        #region "Properties"

        #region CloseButtonStyle

        /// <summary>
        /// Identifies the <see cref="CloseButtonStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CloseButtonStyleProperty = DependencyProperty.Register(
            "CloseButtonStyle",
            typeof(Style),
            typeof(DocumentPaneItem),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the CloseButtonStyle property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public Style CloseButtonStyle
        {
            get { return (Style)GetValue(CloseButtonStyleProperty); }
            set { SetValue(CloseButtonStyleProperty, value); }
        }

        #endregion

        #region PinButtonStyle

        /// <summary>
        /// Identifies the <see cref="PinButtonStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PinButtonStyleProperty = DependencyProperty.Register(
            "PinButtonStyle",
            typeof(Style),
            typeof(DocumentPaneItem),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the PinButtonStyle property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public Style PinButtonStyle
        {
            get { return (Style)GetValue(PinButtonStyleProperty); }
            set { SetValue(PinButtonStyleProperty, value); }
        }

        #endregion

        #region IsLocked

        /// <summary>
        /// Identifies the <see cref="IsLocked"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsLockedProperty = DependencyProperty.Register(
            "IsLocked",
            typeof(bool),
            typeof(DocumentPaneItem),
            new FrameworkPropertyMetadata(BooleanBoxes.False));

        /// <summary>
        /// Gets or sets the IsLocked property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public bool IsLocked
        {
            get { return (bool)GetValue(IsLockedProperty); }
            set { SetValue(IsLockedProperty, BooleanBoxes.Box(value)); }
        }

        #endregion

        #region IsModified

        /// <summary>
        /// Identifies the <see cref="IsModified"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsModifiedProperty = DependencyProperty.Register(
            "IsModified",
            typeof(bool),
            typeof(DocumentPaneItem),
            new FrameworkPropertyMetadata(BooleanBoxes.False));

        /// <summary>
        /// Gets or sets the IsModified property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public bool IsModified
        {
            get { return (bool)GetValue(IsModifiedProperty); }
            set { SetValue(IsModifiedProperty, BooleanBoxes.Box(value)); }
        }

        #endregion

        #endregion

        #region "Static Methods"

        private static void OnPinExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var tabItem = (DocumentPaneItem)sender;
            tabItem.IsPinned = true;
        }

        private static void OnUnpinExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var tabItem = (DocumentPaneItem)sender;
            tabItem.IsPinned = false;
        }

        private static void OnUnpinCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var tabItem = (DocumentPaneItem)sender;
            e.CanExecute = tabItem.IsPinned;
        }

        private static void OnPinCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var tabItem = (DocumentPaneItem)sender;
            e.CanExecute = !tabItem.IsPinned;
        }

        #endregion

        #region "Methods"

        protected override void OnPinned()
        {
            base.OnPinned();

            var panel = VisualTreeHelper.GetParent(this) as DocumentsPanel;
            if (panel != null)
            {
                panel.PinChild(this);
            }
        }

        protected override void OnUnpinned()
        {
            base.OnUnpinned();

            var panel = VisualTreeHelper.GetParent(this) as PinnedDocumentsPanel;
            if (panel != null)
            {
                panel.UnpinChild(this);
            }
        }

        #endregion
    }
}