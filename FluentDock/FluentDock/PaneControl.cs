using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FluentDock.Internals;

namespace FluentDock
{
    public class PaneControl : TabControl
    {
        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="PaneControl"/> class.
        /// </summary>
        static PaneControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PaneControl), new FrameworkPropertyMetadata(typeof(PaneControl)));
            TabStripPlacementProperty.OverrideMetadata(typeof(PaneControl), new FrameworkPropertyMetadata(Dock.Bottom));

            InitPanel();
        }

        #endregion

        #region "Properties"

        #region HasSinglePane

        /// <summary>
        /// HasSinglePane Read-Only Dependency Property
        /// </summary>
        private static readonly DependencyPropertyKey HasSinglePanePropertyKey = DependencyProperty.RegisterReadOnly(
            "HasSinglePane",
            typeof(bool),
            typeof(PaneControl),
            new FrameworkPropertyMetadata(BooleanBoxes.False));

        public static readonly DependencyProperty HasSinglePaneProperty = HasSinglePanePropertyKey.DependencyProperty;

        /// <summary>
        /// 
        /// </summary>
        public bool HasSinglePane
        {
            get { return (bool)GetValue(HasSinglePaneProperty); }
            internal set { SetValue(HasSinglePanePropertyKey, BooleanBoxes.Box(value)); }
        }

        #endregion

        #region SelectedPaneTitle

        /// <summary>
        /// Identifies the <see cref="SelectedPaneTitle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedPaneTitleProperty = DependencyProperty.Register(
            "SelectedPaneTitle",
            typeof(object),
            typeof(PaneControl),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the SelectedPaneTitle property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public object SelectedPaneTitle
        {
            get { return (object)GetValue(SelectedPaneTitleProperty); }
            set { SetValue(SelectedPaneTitleProperty, value); }
        }

        #endregion

        #endregion

        #region "Static Methods"

        private static void InitPanel()
        {
            /*var newPane = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(StackPanel)));
            ItemsPanelProperty.OverrideMetadata(typeof(PaneControl), new FrameworkPropertyMetadata(newPane));*/
        }

        #endregion

        #region "Methods"

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new PaneItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is PaneItem;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            InvalidateSelectedContent();
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            InvalidateSelectedContent();
        }

        private void InvalidateSelectedContent()
        {
            var selectedPane = GetSelectedPane();
            SelectedPaneTitle = selectedPane == null ? null : selectedPane.Title;
        }

        private PaneItem GetSelectedPane()
        {
            var selectedItem = SelectedItem;
            if (selectedItem != null)
            {
                var selectedPane = selectedItem as PaneItem;
                if (selectedPane != null)
                    return selectedPane;

                // find the container
                selectedPane = ItemContainerGenerator.ContainerFromItem(selectedItem) as PaneItem;
                return selectedPane;
            }

            return null;
        }

        #endregion
    }
}