// Copyright (c) 2014 'Abdelkarim Sellamna' (abdelkarim.se [at] gmail [dot] com)
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows.Markup;

namespace FluentDock.Model
{
    [ContentProperty("Content")]
    public class PaneView : View
    {
        #region "Fields"

        private string _title;
        private string _header;
        private object _content;
        private double _dockedHeight;
        private double _dockedWidth;
        private double _floatingHeight;
        private double _floatingWidth;

        #endregion

        #region "Properties"

        public object Content
        {
            get { return _content; }
            set
            {
                if (_content == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _content = value;
                RaisePropertyChanged();
            }
        }

        public string Header
        {
            get { return _header; }
            set
            {
                if (_header == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _header = value;
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                    return;

                RaisePropertyChanging();
                _title = value;
                RaisePropertyChanged();
            }
        }

        public double DockedWidth
        {
            get { return _dockedWidth; }
            set
            {
                if (_dockedWidth == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _dockedWidth = value;
                RaisePropertyChanged();
            }
        }

        public double DockedHeight
        {
            get { return _dockedHeight; }
            set
            {
                if (_dockedHeight == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _dockedHeight = value;
                RaisePropertyChanged();
            }
        }

        public double FloatingWidth
        {
            get { return _floatingWidth; }
            set
            {
                if (_floatingWidth == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _floatingWidth = value;
                RaisePropertyChanged();
            }
        }

        public double FloatingHeight
        {
            get { return _floatingHeight; }
            set
            {
                if (_floatingHeight == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _floatingHeight = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}