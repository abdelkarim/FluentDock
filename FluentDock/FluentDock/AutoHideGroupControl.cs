// Copyright (c) 2014 'Abdelkarim Sellamna' (abdelkarim.se [at] gmail [dot] com)
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FluentDock.Primitives;

namespace FluentDock
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof (AutoHideGroupItem))]
    public class AutoHideGroupControl : TabControl
    {
        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="AutoHideGroupControl"/> class.
        /// </summary>
        static AutoHideGroupControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoHideGroupControl), new FrameworkPropertyMetadata(typeof(AutoHideGroupControl)));
            InitPanel();
        }

        /// <summary>
        /// Initializes instance members of the <see cref="AutoHideGroupControl"/> class.
        /// </summary>
        public AutoHideGroupControl()
        {
            SelectedIndex = -1;
        }

        #endregion

        #region "Properties"

        #region SeparatorSpace

        /// <summary>
        /// Identifies the <see cref="SeparatorSpace"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SeparatorSpaceProperty = DependencyProperty.Register(
            "SeparatorSpace",
            typeof(double),
            typeof(AutoHideGroupControl),
            new FrameworkPropertyMetadata(6.0));

        /// <summary>
        /// Gets or sets the SeparatorSpace property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public double SeparatorSpace
        {
            get { return (double)GetValue(SeparatorSpaceProperty); }
            set { SetValue(SeparatorSpaceProperty, value); }
        }

        #endregion

        #endregion

        #region "Static Methods"

        private static void InitPanel()
        {
            var itemsPanelTemplate = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(FlyoutPanel)));
            itemsPanelTemplate.Seal();
            ItemsPanelProperty.OverrideMetadata(typeof(AutoHideGroupControl), new FrameworkPropertyMetadata(itemsPanelTemplate));
        }

        #endregion

        #region "Methods"

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is AutoHideGroupItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new AutoHideGroupItem();
        }

        #endregion
    }
}