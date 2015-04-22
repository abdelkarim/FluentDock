using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using FluentDock.Internals;

namespace FluentDock.Primitives
{
    /// <summary>
    /// Used as the items host of the <see cref="DocumentPaneControl"/> control.
    /// </summary>
    public class DocumentsPanel : VirtualizingPanel
    {
        #region "Fields"

        private IList<DocumentPaneItem> _generatedContainers;
        private DispatcherOperation _currentOperation;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes static members of the <see cref="DocumentsPanel"/> class.
        /// </summary>
        static DocumentsPanel()
        {
            
        }

        /// <summary>
        /// Initializes instance members of the <see cref="DocumentsPanel"/> class.
        /// </summary>
        public DocumentsPanel()
        {
            _generatedContainers = new List<DocumentPaneItem>();
        }

        #endregion

        #region "Private Properties"

        private DocumentPaneControl DocumentPaneControl
        {
            get
            {
                return this.ParentOfType<DocumentPaneControl>();
            }
        }

        private PinnedDocumentsPanel PinnedDocumentsPanel
        {
            get
            {
                var control = DocumentPaneControl;
                return control == null ? null : control.PinnedDocumentsPanel;
            }
        }

        private int ItemsCount
        {
            get
            {
                var itemsOwner = this.DocumentPaneControl;
                return itemsOwner == null ? 0 : itemsOwner.Items.Count;
            }
        }

        #endregion

        #region "Protected Methods"

        protected override void OnIsItemsHostChanged(bool oldIsItemsHost, bool newIsItemsHost)
        {
            base.OnIsItemsHostChanged(oldIsItemsHost, newIsItemsHost);
            InvalidateContainers();
        }

        protected override void OnItemsChanged(object sender, ItemsChangedEventArgs args)
        {
            base.OnItemsChanged(sender, args);
            InvalidateContainers();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var desiredWidth = 0.0;
            var desiredHeight = 0.0;
            var documentMeasureSize = new Size(double.PositiveInfinity, double.PositiveInfinity);

            var documents = this.InternalChildren.Cast<DocumentPaneItem>();
            foreach (var document in documents)
            {
                document.Measure(documentMeasureSize);
                desiredWidth += document.DesiredSize.Width;
                desiredHeight = Math.Max(document.DesiredSize.Height, desiredHeight);
            }

            return new Size(desiredWidth, desiredHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var rect = new Rect(finalSize);
            var documents = this.InternalChildren.Cast<DocumentPaneItem>();
            
            foreach (var document in documents)
            {
                rect.Width = document.DesiredSize.Width;
                document.Arrange(rect);
                rect.X += rect.Width;
            }

            return finalSize;
        }

        #endregion

        #region "Private Methods"

        private void InvalidateContainers()
        {
            if (_currentOperation != null)
                _currentOperation.Abort();

            Action action = delegate
            {
                _currentOperation = null;
                GenerateContainers();
            };

            _currentOperation = Dispatcher.BeginInvoke(action, DispatcherPriority.Normal);
        }

        private void GenerateContainers()
        {
            var count = ItemsCount;
            if (count == 0)
            {
                return;
            }

            var generator = ItemContainerGenerator;
            Cleanup(generator, _generatedContainers);

            int index = -1;
            while (index < (count - 1))
            {
                var container = GenerateChild(generator, ++index);
                _generatedContainers.Add(container);
            }
        }

        private DocumentPaneItem GenerateChild(IItemContainerGenerator generator, int index, int uiIndex = -1)
        {
            var position = generator.GeneratorPositionFromIndex(index);
            using (generator.StartAt(position, GeneratorDirection.Forward, false))
            {
                var container = (DocumentPaneItem)generator.GenerateNext();
                generator.PrepareItemContainer(container);

                if (container.IsPinned)
                {
                    PinnedDocumentsPanel.Children.Add(container);
                }
                else
                {
                    this.AddInternalChild(container);
                }

                return container;
            }
        }

        private void Cleanup(IItemContainerGenerator generator, IEnumerable<UIElement> children)
        {
            foreach (var child in children)
            {
                var childIndex = InternalChildren.IndexOf(child);
                VirtualizeContainer(generator, child, childIndex);
            }
        }

        private void VirtualizeContainer(IItemContainerGenerator generator, UIElement container, int indexOf)
        {
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            int index = indexOf;
            // remove from the visual tree
            RemoveInternalChildRange(indexOf, 1);

            // remove from the generator
            var trackPosition = generator.GeneratorPositionFromIndex(index);
            generator.Remove(trackPosition, 1);
        }

        #endregion

        #region "Internal Methods"

        /// <summary>
        /// Invoked by the <see cref="Primitives.PinnedDocumentsPanel"/> to unpin a child.
        /// </summary>
        /// <param name="documentPaneItem">The <see cref="DocumentPaneItem"/> being unpinned.</param>
        internal void InsertChild(DocumentPaneItem documentPaneItem)
        {
            if (documentPaneItem == null)
            {
                throw new ArgumentNullException("documentPaneItem");
            }

            // since we're virtualizing the scrolling region,
            // we first check if we need to virtualize.

            // TODO: initil implementation, needs improvements on where to insert the just-unpinned document
            this.AddInternalChild(documentPaneItem);
        }

        internal void PinChild(DocumentPaneItem documentPaneItem)
        {
            var pinnedDcoumentsPanel = this.PinnedDocumentsPanel;
            if (pinnedDcoumentsPanel == null)
            {
                return;
            }

            this.InternalChildren.RemoveNoVerify(documentPaneItem);
            pinnedDcoumentsPanel.Children.Add(documentPaneItem);

            this.InvalidateMeasure();
            pinnedDcoumentsPanel.InvalidateMeasure();
        }

        #endregion
    }
}