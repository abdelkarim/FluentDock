using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FluentDock.Internals;

namespace FluentDock.Primitives
{
    /// <summary>
    /// 
    /// </summary>
    public class SplitterGrip : Thumb
    {
        #region "Constructors"

        /// <summary>
        /// initializes static members of the <see cref="SplitterGrip"/> class.
        /// </summary>
        static SplitterGrip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitterGrip), new FrameworkPropertyMetadata(typeof(SplitterGrip)));
            FocusableProperty.OverrideMetadata(typeof(SplitterGrip), new FrameworkPropertyMetadata(BooleanBoxes.False));
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
            typeof(SplitterGrip),
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

        #endregion

        #region "Internal Properties"

        /// <summary>
        /// Gets the <see cref="System.Windows.Controls.Primitives.Popup"/>
        /// </summary>
        internal Popup Popup { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal SplitterItem LeftChild { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal SplitterItem RightChild { get; set; }

        #endregion

        #region "Methods"

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Popup = GetTemplateChild("PART_Popup") as Popup;
        }

        #endregion
    }
}
