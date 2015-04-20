// Copyright (c) 2014 'Abdelkarim Sellamna' (abdelkarim.se [at] gmail [dot] com)
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows.Controls;
using System.Windows.Markup;

namespace FluentDock.Model
{
    [ContentProperty("Groups")]
    public class AutoHideChannelView : DockingView
    {
        #region "Fields"

        private Dock _location;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes instance members of the <see cref="AutoHideChannelView"/> class.
        /// </summary>
        public AutoHideChannelView()
        {
            Groups = new AutoHideGroupViewCollection();
        }

        #endregion

        #region "Properties"

        public Dock Location
        {
            get { return _location; }
            set
            {
                if (_location == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _location = value;
                RaisePropertyChanged();
            }
        }

        public AutoHideGroupViewCollection Groups { get; private set; }

        #endregion
    }
}