// Copyright (c) 2014 'Abdelkarim Sellamna' (abdelkarim.se [at] gmail [dot] com)
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FluentDock.Model;

namespace FluentDock
{
    /// <summary>
    /// Represents the root control of the docking solution.
    /// </summary>
    public class DockingContainer : Control
    {
        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="DockingContainer"/> class.
        /// </summary>
        static DockingContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockingContainer), new FrameworkPropertyMetadata(typeof(DockingContainer)));
        }

        #endregion

        #region "Properties"

        #region Profile

        /// <summary>
        /// Identifies the <see cref="Profile"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ProfileProperty = DependencyProperty.Register(
            "Profile",
            typeof(DockingProfile),
            typeof(DockingContainer),
            new FrameworkPropertyMetadata(
                null,
                (o, args) =>
                {
                    var dockingContainer = (DockingContainer)o;
                    dockingContainer.OnItemsSourceChange((DockingProfile)args.OldValue, (DockingProfile)args.NewValue);
                }));

        /// <summary>
        /// Gets or sets the Profile property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public DockingProfile Profile
        {
            get { return (DockingProfile)GetValue(ProfileProperty); }
            set { SetValue(ProfileProperty, value); }
        }

        #endregion

        #endregion

        #region "Methods"

        protected virtual void OnItemsSourceChange(DockingProfile oldProfile, DockingProfile newProfile)
        {
            
        }

        #endregion
    }
}
