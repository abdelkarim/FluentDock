﻿using System.Windows.Markup;

namespace FluentDock.Model
{
    [ContentProperty("Items")]
    public class DockPaneGroup : DockingView
    {
        #region "Constructors"

        /// <summary>
        /// Initializes instance members of the <see cref="DockPaneGroup"/> class.
        /// </summary>
        public DockPaneGroup()
        {
            Items = new PaneViewCollection();
        }

        #endregion

        #region "Properties"

        public PaneViewCollection Items { get; private set; }

        public double DockedWidth { get; set; }
        public double DockedHeight { get; set; }
        public double FloatingWidth { get; set; }
        public double FloatingHeight { get; set; }

        #endregion
    }
}