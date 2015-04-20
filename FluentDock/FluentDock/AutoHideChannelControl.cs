// Copyright (c) 2014 'Abdelkarim Sellamna' (abdelkarim.se [at] gmail [dot] com)
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FluentDock
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(AutoHideGroupControl))]
    public class AutoHideChannelControl : ItemsControl
    {
        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="AutoHideChannelControl"/> class.
        /// </summary>
        static AutoHideChannelControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoHideChannelControl), new FrameworkPropertyMetadata(typeof(AutoHideChannelControl)));
        }

        /// <summary>
        /// Initializes instance members of the <see cref="AutoHideChannelControl"/> class.
        /// </summary>
        public AutoHideChannelControl()
        {

        }

        #endregion

        #region "Properties"

        #region Dock

        /// <summary>
        /// Identifies the <see cref="Dock"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DockProperty = DependencyProperty.Register(
            "Dock",
            typeof(Dock),
            typeof(AutoHideChannelControl),
            new FrameworkPropertyMetadata(Dock.Left));

        /// <summary>
        /// Gets or sets the Dock property. This is a dependency property.
        /// </summary>
        [Bindable(true)]
        public Dock Dock
        {
            get { return (Dock)GetValue(DockProperty); }
            set { SetValue(DockProperty, value); }
        }

        #endregion

        #endregion

        #region "Internal Properties"

        #endregion

        #region "Methods"

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is AutoHideGroupControl;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new AutoHideGroupControl();
        }

        #endregion
    }
}