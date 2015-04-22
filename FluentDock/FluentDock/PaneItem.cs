using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FluentDock.Internals;

namespace FluentDock
{
    public class PaneItem : TabItem
    {
        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="PaneItem"/> class.
        /// </summary>
        static PaneItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PaneItem), new FrameworkPropertyMetadata(typeof(PaneItem)));
        }

        /// <summary>
        /// Initializes instance members of the <see cref="PaneItem"/> class.
        /// </summary>
        public PaneItem() : base()
        {

        }

        #endregion

        #region "Properties"

        #region IsHeaderCollapsed

        private static readonly DependencyPropertyKey IsHeaderCollapsedPropertyKey = DependencyProperty.RegisterReadOnly(
            "IsHeaderCollapsed",
            typeof(bool),
            typeof(PaneItem),
            new FrameworkPropertyMetadata(BooleanBoxes.False));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsHeaderCollapsedProperty
            = IsHeaderCollapsedPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the IsHeaderCollapsed property. This dependency property 
        /// indicates ....
        /// </summary>
        public bool IsHeaderCollapsed
        {
            get { return (bool)GetValue(IsHeaderCollapsedProperty); }
            internal set { SetValue(IsHeaderCollapsedPropertyKey, BooleanBoxes.Box(value)); }
        }

        #endregion

        #region Title

        /// <summary>
        /// Identifies the <see cref="Title"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(object),
            typeof(PaneItem),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the Title property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        #endregion

        #region IsPinned

        /// <summary>
        /// Identifies the <see cref="IsPinned"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPinnedProperty = DependencyProperty.Register(
            "IsPinned",
            typeof(bool),
            typeof(PaneItem),
            new FrameworkPropertyMetadata(
                BooleanBoxes.False,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (o, args) =>
                {
                    var pane = (PaneItem) o;

                    if ((bool)args.NewValue)
                    {
                        pane.OnPinned();
                    }
                    else
                    {
                        pane.OnUnpinned();
                    }
                }));


        /// <summary>
        /// Gets or sets the IsPinned property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public bool IsPinned
        {
            get { return (bool)GetValue(IsPinnedProperty); }
            set { SetValue(IsPinnedProperty, BooleanBoxes.Box(value)); }
        }

        #endregion

        #region Icon

        /// <summary>
        /// Identifies the <see cref="Icon"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon",
            typeof(object),
            typeof(PaneItem),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the Icon property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        #endregion

        #region IsActive

        private static readonly DependencyPropertyKey IsActivePropertyKey = DependencyProperty.RegisterReadOnly(
            "IsActive",
            typeof(bool),
            typeof(PaneItem),
            new FrameworkPropertyMetadata(BooleanBoxes.False));

        public static readonly DependencyProperty IsActiveProperty
            = IsActivePropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the IsActive property. This dependency property 
        /// indicates ....
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            internal set { SetValue(IsActivePropertyKey, BooleanBoxes.Box(value)); }
        }

        #endregion

        #endregion

        #region "Methods"

        protected virtual void OnPinned()
        {
            
        }

        protected virtual void OnUnpinned()
        {

        }

        #endregion
    }
}
