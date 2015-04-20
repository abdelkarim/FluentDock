// Copyright (c) 2014 'Abdelkarim Sellamna' (abdelkarim.se [at] gmail [dot] com)
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;
using System.Windows.Controls;
using FluentDock.Internals;

namespace FluentDock
{
    public class AutoHideRootControl : ItemsControl
    {
        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="AutoHideRootControl"/> class.
        /// </summary>
        static AutoHideRootControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoHideRootControl), new FrameworkPropertyMetadata(typeof(AutoHideRootControl)));

            FocusableProperty.OverrideMetadata(typeof(AutoHideRootControl), new FrameworkPropertyMetadata(BooleanBoxes.False));
            IsTabStopProperty.OverrideMetadata(typeof(AutoHideRootControl), new FrameworkPropertyMetadata(BooleanBoxes.False));
        }

        #endregion
    }
}
