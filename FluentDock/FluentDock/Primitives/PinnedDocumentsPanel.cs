using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FluentDock.Internals;

namespace FluentDock.Primitives
{
    public class PinnedDocumentsPanel : Panel
    {
        #region "Fields"

        private double _wrapWidth;
        private double _firstRowWrapWidth;

        #endregion

        #region "Properties"

        #region HasMultipleRows

        /// <summary>
        /// HasMultipleRows Read-Only Dependency Property
        /// </summary>
        private static readonly DependencyPropertyKey HasMultipleRowsPropertyKey = DependencyProperty.RegisterReadOnly(
            "HasMultipleRows",
            typeof(bool),
            typeof(PinnedDocumentsPanel),
            new FrameworkPropertyMetadata(BooleanBoxes.False));

        public static readonly DependencyProperty HasMultipleRowsProperty
            = HasMultipleRowsPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets a value that indicates if the <see cref="PinnedDocumentsPanel"/> is hosting its children
        /// in multiple rows.
        /// </summary>
        public bool HasMultipleRows
        {
            get { return (bool)GetValue(HasMultipleRowsProperty); }
            internal set { SetValue(HasMultipleRowsPropertyKey, BooleanBoxes.Box(value)); }
        }

        #endregion

        #region ScrollButtonWidth

        // TODO: the value is not yet bound in the user interface.

        /// <summary>
        /// Identifies the <see cref="ScrollButtonWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScrollButtonWidthProperty = DependencyProperty.Register(
            "ScrollButtonWidth",
            typeof(double),
            typeof(PinnedDocumentsPanel),
            new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// Gets or sets the ScrollButtonWidth property. This is a dependency property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        [Bindable(true)]
        public double ScrollButtonWidth
        {
            get { return (double)GetValue(ScrollButtonWidthProperty); }
            set { SetValue(ScrollButtonWidthProperty, value); }
        }

        #endregion

        #endregion

        #region "Internal Properties"

        private DocumentPaneControl DocumentPaneControl
        {
            get
            {
                return this.TemplatedParent as DocumentPaneControl;
            }
        }

        private DocumentsPanel DocumentsPanel
        {
            get
            {
                var control = DocumentPaneControl;
                return control == null ? null : control.DocumentsPanel;
            }
        }

        public double WrapWidth
        {
            get { return _wrapWidth; }
        }

        public double FirstRowWrapWidth
        {
            get { return _firstRowWrapWidth; }
        }

        #endregion

        #region "Methods"

        protected override Size MeasureOverride(Size availableSize)
        {
            this.HasMultipleRows = false;

            if (this.InternalChildren.Count == 0)
            {
                return base.MeasureOverride(availableSize);
            }

            /**/
            
            // measure the documents
            var documents = this.InternalChildren.Cast<DocumentPaneItem>().ToList();
            MeasureDocuments(documents);

            // compute the wrap values
            CalculateWrapWidth(availableSize.Width, out _firstRowWrapWidth);
            _wrapWidth = availableSize.Width;

            // find the arrange status
            double desiredWidth;
            double desiredHeight;
            var arrangeStatus = FindArrangeStatus(documents, out desiredWidth, out desiredHeight);

            if (arrangeStatus != ArrangeStatus.MultipleRows)
            {
                if (arrangeStatus == ArrangeStatus.SingleRowWithOverflow)
                {
                    this.HasMultipleRows = true;
                }

                return new Size(desiredWidth, desiredHeight);
            }

            this.HasMultipleRows = true;
            return new Size(desiredWidth, desiredHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.InternalChildren.Count == 0)
            {
                return base.ArrangeOverride(finalSize);
            }

            var rect = new Rect();
            var documents = this.InternalChildren.Cast<DocumentPaneItem>();
            bool hasMultipleRows = this.HasMultipleRows;

            foreach (var document in documents)
            {
                var desiredSize = document.DesiredSize;

                if (hasMultipleRows && (rect.X + desiredSize.Width > this.WrapWidth))
                {
                    rect.Y += desiredSize.Height;
                    rect.X = 0.0;
                }

                rect.Width = desiredSize.Width;
                rect.Height = desiredSize.Height;
                document.Arrange(rect);
                rect.X += rect.Width;
            }

            return finalSize;
        }

        protected override UIElementCollection CreateUIElementCollection(FrameworkElement logicalParent)
        {
            return base.CreateUIElementCollection((FrameworkElement) this.TemplatedParent);
        }

        #endregion

        #region "Private (helper) Methods"

        private void CalculateWrapWidth(double availableWidth, out double wrapWidth)
        {
            // the PinnedDocumentsPanel should at least leave room for a single unpinned item
            // either selected or not, as well as the scroll bar buttons.
            var documentsPanel = this.DocumentsPanel;
            DocumentPaneItem pivotalChild = null;
            if (documentsPanel != null && documentsPanel.Children.Count > 0)
            {
                pivotalChild = documentsPanel.Children[0] as DocumentPaneItem;
            }

            if (pivotalChild != null)
            {
                pivotalChild.Measure(new Size(availableWidth, Double.PositiveInfinity));
                wrapWidth = Math.Max(0.0, availableWidth - (pivotalChild.DesiredSize.Width + (this.ScrollButtonWidth*2)));
            }
            else
            {
                wrapWidth = availableWidth;
            }
        }

        private void MeasureDocuments(IList<DocumentPaneItem> documents)
        {
            var documentMeasureSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            foreach (var document in documents)
            {
                document.Measure(documentMeasureSize);
            }
        }

        private ArrangeStatus FindArrangeStatus(IList<DocumentPaneItem> documents,
            out double desiredWidth,
            out double desiredHeight)
        {
            double panelDesiredWidth = documents.Sum(doc => doc.DesiredSize.Width);
            desiredHeight = documents.First().DesiredSize.Height;

            if (panelDesiredWidth <= this.FirstRowWrapWidth)
            {
                desiredWidth = panelDesiredWidth;
                return ArrangeStatus.SingleRow;
            }

            if (panelDesiredWidth <= this.WrapWidth)
            {
                desiredWidth = panelDesiredWidth;
                return ArrangeStatus.SingleRowWithOverflow;
            }

            int rows = 1;
            desiredWidth = 0.0;
            var rowDesiredWidth = 0.0;
            foreach (var document in documents)
            {
                var documentDesiredWidth = document.DesiredSize.Width;
                if (rowDesiredWidth + document.DesiredSize.Width > this.WrapWidth)
                {
                    desiredWidth = Math.Max(desiredWidth, rowDesiredWidth);
                    rowDesiredWidth = 0.0;
                    rows++;
                }
                else
                {
                    rowDesiredWidth += documentDesiredWidth;
                }
            }

            // another check in case we exit the foreach without getting into the if clause
            desiredWidth = Math.Max(desiredWidth, rowDesiredWidth);
            desiredHeight = desiredHeight * rows;
            return ArrangeStatus.MultipleRows;
        }

        #endregion

        #region "Internal Methods"

        internal void UnpinChild(DocumentPaneItem documentPaneItem)
        {
            if (documentPaneItem == null)
            {
                throw new ArgumentNullException("documentPaneItem");
            }

            //TODO: what should we do in case the items panel is missing from the template.
            var documentsPanel = this.DocumentsPanel;
            if (documentsPanel == null)
            {
                return;
            }

            this.Children.Remove(documentPaneItem);
            documentsPanel.InsertChild(documentPaneItem);
        }

        #endregion

        #region "Private Types"

        /// <summary>
        /// 
        /// </summary>
        private enum ArrangeStatus
        {
            /// <summary>
            /// Indicates that the pinned documents can be arrange on a single row,
            /// but unpinned documents has to move to the second.
            /// </summary>
            SingleRowWithOverflow,

            /// <summary>
            /// Inidcates that pinned and unpinned documents can share the same row.
            /// </summary>
            SingleRow,

            /// <summary>
            /// Indicates that pinned documents will have multiple row to display
            /// all the documents.
            /// </summary>
            MultipleRows
        }

        #endregion
    }
}