using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using FluentDock.Primitives;

namespace FluentDock
{
    [
        TemplatePart(Name = "PART_PinnedDocumentsPanel", Type = typeof(PinnedDocumentsPanel)),
        StyleTypedProperty(Property = "NextButtonStyle", StyleTargetType = typeof(RepeatButton)),
        StyleTypedProperty(Property = "PreviousButtonStyle", StyleTargetType = typeof(RepeatButton)),
        StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(DocumentPaneControl))
    ]
    public class DocumentPaneControl : PaneControl
    {
        #region "Fields"

        

        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="DocumentPaneControl"/> class.
        /// </summary>
        static DocumentPaneControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DocumentPaneControl), new FrameworkPropertyMetadata(typeof(DocumentPaneControl)));
            CommandManager.RegisterClassCommandBinding(typeof(DocumentPaneControl), new CommandBinding(DocumentPaneControlCommands.CloseTabCommand, OnClosePaneCommandExecuted));
        }

        /// <summary>
        /// Initializes instance members of the <see cref="DocumentPaneControl"/> class.
        /// </summary>
        public DocumentPaneControl()
        {
            
        }

        #endregion

        #region "Properties"

        #region NextButtonStyle

        /// <summary>
        /// Identifies the <see cref="NextButtonStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NextButtonStyleProperty = DependencyProperty.Register(
            "NextButtonStyle",
            typeof(Style),
            typeof(DocumentPaneControl),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the NextButtonStyle property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public Style NextButtonStyle
        {
            get { return (Style)GetValue(NextButtonStyleProperty); }
            set { SetValue(NextButtonStyleProperty, value); }
        }

        #endregion

        #region PreviousButtonStyle

        /// <summary>
        /// Identifies the <see cref="PreviousButtonStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PreviousButtonStyleProperty = DependencyProperty.Register(
            "PreviousButtonStyle",
            typeof(Style),
            typeof(DocumentPaneControl),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the PreviousButtonStyle property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public Style PreviousButtonStyle
        {
            get { return (Style)GetValue(PreviousButtonStyleProperty); }
            set { SetValue(PreviousButtonStyleProperty, value); }
        }

        #endregion

        #endregion

        #region "Static Methods"

        private static void OnClosePaneCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var documentsGroup = (DocumentPaneControl)sender;
            var item = e.OriginalSource as DocumentPaneItem;
            if (item != null)
            {
                
            }
        }

        private static void OnPinDocumentPaneCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var paneControl = (DocumentPaneControl) sender;
            var paneItem = e.OriginalSource as DocumentPaneItem;
            if (paneItem != null)
            {
                paneItem.IsPinned = !paneItem.IsPinned;
            }
        }

        #endregion

        #region "Methods"

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DocumentPaneItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DocumentPaneItem;
        }

        #endregion
    }
}