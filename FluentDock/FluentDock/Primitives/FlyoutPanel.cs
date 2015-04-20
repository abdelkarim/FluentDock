// Copyright (c) 2014 'Abdelkarim Sellamna' (abdelkarim.se [at] gmail [dot] com)
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FluentDock.Primitives
{
    /// <summary>
    /// a custom panel to be used as the hosting panel for the <see cref="AutoHideGroupControl"/>
    /// </summary>
    public class FlyoutPanel : Panel
    {
        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="FlyoutPanel"/> class.
        /// </summary>
        static FlyoutPanel()
        {
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(FlyoutPanel), new FrameworkPropertyMetadata(KeyboardNavigationMode.Once));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(FlyoutPanel), new FrameworkPropertyMetadata(KeyboardNavigationMode.Once));
        }

        /// <summary>
        /// Initializes instance members of the <see cref="FlyoutPanel"/> class.
        /// </summary>
        public FlyoutPanel()
        {

        }

        #endregion

        #region "Internal Properties"

        private  AutoHideGroupControl Owner
        {
            get { return ItemsControl.GetItemsOwner(this) as AutoHideGroupControl; }
        }

        #endregion

        #region "Methods"

        protected override Size MeasureOverride(Size availableSize)
        {
            var itemsOwner = Owner;
            if (itemsOwner == null)
                return base.MeasureOverride(availableSize);

            var tabStripPlacement = itemsOwner.TabStripPlacement;
            var isHorizontal = tabStripPlacement == Dock.Top || tabStripPlacement == Dock.Bottom;
            var desiredSize = new Size();

            // measure all children
            foreach (UIElement child in this.InternalChildren)
            {
                child.Measure(availableSize);
                var childDesiredSize = child.DesiredSize;

                if (isHorizontal)
                {
                    desiredSize.Width += childDesiredSize.Width;
                    desiredSize.Height = Math.Max(desiredSize.Height, childDesiredSize.Height);
                }
                else
                {
                    desiredSize.Width = Math.Max(desiredSize.Width, childDesiredSize.Width);
                    desiredSize.Height += child.DesiredSize.Height;
                }
            }

            // add spacing between items;
            var count = this.InternalChildren.Count;
            var separatorSpace = Math.Max(0.0, count - 1) * itemsOwner.SeparatorSpace;
            if (isHorizontal)
                desiredSize.Width += separatorSpace;
            else
                desiredSize.Height += separatorSpace;

            return desiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var itemsOwner = Owner;
            if (itemsOwner == null)
                return base.MeasureOverride(finalSize);

            var tabStripPlacement = itemsOwner.TabStripPlacement;
            var isHorizontal = tabStripPlacement == Dock.Top || tabStripPlacement == Dock.Bottom;
            var separatorSpace = itemsOwner.SeparatorSpace;
            var finalRect = new Rect(finalSize);

            if (isHorizontal)
            {
                foreach (UIElement child in InternalChildren)
                {
                    var width = child.DesiredSize.Width;
                    finalRect.Width = width;
                    child.Arrange(finalRect);
                    finalRect.X += width + separatorSpace;
                }
            }
            else
            {
                foreach (UIElement child in InternalChildren)
                {
                    var height = child.DesiredSize.Height;
                    finalRect.Height = height;
                    child.Arrange(finalRect);
                    finalRect.Y += height + separatorSpace;
                }
            }

            return finalSize;
        }

        #endregion
    }
}
