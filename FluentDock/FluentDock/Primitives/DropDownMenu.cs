using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FluentDock.Internals;

namespace FluentDock.Primitives
{
    public class DropDownMenu : Button
    {
        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="DropDownMenu"/> class.
        /// </summary>
        static DropDownMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownMenu), new FrameworkPropertyMetadata(typeof(DropDownMenu)));
            ClickModeProperty.OverrideMetadata(typeof(DropDownMenu), new FrameworkPropertyMetadata(ClickMode.Release));
        }

        #endregion

        #region "Properties"

        #region ItemsSource

        /// <summary>
        /// Identifies the <see cref="ItemsSource"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(IEnumerable),
            typeof(DropDownMenu),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the ItemsSource property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        #endregion

        #region IsDropDownOpen

        /// <summary>
        /// IsDropDownOpen Read-Only Dependency Property
        /// </summary>
        private static readonly DependencyPropertyKey IsDropDownOpenPropertyKey
            = DependencyProperty.RegisterReadOnly("IsDropDownOpen", typeof(bool), typeof(DropDownMenu),
                new FrameworkPropertyMetadata(BooleanBoxes.False));

        public static readonly DependencyProperty IsDropDownOpenProperty
            = IsDropDownOpenPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the IsDropDownOpen property. This dependency property 
        /// indicates ....
        /// </summary>
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            private set { SetValue(IsDropDownOpenPropertyKey, BooleanBoxes.Box(value)); }
        }


        #endregion

        #endregion

        #region "Methods"

        protected override void OnIsPressedChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsPressedChanged(e);

            var contextMenu = this.ContextMenu;
            if (contextMenu == null || ClickMode != ClickMode.Release || !IsPressed)
            {
                return;
            }

            if (IsDropDownOpen)
            {
                HideContextMenu();
            }
            else
            {
                ShowContextMenu();
            }
        }

        private void ShowContextMenu()
        {
            var contextMenu = this.ContextMenu;
            if (contextMenu.PlacementTarget != this)
                contextMenu.PlacementTarget = this;

            contextMenu.Placement = PlacementMode.Bottom;
            this.IsDropDownOpen = true;
            contextMenu.IsOpen = true;
            contextMenu.Closed += OnContextMenuClosed;
        }

        private void HideContextMenu()
        {
            var contextMenu = this.ContextMenu;
            if (!IsDropDownOpen || contextMenu == null)
            {
                return;
            }
            contextMenu.IsOpen = false;
        }

        private void OnContextMenuClosed(object sender, RoutedEventArgs e)
        {
            // remove the event handler
            this.ContextMenu.Closed -= OnContextMenuClosed;
            this.IsDropDownOpen = false;
        }

        #endregion
    }
}
