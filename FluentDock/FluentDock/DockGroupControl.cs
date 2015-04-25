using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using FluentDock.Internals;
using FluentDock.Primitives;

namespace FluentDock
{
    public class DockGroupControl : ItemsControl
    {
        #region "Fields"

        private IDraggingStrategy _defferedDraggingStrategy;
        private IDraggingStrategy _continuousDraggingStrategy;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="DockGroupControl"/> class.
        /// </summary>
        static DockGroupControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockGroupControl), new FrameworkPropertyMetadata(typeof(DockGroupControl)));

            EventManager.RegisterClassHandler(typeof(DockGroupControl), Thumb.DragStartedEvent, new DragStartedEventHandler(OnDragStarted));
            EventManager.RegisterClassHandler(typeof(DockGroupControl), Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnDragDelta));
            EventManager.RegisterClassHandler(typeof(DockGroupControl), Thumb.DragCompletedEvent, new DragCompletedEventHandler(OnDragCompleted));
        }

        /// <summary>
        /// Initializes instance members of the <see cref="DockGroupControl"/> class.
        /// </summary>
        public DockGroupControl()
        {

        }

        #endregion

        #region "Properties"

        #region Orientation

        /// <summary>
        /// Identifies the <see cref="Orientation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation",
            typeof(Orientation),
            typeof(DockGroupControl),
            new FrameworkPropertyMetadata(Orientation.Horizontal));

        /// <summary>
        /// Gets or sets the Orientation property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        #endregion

        #region DraggingMode

        /// <summary>
        /// Identifies the <see cref="DraggingMode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DraggingModeProperty = DependencyProperty.Register(
            "DraggingMode",
            typeof(DraggingMode),
            typeof(DockGroupControl),
            new FrameworkPropertyMetadata(DraggingMode.Deferred));

        /// <summary>
        /// Gets or sets the DraggingMode property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public DraggingMode DraggingMode
        {
            get { return (DraggingMode)GetValue(DraggingModeProperty); }
            set { SetValue(DraggingModeProperty, value); }
        }

        #endregion

        #region GripLength

        /// <summary>
        /// Identifies the <see cref="GripLength"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty GripLengthProperty = DependencyProperty.Register(
            "GripLength",
            typeof(double),
            typeof(DockGroupControl),
            new FrameworkPropertyMetadata(6.0));

        /// <summary>
        /// Gets or sets the GripLength property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public double GripLength
        {
            get { return (double)GetValue(GripLengthProperty); }
            set { SetValue(GripLengthProperty, value); }
        }

        #endregion

        #region KeyboardIncrement

        /// <summary>
        /// Identifies the <see cref="KeyboardIncrement"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty KeyboardIncrementProperty = DependencyProperty.Register(
            "KeyboardIncrement",
            typeof(double),
            typeof(DockGroupControl),
            new FrameworkPropertyMetadata(10.0));

        /// <summary>
        /// Gets or sets a value that represents the distance by which
        /// the <see cref="SplitterGrip"/> moves on each arrow key press. 
        /// This is a dependency property.
        /// </summary>
        /// <value>
        /// The distance the <see cref="SplitterGrip"/> moves for each press on an arrow key.
        /// The default value is 10.
        /// </value>
        [Bindable(true)]
        public double KeyboardIncrement
        {
            get { return (double)GetValue(KeyboardIncrementProperty); }
            set { SetValue(KeyboardIncrementProperty, value); }
        }

        #endregion

        #endregion

        #region "Private & Internal Properties"

        /// <summary>
        /// 
        /// </summary>
        internal bool DisallowPanelInvalidation { get; set; }

        private IDraggingStrategy DefferedDraggingStrategy
        {
            get { return _defferedDraggingStrategy ?? (_defferedDraggingStrategy = new DeferredDraggingStrategy()); }
        }

        private IDraggingStrategy ContinuousDraggingStrategy
        {
            get
            {
                return _continuousDraggingStrategy ?? (_continuousDraggingStrategy = new ContinuousDraggingStrategy());
            }
        }

        internal IDraggingStrategy ActiveDraggingStrategy
        {
            get
            {
                return DraggingMode == DraggingMode.Continuous
                    ? ContinuousDraggingStrategy
                    : DefferedDraggingStrategy;
            }
        }

        #endregion

        #region "Methods"

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is SplitterItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SplitterItem();
        }

        #endregion

        #region "Static Methods"

        private static void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            var grip = e.OriginalSource as SplitterGrip;
            if (grip == null)
                return;

            var itemsControl = (DockGroupControl)sender;
            itemsControl.ActiveDraggingStrategy.OnDragStarted(grip, e);
        }

        private static void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var grip = e.OriginalSource as SplitterGrip;
            if (grip == null)
                return;

            var itemsControl = (DockGroupControl)sender;
            itemsControl.ActiveDraggingStrategy.OnDragDelta(grip, e);
        }

        private static void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            var grip = e.OriginalSource as SplitterGrip;
            if (grip == null)
                return;

            var itemsControl = (DockGroupControl)sender;
            itemsControl.ActiveDraggingStrategy.OnDragCompleted(grip, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        internal static SplitterItemsPanel PanelFromContainer(SplitterItem container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return VisualTreeHelper.GetParent(container) as SplitterItemsPanel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static double GetUnitForSize(DockGroupControl itemsControl, double size)
        {
            var nbrChildren = itemsControl.Items.Count;
            double allUnits = 0.0;
            double allSize = 0.0;
            for (int i = 0; i < nbrChildren; i++)
            {
                var container = itemsControl.ItemContainerGenerator.ContainerFromIndex(i) as SplitterItem;
                if (container == null || container.IsFixed)
                    continue;

                allUnits += container.Length.Value;
                allSize += itemsControl.Orientation == Orientation.Vertical
                    ? container.ActualWidth
                    : container.ActualHeight;
            }

            var diffUnit = (allUnits * Math.Abs(size)) / allSize;
            return diffUnit;
        }

        #endregion
    }
}
