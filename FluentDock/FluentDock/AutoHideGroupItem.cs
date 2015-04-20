// Copyright (c) 2014 'Abdelkarim Sellamna' (abdelkarim.se [at] gmail [dot] com)
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FluentDock
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoHideGroupItem : TabItem
    {
        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="AutoHideGroupItem"/> class.
        /// </summary>
        static AutoHideGroupItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoHideGroupItem), new FrameworkPropertyMetadata(typeof(AutoHideGroupItem)));
        }

        /// <summary>
        /// Initializes instance members of the <see cref="AutoHideGroupItem"/> class.
        /// </summary>
        public AutoHideGroupItem()
        {

        }

        #endregion

        #region "Properties"

        #region Location

        /// <summary>
        /// Identifies the <see cref="Location"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(
            "Location",
            typeof(Dock),
            typeof(AutoHideGroupItem),
            new FrameworkPropertyMetadata(Dock.Left));

        /// <summary>
        /// Gets or sets the Location property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public Dock Location
        {
            get { return (Dock)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        #endregion

        #region Title

        /// <summary>
        /// Identifies the <see cref="Title"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(object),
            typeof(AutoHideGroupItem),
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
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        #endregion

        #region TitleTemplate

        /// <summary>
        /// Identifies the <see cref="TitleTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register(
            "TitleTemplate",
            typeof(DataTemplate),
            typeof(AutoHideGroupItem),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the TitleTemplate property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        #endregion

        #endregion
    }
}
