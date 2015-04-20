using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Markup;

namespace FluentDock.Model
{
    [ContentProperty("Items")]
    public class DockGroup : DockingView
    {
        #region "Fields"

        private Orientation _orientation;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes instance members of the <see cref="DockGroup"/> class.
        /// </summary>
        public DockGroup()
        {
            this.Items = new ObservableCollection<DockingView>();
        }

        #endregion

        #region "Properties"

        public Orientation Orientation
        {
            get { return _orientation; }
            set
            {
                if (_orientation == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _orientation = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<DockingView> Items { get; private set; }

        #endregion
    }
}