using System.Windows.Markup;

namespace FluentDock.Model
{
    [ContentProperty("Items")]
    public class DocumentPaneGroup : DockingView
    {
        #region "Constructors"

        /// <summary>
        /// Initializes instance members of the <see cref="DocumentPaneGroup"/> class.
        /// </summary>
        public DocumentPaneGroup()
        {
            Items = new PaneViewCollection();
        }

        #endregion

        #region "Properties"

        public PaneViewCollection Items { get; private set; }

        #endregion
    }
}
